using System;
using NUnit.Framework;
using Switcharoo.Commands;
using Switcharoo.Entities;

namespace Switcharoo.Tests
{
    [TestFixture]
    public class when_activating_a_feature : FeatureSpec
    {
        [Test]
        public void feature_is_active()
        {
            var featureId = Guid.NewGuid();
            var command = New.CreateFeature(featureId);
            Execute(command);

            var activateFeature = new ActivateFeature(featureId);
            Execute(activateFeature);

            var feature = Load<Feature>(featureId);

            Assert.That(feature.IsActive, Is.True);
        }
    }
}