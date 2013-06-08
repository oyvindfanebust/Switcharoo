using System;
using NUnit.Framework;
using Raven.Abstractions;
using Switcharoo.Commands;
using Switcharoo.Entities;

namespace Switcharoo.Tests
{
    [TestFixture]
    public class creating_a_feature_with_environments : FeatureSpec
    {
        [Test]
        public void can_load_created_feature()
        {
            var featureId = Guid.NewGuid();
            const string featureName = "Feature A";
            var currentTime = new DateTime(2013, 6, 8);
            var environments = new[]{ "Dev", "Test", "Prod"};
            var command = new CreateFeature(featureId, featureName, environments);
            SystemTime.UtcDateTime = () => currentTime;

            Execute(command);
            
            var feature = Load<Feature>(featureId);

            Assert.That(feature, Is.Not.Null);
            Assert.That(feature.Environments, Is.EqualTo(environments));
        }
    }
}