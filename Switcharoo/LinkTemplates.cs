using WebApi.Hal;

namespace Switcharoo
{
    public static class LinkTemplates
    {
        public static class Features
        {
            public static Link Feature { get { return new Link("feature", "features/{id}"); } }
        }
    }
}