using System;
using Should;
using Xunit;
using Switcharoo.Commands;
using Switcharoo.Entities;

namespace Switcharoo.Tests
{

    public class activating_a_feature : SwitcharooSpec
    {
        [Fact]
        public void feature_is_active_when_no_environment_is_specified()
        {
            var featureId = Guid.NewGuid();
            var command = New.CreateFeature(featureId);
            Execute(command);

            var activateFeature = new ActivateFeature(featureId);
            Execute(activateFeature);

            var feature = Load<Feature>(featureId);

            feature.IsActive.ShouldBeTrue();
        }

        [Fact]
        public void feature_is_active_only_in_activated_environment()
        {
            var featureId = Guid.NewGuid();
            var command = New.CreateFeatureWithEnvironments(featureId);
            Execute(command);

            var activateFeature = new ActivateFeature(featureId, Environments.Dev);
            Execute(activateFeature);

            var feature = Load<Feature>(featureId);

            feature.IsActiveIn(Environments.Dev).ShouldBeTrue();
            feature.IsActiveIn(Environments.Test).ShouldBeFalse();
            feature.IsActiveIn(Environments.Prod).ShouldBeFalse();
        }

        [Fact]
        public void can_activate_feature_for_all_environments()
        {
            var featureId = Guid.NewGuid();
            Execute(New.CreateFeatureWithEnvironments(featureId));

            Execute(new ActivateFeature(featureId));

            var feature = Load<Feature>(featureId);

            feature.IsActiveIn(Environments.Dev).ShouldBeTrue();
            feature.IsActiveIn(Environments.Test).ShouldBeTrue();
            feature.IsActiveIn(Environments.Prod).ShouldBeTrue();
        }

        [Fact]
        public void feature_is_active_if_all_environments_are_active()
        {
            var featureId = Guid.NewGuid();
            Execute(New.CreateFeatureWithEnvironments(featureId));

            Execute(new ActivateFeature(featureId, Environments.Test));
            Execute(new ActivateFeature(featureId, Environments.Dev));
            Execute(new ActivateFeature(featureId, Environments.Prod));

            var feature = Load<Feature>(featureId);

           feature.IsActive.ShouldBeTrue();
        }

        [Fact]
        public void feature_is_not_active_if_some_environments_are_not_active()
        {
            var featureId = Guid.NewGuid();
            Execute(New.CreateFeatureWithEnvironments(featureId));

            Execute(new ActivateFeature(featureId, Environments.Test));

            var feature = Load<Feature>(featureId);

            feature.IsActive.ShouldBeFalse();
        }

        [Fact]
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