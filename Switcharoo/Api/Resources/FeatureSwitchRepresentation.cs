using System;
using WebApi.Hal;

namespace Switcharoo.Api.Resources
{
    public class FeatureSwitchRepresentation : Representation
    {
        public FeatureSwitchRepresentation(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        protected override void CreateHypermedia()
        {
            var selfLink = LinkTemplates.Features.Feature.CreateLink(id => Id);
            Href = selfLink.Href;
            Rel = selfLink.Rel;
        }
    }
}