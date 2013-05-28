using System.Web.Http;
using Newtonsoft.Json.Serialization;
using WebApi.Hal;

namespace Switcharoo
{
    public class Bootstrap
    {
        public void Configure(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "API Default",
                routeTemplate: "{controller}/{id}",
                defaults: new
                {
                    controller = "Features",
                    id = RouteParameter.Optional
                });

            config.Formatters.Add(new JsonHalMediaTypeFormatter
                {
                    SerializerSettings = { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                });
            config.Formatters.Add(new XmlHalMediaTypeFormatter());
        }
    }
}