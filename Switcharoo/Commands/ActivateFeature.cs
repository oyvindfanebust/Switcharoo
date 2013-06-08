using System;
using Switcharoo.Entities;

namespace Switcharoo.Commands
{
    public class ActivateFeature : Command
    {
        private readonly Guid _featureId;

        public ActivateFeature(Guid featureId)
        {
            _featureId = featureId;
        }

        public override void Execute()
        {
            var feature = Session.Load<Feature>(_featureId);
            feature.Activate();
        }
    }
}