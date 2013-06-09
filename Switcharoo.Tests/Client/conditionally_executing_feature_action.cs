using System;
using FakeItEasy;
using NUnit.Framework;
using Switcharoo.Client;

namespace Switcharoo.Tests.Client
{
    [TestFixture]
    public class conditionally_executing_feature_action
    {
        private static readonly Uri featureUri = new Uri("http://localhost:1337/features/08FEB265-207D-4840-96B2-018A70CAC74A");
        private ILookupFeatureSwitches lookup;
        private IConfigureFeatureSwitches config;
        private SwitcharooClient switcharoo;

        [SetUp]
        public void SetUp()
        {
            lookup = A.Fake<ILookupFeatureSwitches>();
            config = A.Fake<IConfigureFeatureSwitches>();
            A.CallTo(() => config.Get<FeatureA>()).Returns(featureUri);
            switcharoo = new SwitcharooClient(lookup, config);
        }

        [Test]
        public void should_not_execute_when_feature_is_inactive()
        {
            var called = false;

            A.CallTo(() => lookup.IsActive(featureUri)).Returns(false);

            switcharoo.For<FeatureA>(() => called = true);

            Assert.That(called, Is.False);
        }

        [Test]
        public void should_execute_when_feature_is_active()
        {
            var called = false;

            A.CallTo(() => lookup.IsActive(featureUri)).Returns(true);

            switcharoo.For<FeatureA>(() => called = true);

            Assert.That(called, Is.True);
        }

        [Test]
        public void should_only_execute_inactive_action_when_feature_is_inactive()
        {
            var activeCalled = false;
            var inactiveCalled = false;

            A.CallTo(() => lookup.IsActive(featureUri)).Returns(false);

            switcharoo.For<FeatureA>(() => activeCalled = true, () => inactiveCalled = true);

            Assert.That(activeCalled, Is.False);
            Assert.That(inactiveCalled, Is.True);
        }

        [Test]
        public void should_only_execute_active_action_when_feature_is_active()
        {
            var activeCalled = false;
            var inactiveCalled = false;

            A.CallTo(() => lookup.IsActive(featureUri)).Returns(true);

            switcharoo.For<FeatureA>(() => activeCalled = true, () => inactiveCalled = true);

            Assert.That(activeCalled, Is.True);
            Assert.That(inactiveCalled, Is.False);
        }
    }
}
