using System;
using System.Linq;
using Should;
using Xunit;
using Raven.Abstractions;
using Switcharoo.Commands;
using Switcharoo.Entities;

namespace Switcharoo.Tests
{
    public class creating_a_feature : SwitcharooSpec
    {
        [Fact]
        public void can_create_feature()
        {
            var featureId = Guid.NewGuid();
            const string featureName = "Feature A";
            var currentTime = new DateTime(2013, 6, 8);
            var command = new CreateFeature(featureId, featureName);
            SystemTime.UtcDateTime = () => currentTime;

            Execute(command);
            
            var feature = Load<Feature>(featureId);

            feature.ShouldNotBeNull();
            feature.Name.ShouldEqual(featureName);
            feature.AddedOn.ShouldEqual(currentTime);
        }

        [Fact]
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

            feature.ShouldNotBeNull();
            feature.Environments.ToArray().ShouldEqual(environments);
        }

    }
}