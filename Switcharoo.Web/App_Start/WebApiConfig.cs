using System.Web.Http;

namespace Switcharoo.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            new Bootstrap().Configure(config);
        }
    }
}
