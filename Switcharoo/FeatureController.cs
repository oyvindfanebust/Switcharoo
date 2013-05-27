using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Switcharoo
{
    public class FeaturesController : ApiController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(FeatureSwitch featureSwitch)
        {
            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}