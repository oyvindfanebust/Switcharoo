using System;
using Should;
using Switcharoo.Entities;
using Xunit;

namespace Switcharoo.Tests
{
    public class checking_conditions : SwitcharooSpec
    {
        [Fact]
        public void feature_with_no_conditions_is_not_active()
        {
            var feature = new Feature(Guid.NewGuid(), DateTime.Now, "Feature A");
            var isActive = feature.IsActiveFor(new InstanceData());

            isActive.ShouldBeFalse();
        }

        [Fact]
        public void feature_with_single_condition_is_active_when_instance_matches()
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
        public void feature_with_single_condition_is_not_active_when_instance_does_not_matches()
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

        //No conditions -> true?
        //One of multiple conditions -> false?

        [Fact]
        public void feature_with_multiple_conditions_is_active_when_instance_matches_all_conditions()
        {
            var feature = new Feature(Guid.NewGuid(), DateTime.Now, "Feature A");
            var @switch = new Switch("Dev");
            var condition1 = new SimpleCondition("environment", "dev");
            var condition2 = new SimpleCondition("computer_name", "dev_01");
            @switch.AddCondition(condition1);
            @switch.AddCondition(condition2);
            feature.AddSwitch(@switch);
            var instanceData = new InstanceData();
            instanceData.Add("environment", "dev");
            instanceData.Add("computer_name", "dev_01");
            var isActive = feature.IsActiveFor(instanceData);

            isActive.ShouldBeTrue();
        }

    }
}