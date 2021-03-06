﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TvCorporativa
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{empresa}/{action}/{idPonto}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
