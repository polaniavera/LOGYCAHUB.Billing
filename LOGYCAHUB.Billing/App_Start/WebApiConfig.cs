namespace LOGYCAHUB.Billing
{
    using System.Web.Http;
    using ActionFilters;
    using Filters;
    using Autofac;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Registering Action Filter
            config.Filters.Add(new LoggingFilterAttribute());

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
