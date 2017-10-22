using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using BTRS_Server.Models.BTRSModel;
using BTRS_Base.Models.ContractModel;
using System.Web.Http.Cors;


namespace BTRS_Base
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Passenger>("Passengers");
            
            builder.EntitySet<Transaction>("Transactions");
            builder.EntitySet<Payment>("Payments");
            builder.EntitySet<Location>("Locations");
            builder.EntitySet<Fare>("Fares");
            builder.EntitySet<BusSchedule>("BusSchedules");
            builder.EntitySet<Bus>("Buses");
            builder.EntitySet<Contract>("Contracts");
           // builder.Entity<Transaction>().Collection.Action("M").Returns<int>();
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            EnableCorsAttribute attr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(attr);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
