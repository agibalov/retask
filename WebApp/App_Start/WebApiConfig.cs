using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Infrastructure;

namespace WebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{action}",
                defaults: new { controller = "RetaskApi" }
            );

            config.Services.Replace(typeof(IDocumentationProvider), new AttributesDocumentationProvider());
        }
    }
}
