using System;
using NUnit.Framework;
using Raven.Abstractions;
using Raven.Client.Embedded;
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
            const string featureName = "Feature A";
            var currentTime = new DateTime(2013, 6, 8);
            SystemTime.UtcDateTime = () => currentTime;
            var command = new CreateFeature(featureId, featureName);
            Execute(command);

            var activateFeature = new ActivateFeature(featureId);
            Execute(activateFeature);

            var feature = Load<Feature>(featureId);

            Assert.That(feature.IsActive, Is.True);
        }
    }
}