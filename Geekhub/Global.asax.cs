﻿using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using Deldysoft.Foundation;

namespace Geekhub
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            var assembly = Assembly.GetExecutingAssembly();
            Bootstrapper.Configure(x =>
                x.WithContainer(y => 
                    y.WithDefaultBindings(assembly)
                     .WithMvcBindings(assembly)
                     .WithModuleBindings(assembly)
                     .WithCommandHandlers(assembly))
            );
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);   
            

        }
    }
}