using System;
using Switcharoo.Entities;

namespace Switcharoo.Commands
{
    public class ActivateFeature : Command
    {
        private readonly Guid _featureId;
        private readonly string _environment;

        public ActivateFeature(Guid featureId)
            : this(featureId, null)
        {
        }

        public ActivateFeature(Guid featureId, string environment)
        {
            _featureId = featureId;
            _environment = environment;
        }

        public override void Execute()
        {
            var feature = Session.Load<Feature>(_featureId);
            if (_environment != null)
            {
                feature.Activate(_environment);
            }
            else
            {
                feature.Activate();
            }
        }
    }
}