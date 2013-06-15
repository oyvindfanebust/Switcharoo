using System;
using FakeItEasy;
using Should;
using Xunit;
using Switcharoo.Client;

namespace Switcharoo.Tests.Client
{
    public class conditionally_executing_feature_action
    {
        private static readonly Uri FeatureUri = new Uri("http://localhost:1337/features/08FEB265-207D-4840-96B2-018A70CAC74A");
        private readonly ILookupFeatureSwitches lookup;
        private readonly IConfigureFeatureSwitches config;
        private readonly SwitcharooClient switcharoo;

        public conditionally_executing_feature_action()
        {
            lookup = A.Fake<ILookupFeatureSwitches>();
            config = A.Fake<IConfigureFeatureSwitches>();
            A.CallTo(() => config.Get<FeatureA>()).Returns(FeatureUri);
            switcharoo = new SwitcharooClient(lookup, config);
        }

        [Fact]
        public void should_not_execute_when_feature_is_inactive()
        {
            var called = false;

            A.CallTo(() => lookup.IsActive(FeatureUri)).Returns(false);

            switcharoo.For<FeatureA>(() => called = true);

            called.ShouldBeFalse();
        }

        [Fact]
        public void should_execute_when_feature_is_active()
        {
            var called = false;

            A.CallTo(() => lookup.IsActive(FeatureUri)).Returns(true);

            switcharoo.For<FeatureA>(() => called = true);

            called.ShouldBeTrue();
        }

        [Fact]
        public void should_only_execute_inactive_action_when_feature_is_inactive()
        {
            var activeCalled = false;
            var inactiveCalled = false;

            A.CallTo(() => lookup.IsActive(FeatureUri)).Returns(false);

            switcharoo.For<FeatureA>(() => activeCalled = true, () => inactiveCalled = true);

            activeCalled.ShouldBeFalse();
            inactiveCalled.ShouldBeTrue();
        }

        [Fact]
        public void should_only_execute_active_action_when_feature_is_active()
        {
            var activeCalled = false;
            var inactiveCalled = false;

            A.CallTo(() => lookup.IsActive(FeatureUri)).Returns(true);

            switcharoo.For<FeatureA>(() => activeCalled = true, () => inactiveCalled = true);

            activeCalled.ShouldBeTrue();
            inactiveCalled.ShouldBeFalse();
        }
    }


    public class conditionally_executing_feature_func
    {
        private static readonly Uri featureUri = new Uri("http://localhost:1337/features/08FEB265-207D-4840-96B2-018A70CAC74A");
        private readonly ILookupFeatureSwitches lookup;
        private readonly IConfigureFeatureSwitches config;
        private readonly SwitcharooClient switcharoo;

        public conditionally_executing_feature_func()
        {
            lookup = A.Fake<ILookupFeatureSwitches>();
            config = A.Fake<IConfigureFeatureSwitches>();
            A.CallTo(() => config.Get<FeatureA>()).Returns(featureUri);
            switcharoo = new SwitcharooClient(lookup, config);
        }

        [Fact]
        public void calling_with_single_func_should_return_default_when_feature_is_inactive()
        {
            A.CallTo(() => lookup.IsActive(featureUri)).Returns(false);

            var result = switcharoo.For<FeatureA, string>(() => "called");

            result.ShouldBeNull();        }

        [Fact]
        public void calling_with_two_funcs_should_return_inactive_result_when_feature_is_inactive()
        {
            A.CallTo(() => lookup.IsActive(featureUri)).Returns(false);

            var result = switcharoo.For<FeatureA, string>(() => "active", () => "inactive");

            result.ShouldEqual("inactive");
        }

        [Fact]
        public void calling_with_single_func_should_return_result_when_feature_is_active()
        {
            A.CallTo(() => lookup.IsActive(featureUri)).Returns(true);

            var result = switcharoo.For<FeatureA, string>(() => "called");

            result.ShouldEqual("called");
        }

        [Fact]
        public void calling_with_two_funcs_should_return_active_result_when_feature_is_active()
        {
            A.CallTo(() => lookup.IsActive(featureUri)).Returns(true);

            var result = switcharoo.For<FeatureA, string>(() => "active", () => "inactive");

            result.ShouldEqual("active");
        }
    }
}
