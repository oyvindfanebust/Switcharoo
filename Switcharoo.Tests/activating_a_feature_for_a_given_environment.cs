using System;
using NUnit.Framework;
using Switcharoo.Commands;
using Switcharoo.Entities;

namespace Switcharoo.Tests
{
    [TestFixture]
    public class activating_a_feature_for_a_given_environment : FeatureSpec
    {
        [Test]
        public void feature_is_active_only_in_activated_environment()
        {
            var featureId = Guid.NewGuid();
            var command = New.CreateFeatureWithEnvironments(featureId);
            Execute(command);

            var activateFeature = new ActivateFeature(featureId, Environments.Dev);
            Execute(activateFeature);

            var feature = Load<Feature>(featureId);

            Assert.That(feature.IsActiveIn(Environments.Dev), Is.True);
            Assert.That(feature.IsActiveIn(Environments.Test), Is.False);
            Assert.That(feature.IsActiveIn(Environments.Prod), Is.False);
        }

        [Test]
        public void throws_if_trying_to_activate_non_existing_environment()
        {
            var featureId = Guid.NewGuid();
            var command = New.CreateFeatureWithEnvironments(featureId);
            Execute(command);

            var activateFeature = new ActivateFeature(featureId, "IDontExist");
            Assert.Throws<EnvironmentNotFoundException>(() => Execute(activateFeature));
        }
    }
}