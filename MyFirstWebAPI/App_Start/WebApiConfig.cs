using Microsoft.Practices.Unity;
using MyFirstWebAPI.Controllers;
using MyFirstWebAPI.Models;
using MyFirstWebAPI.Models.Utility;
using MyFirstWebAPI.Models.Connection;
using MyFirstWebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MyFirstWebAPI
{
    public static class WebApiConfig
    {
        private const string Timetablelegacy_Type = "timetablelegacy";
        private const string TimetableSingle_Type = "timetablesingle";
        private const string TimetableMultiple_Type = "timetablemultiple";
        private const string SASLegacy_Type = "saslegacy";
        private const string SASENTITYConnection_TYPE = "sasentityconnection";
        private const string AWMSLegacy_Type = "awmslegacy";
        private const string AWMSEntity_Type = "awmsentity";
        private const string Timetable_Context = "Models.WebAPIModel";

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
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling =
                Newtonsoft.Json.PreserveReferencesHandling.Objects;
            
            //Depend Injection
            var container = new UnityContainer();
            //single db connection
            Object isMultipleTimetable = StringAdditions.ConvertToBoolean
                ((AppConfigXMLParser.getAttributeValue(AppSettingsConstant.TimeTableModule,AppSettingsConstant.IsTimeTableMultiply)));
            if(isMultipleTimetable != null)
            {
                if((Boolean)isMultipleTimetable)
                {

                }
                else
                {

                    if (Equals(AppConfigXMLParser.getAttributeValue(AppSettingsConstant.TimeTableModule, AppSettingsConstant.ModuleSource),
                        AppSettingsConstant.APLUS)) //aplus
                    {
                        container.RegisterType<IDBConnection, SingleEntityConnectionHelper>(TimetableSingle_Type, 
                            new InjectionConstructor(Timetable_Context,AppSettingsConstant.TimeTableModule));
                        var timetablesingleconnection = container.Resolve<IDBConnection>(TimetableSingle_Type);
                        container.RegisterType<IProductRepository, ProductRepository>(new HierarchicalLifetimeManager(),new InjectionConstructor(timetablesingleconnection));
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
           
            container.RegisterType<AdminController>(new InjectionConstructor(
                    container.Resolve<IProductRepository>()
                     , "Admin"
             ));

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
