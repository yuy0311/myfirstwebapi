using Microsoft.Practices.Unity;
using MyFirstWebAPI.Controllers;
using MyFirstWebAPI.Models;
using MyFirstWebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MyFirstWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IProductRepository, ProductRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<AdminController>(new InjectionConstructor(
                container.Resolve<IProductRepository>()
                ,"Admin"
                ));
            config.DependencyResolver = new UnityResolver(container);

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
        }
    }
}
