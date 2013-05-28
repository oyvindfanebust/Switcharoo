using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Switcharoo.Api.Resources;

namespace Switcharoo.Api
{
    public class FeaturesController : ApiController
    {
        private static readonly List<FeatureSwitch> FeatureSwitches = new List<FeatureSwitch>(); 
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Get(Guid id)
        {
            var featureSwitch = FeatureSwitches.Single(fs => fs.Id == id);
            return Request.CreateResponse(HttpStatusCode.OK, new FeatureSwitchRepresentation
                {
                    Id = featureSwitch.Id,
                    Name = featureSwitch.Name
                });
        }

        public HttpResponseMessage Post(FeatureSwitchRepresentation input)
        {
            var featureSwitch = new FeatureSwitch(Guid.NewGuid(), input.Name);
            FeatureSwitches.Add(featureSwitch);
            return new HttpResponseMessage(HttpStatusCode.Created)
            {
                Headers = { Location = LinkTemplates.Features.Feature.CreateUri(id => featureSwitch.Id)}
            };
        }
    }
}