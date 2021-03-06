﻿using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);
           // CasesBL.CaseChose(147, 1, "https://www.google.com/m8/feeds/contacts/default/thin?alt=json&access_token=ya29.A0AfH6SMCNDEjvRuHb2DXwzTXknZKMdsFuGpxd-EeVOnysHEkulwjitjNBZGfB9rIgOxhhCV21ng1BIxp2aHPe3utuuqb_50eZiEsIByWfooDQPWznDOxq2KQJZfdI_qbyyWV1pIloTByCHgwTeOdjyCM8qOMxDA&max-results=500&v=3.0");

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.EnableCors();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
