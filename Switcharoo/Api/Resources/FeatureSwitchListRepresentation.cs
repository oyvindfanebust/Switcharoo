using System.Collections.Generic;
using WebApi.Hal;

namespace Switcharoo.Api.Resources
{
    public class FeatureSwitchListRepresentation : RepresentationList<FeatureSwitchRepresentation>
    {
        public FeatureSwitchListRepresentation(IList<FeatureSwitchRepresentation> res) : base(res)
        {
        }

        protected override void CreateHypermedia()
        {
            
        }
    }
}