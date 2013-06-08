using System;
using NUnit.Framework;
using Raven.Abstractions;
using Switcharoo.Commands;
using Switcharoo.Entities;

namespace Switcharoo.Tests
{
    [TestFixture]
    public class when_creating_a_feature : FeatureSpec
    {
        [Test]
        public void can_load_created_feature()
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
    }
}