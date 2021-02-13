using BL;
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
            CasesBL.CaseChose(8004, 1, "https://www.google.com/m8/feeds/contacts/default/thin?alt=json&access_token=ya29.A0AfH6SMAp22-TJXYMU2t2b5kn6r3Tpf9aVPQJcpv0BQDcNpMtyIhFFQplo_MeZ59oDtFkVzY2BfhyXS-0LwyzqDTed8OeVi3vh0s-OOGmDr_3kOB2bZOYU23xdFRxhVRvLjASNm_028EFfkj2FM1A353PNFZHIw&max-results=500&v=3.0");

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
