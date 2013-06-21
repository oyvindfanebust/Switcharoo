using Should;
using Switcharoo.Entities;
using Xunit;

namespace Switcharoo.Tests
{
    public class matching_a_switch_to_instance_data
    {
        [Fact]
        public void should_match_if_no_conditions()
        {
            var @switch = new Switch("switch a");
            var instanceData = new InstanceData();
            var matches = @switch.Matches(instanceData);

            matches.ShouldBeTrue();
        }

        [Fact]
        public void should_not_match_if_no_matching_element_in_instance_data()
        {
            var @switch = new Switch("switch a");
            @switch.AddCondition(new SimpleCondition("a", "1"));
            var instanceData = new InstanceData();
            var matches = @switch.Matches(instanceData);

            matches.ShouldBeFalse();
        }

        [Fact]
        public void single_condition_should_match_if_instance_contains_correct_value()
        {
            var @switch = new Switch("switch a");
            @switch.AddCondition(new SimpleCondition("a", "1"));
            var instanceData = new InstanceData();
            instanceData.Add("a", "1");
            var matches = @switch.Matches(instanceData);

            matches.ShouldBeTrue();
        }

        [Fact]
        public void single_condition_should_not_match_if_instance_data_does_not_contain_correct_value()
        {
            var @switch = new Switch("switch a");
            @switch.AddCondition(new SimpleCondition("a", "1"));
            var instanceData = new InstanceData();
            instanceData.Add("a", "2");
            var matches = @switch.Matches(instanceData);

            matches.ShouldBeFalse();
        }

        [Fact]
        public void multiple_conditions_should_match_if_instance_data_contains_correct_value_for_all_conditions()
        {
            var @switch = new Switch("switch a");
            @switch.AddCondition(new SimpleCondition("a", "1"));
            @switch.AddCondition(new SimpleCondition("b", "2"));
            var instanceData = new InstanceData();
            instanceData.Add("a", "1");
            instanceData.Add("b", "2");
            var matches = @switch.Matches(instanceData);

            matches.ShouldBeTrue();
        }

        [Fact]
        public void multiple_conditions_should_not_match_if_some_instance_data_is_missing()
        {
            var @switch = new Switch("switch a");
            @switch.AddCondition(new SimpleCondition("a", "1"));
            @switch.AddCondition(new SimpleCondition("b", "2"));
            var instanceData = new InstanceData();
            instanceData.Add("a", "1");
            var matches = @switch.Matches(instanceData);

            matches.ShouldBeFalse();
        }

        [Fact]
        public void multiple_conditions_should_no_match_if_instance_data_does_not_contain_correct_values_for_all_conditions()
        {
            var @switch = new Switch("switch a");
            @switch.AddCondition(new SimpleCondition("a", "1"));
            @switch.AddCondition(new SimpleCondition("b", "2"));
            var instanceData = new InstanceData();
            instanceData.Add("a", "1");
            instanceData.Add("b", "3");
            var matches = @switch.Matches(instanceData);

            matches.ShouldBeFalse();
        }
    }
}