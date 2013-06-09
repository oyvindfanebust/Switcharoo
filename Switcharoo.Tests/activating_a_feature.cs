using System;
using NUnit.Framework;
using Switcharoo.Commands;
using Switcharoo.Entities;

namespace Switcharoo.Tests
{
    [TestFixture]
    public class activating_a_feature : SwitcharooSpec
    {
        [Test]
        public void feature_is_active_when_no_environment_is_specified()
        {
            var featureId = Guid.NewGuid();
            var command = New.CreateFeature(featureId);
            Execute(command);

            var activateFeature = new ActivateFeature(featureId);
            Execute(activateFeature);

            var feature = Load<Feature>(featureId);

            Assert.That(feature.IsActive, Is.True);
        }

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
        public void can_activate_feature_for_all_environments()
        {
            var featureId = Guid.NewGuid();
            Execute(New.CreateFeatureWithEnvironments(featureId));

            Execute(new ActivateFeature(featureId));

            var feature = Load<Feature>(featureId);

            Assert.That(feature.IsActiveIn(Environments.Dev), Is.True);
            Assert.That(feature.IsActiveIn(Environments.Test), Is.True);
            Assert.That(feature.IsActiveIn(Environments.Prod), Is.True);
        }

        [Test]
        public void feature_is_active_if_all_environments_are_active()
        {
            var featureId = Guid.NewGuid();
            Execute(New.CreateFeatureWithEnvironments(featureId));

            Execute(new ActivateFeature(featureId, Environments.Test));
            Execute(new ActivateFeature(featureId, Environments.Dev));
            Execute(new ActivateFeature(featureId, Environments.Prod));

            var feature = Load<Feature>(featureId);

           Assert.That(feature.IsActive, Is.True);
        }

        [Test]
        public void feature_is_not_active_if_some_environments_are_not_active()
        {
            var featureId = Guid.NewGuid();
            Execute(New.CreateFeatureWithEnvironments(featureId));

            Execute(new ActivateFeature(featureId, Environments.Test));

            var feature = Load<Feature>(featureId);

            Assert.That(feature.IsActive, Is.False);
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