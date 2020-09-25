using System.Web.Http;
using System.Web.Http.Cors;
using WebApiContrib.Formatting.Jsonp;

namespace WebApiLayer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var cors = new EnableCorsAttribute("http://127.0.0.1:8080/api/employee", "*", "*");
            config.EnableCors(cors);

            var jsonpFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            config.Formatters.Add(jsonpFormatter);

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
