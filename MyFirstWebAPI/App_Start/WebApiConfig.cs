using Microsoft.Practices.Unity;
using MyFirstWebAPI.Controllers;
using MyFirstWebAPI.Models;
using MyFirstWebAPI.Models.Utility;
using MyFirstWebAPI.Models.Connection;
using MyFirstWebAPI.Utility;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MyFirstWebAPI.Models.People;
using MyFirstWebAPI.Models.SchoolInfo;
using MyFirstWebAPI.APIInterception;
using System.Web.Http.Dispatcher;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.Logging.PolicyInjection;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace MyFirstWebAPI
{
    public static class WebApiConfig
    {
        private const string Timetablelegacy_Type = "timetablelegacy";
        private const string TimetableSingle_Type = "timetablesingle";
        private const string TimetableMultiple_Type = "timetablemultiple";
        private const string SASLegacy_Type = "saslegacy";
        private const string SASEntityConnection_Type = "sasentityconnection";
        private const string AWMSLegacy_Type = "awmslegacy";
        private const string AWMSEntity_Type = "awmsentity";
        private const string Timetable_Context = "Models.WebAPIModel";
        private const string People_Context = "Models.WebAPIModel";
        private const string SchoolInfo_Context = "Models.WebAPIModel";
      
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // New code:
            config.Formatters.JsonFormatter.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
/*            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling =
                Newtonsoft.Json.PreserveReferencesHandling.Objects;
 */
            
            //Depend Injection
            var container = new UnityContainer();
            //Add Interception
            container.AddNewExtension<Interception>();
            //
            container.Configure<Interception>()
                .AddPolicy("logging")
                .AddMatchingRule<NamespaceMatchingRule>(
               new InjectionConstructor(
                    "MyFirstWebAPI.Models.IProductRepository", true))
                .AddCallHandler<LogCallHandler>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                 9001, true, true,
                 "start", "finish", true, false, true, 10, 1));
            
            container.Configure<Interception>()
             .AddPolicy("logging_seralize_object")
             .AddMatchingRule<NamespaceMatchingRule>(
            new InjectionConstructor(
                 "MyFirstWebAPI.Models.IProductRepository", true))
             .AddCallHandler<LoggingCallHandler>(
             new ContainerControlledLifetimeManager(),
             new InjectionConstructor(),
                 new InjectionProperty("Order", 1));

            //Register a SAS Single Connection, with no context provide, Will use the parameterOverride to finialize the it when resolving.
            container.RegisterType<IDBConnection,SingleEntityConnectionHelper>(SASEntityConnection_Type,
                 new InjectionConstructor(typeof(string), AppSettingsConstant.AdmineModule)
                );

            //check xml if we need multi timetable db connection
            Object isMultipleTimetable = StringAdditions.ConvertToBoolean
                ((AppConfigXMLParser.getAttributeValue(AppSettingsConstant.TimeTableModule,AppSettingsConstant.IsTimeTableMultiply)));
            if(isMultipleTimetable != null)
            {
                if((Boolean)isMultipleTimetable)
                {
                    var peopleconnection = container.Resolve<IDBConnection>(SASEntityConnection_Type,new ParameterOverride("context",People_Context));
                    var schoolconnection = container.Resolve<IDBConnection>(SASEntityConnection_Type, new ParameterOverride("context", SchoolInfo_Context));
                    container.RegisterType<IPeopleRepository, PeopleRepository>(new HierarchicalLifetimeManager(), new InjectionConstructor(peopleconnection));
                    container.RegisterType<ISchoolInfoRepository, SchoolInfoRepository>(new HierarchicalLifetimeManager(),new InjectionConstructor(schoolconnection));

                    container.RegisterType<IDBConnection, MultiplyEntityConnectionHelper>(TimetableMultiple_Type, new InjectionConstructor(
                        container.Resolve<PeopleRepository>(),
                        container.Resolve<ISchoolInfoRepository>()
                        ));
                    var timetableMultiConnection = container.Resolve<IDBConnection>(TimetableMultiple_Type);
                    container.RegisterType<IProductRepository, ProductMultipleRepository>(new HierarchicalLifetimeManager(), new InjectionConstructor(timetableMultiConnection));
                }
                else
                {

                    if (Equals(AppConfigXMLParser.getAttributeValue(AppSettingsConstant.TimeTableModule, AppSettingsConstant.ModuleSource),
                        AppSettingsConstant.APLUS)) //aplus
                    {
                        container.RegisterType<IDBConnection, SingleEntityConnectionHelper>(TimetableSingle_Type, 
                            new InjectionConstructor(Timetable_Context,AppSettingsConstant.TimeTableModule));
                        var timetablesingleconnection = container.Resolve<IDBConnection>(TimetableSingle_Type);
                        container.RegisterType<IProductRepository, ProductRepository>(new HierarchicalLifetimeManager(),new InjectionConstructor(timetablesingleconnection),
                            new InterceptionBehavior<PolicyInjectionBehavior>(),
                            new Interceptor<InterfaceInterceptor>());
                    }    
                    else
                    {
                        container.RegisterType<IDBConnection, SQLConnectionHelper>(Timetablelegacy_Type, new InjectionConstructor(AppSettingsConstant.TimeTableModule));
                    }
                }
            }
            else
            {
                throw new Exception("Dependence Injection error");
            }
       
            container.RegisterType<AdminController>(
                new InjectionConstructor(
                    container.Resolve<IProductRepository>()
                    ,"admin")
            );
            config.DependencyResolver = new UnityResolver(container);
            IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
            Logger.SetLogWriter(new LogWriterFactory().Create());
        }
    }
}
