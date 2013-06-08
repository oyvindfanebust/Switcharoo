using System;
using NUnit.Framework;
using Raven.Abstractions;
using Switcharoo.Commands;
using Switcharoo.Entities;

namespace Switcharoo.Tests
{
    [TestFixture]
    public class creating_a_feature : FeatureSpec
    {
        [Test]
        public void can_create_feature()
        {
            var featureId = Guid.NewGuid();
            const string featureName = "Feature A";
            var currentTime = new DateTime(2013, 6, 8);
            var command = new CreateFeature(featureId, featureName);
            SystemTime.UtcDateTime = () => currentTime;

            Execute(command);
            
            var feature = Load<Feature>(featureId);

            Assert.That(feature, Is.Not.Null);
            Assert.That(feature.Name, Is.EqualTo(featureName));
            Assert.That(feature.AddedOn, Is.EqualTo(currentTime));
        }

        [Test]
        public void can_create_feature_with_multiple_environments()
        {
            var featureId = Guid.NewGuid();
            const string featureName = "Feature A";
            var currentTime = new DateTime(2013, 6, 8);
            var environments = new[] { "Dev", "Test", "Prod" };
            var command = new CreateFeature(featureId, featureName, environments);
            SystemTime.UtcDateTime = () => currentTime;

            Execute(command);

            var feature = Load<Feature>(featureId);

            Assert.That(feature, Is.Not.Null);
            Assert.That(feature.Environments, Is.EqualTo(environments));
        }

    }
}