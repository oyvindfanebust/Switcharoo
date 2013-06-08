using System;
using Raven.Client;
using Switcharoo.Entities;

namespace Switcharoo.Commands
{
    public class ActivateFeature
    {
        private readonly Guid _featureId;

        public ActivateFeature(Guid featureId)
        {
            _featureId = featureId;
        }

        public IDocumentSession Session { private get; set; }

        public void Execute()
        {
            var feature = Session.Load<Feature>(_featureId);
            feature.Activate();
        }
    }
}