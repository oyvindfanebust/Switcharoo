using System;
using Should;
using Switcharoo.Entities;
using Xunit;

namespace Switcharoo.Tests
{
    public class switching_features : SwitcharooSpec
    {
        [Fact]
        public void feature_with_no_switches_is_not_active()
        {
            var feature = new Feature(Guid.NewGuid(), DateTime.Now, "Feature A");
            
            var isActive = feature.IsActiveFor(new InstanceData());

            isActive.ShouldBeFalse();
        }

        [Fact]
        public void feature_with_single_switch_is_active_when_instance_matches()
        {
            var feature = new Feature(Guid.NewGuid(), DateTime.Now, "Feature A");
            
            var @switch = new Switch("Dev");
            var condition = new SimpleCondition("environment", "dev");
            @switch.AddCondition(condition);
            feature.AddSwitch(@switch);
            
            var instanceData = new InstanceData();
            instanceData.Add("environment", "dev");
            
            var isActive = feature.IsActiveFor(instanceData);

            isActive.ShouldBeTrue();
        }

        [Fact]
        public void feature_with_single_switch_is_not_active_when_instance_does_not_matches()
        {
            var feature = new Feature(Guid.NewGuid(), DateTime.Now, "Feature A");
            
            var @switch = new Switch("Dev");
            var condition = new SimpleCondition("environment", "dev");
            @switch.AddCondition(condition);
            feature.AddSwitch(@switch);
            
            var instanceData = new InstanceData();
            instanceData.Add("environment", "prod");
            
            var isActive = feature.IsActiveFor(instanceData);

            isActive.ShouldBeFalse();
        }

        [Fact]
        public void feature_with_multiple_switches_is_active_when_instance_matches_at_least_one_switch()
        {
            var feature = new Feature(Guid.NewGuid(), DateTime.Now, "Feature A");
            
            var @switch = new Switch("Dev");
            var condition = new SimpleCondition("environment", "dev");
            @switch.AddCondition(condition);
            feature.AddSwitch(@switch);
            
            var @switch2 = new Switch("Test");
            var condition2 = new SimpleCondition("environment", "test");
            @switch.AddCondition(condition2);
            feature.AddSwitch(@switch2);
            
            var instanceData = new InstanceData();
            instanceData.Add("environment", "test");
            
            var isActive = feature.IsActiveFor(instanceData);

            isActive.ShouldBeTrue();
        }
    }
}