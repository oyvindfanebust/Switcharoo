using System;
using FakeItEasy;
using NUnit.Framework;
using Switcharoo.Client;

namespace Switcharoo.Tests.Client
{
    [TestFixture]
    public class conditionally_executing_features
    {
        //Active in env
        //Not active in env

        [Test]
        public void should_not_execute_feature_when_feature_is_inactive()
        {
            var featureUri = new Uri("http://localhost:1337/features/08FEB265-207D-4840-96B2-018A70CAC74A");
            var someBool = false;

            var lookup = A.Fake<ILookupFeatureSwitches>();
            A.CallTo(() => lookup.IsActive(featureUri)).Returns(false);

            var config = A.Fake<IConfigureFeatureSwitches>();
            A.CallTo(() => config.Get<FeatureA>()).Returns(featureUri);

            var switcharoo = new SwitcharooClient(lookup, config);

            switcharoo.For<FeatureA>(() => someBool = true);

            Assert.That(someBool, Is.False);
        }

        [Test]
        public void should_execute_feature_when_feature_is_active()
        {
            var featureUri = new Uri("http://localhost:1337/features/08FEB265-207D-4840-96B2-018A70CAC74A");
            var someBool = false;

            var lookup = A.Fake<ILookupFeatureSwitches>();
            A.CallTo(() => lookup.IsActive(featureUri)).Returns(true);

            var config = A.Fake<IConfigureFeatureSwitches>();
            A.CallTo(() => config.Get<FeatureA>()).Returns(featureUri);

            var switcharoo = new SwitcharooClient(lookup, config);

            switcharoo.For<FeatureA>(() => someBool = true);

            Assert.That(someBool, Is.True);
        }
    }
}
