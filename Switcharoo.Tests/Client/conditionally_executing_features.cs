using System;
using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;

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

    public interface IConfigureFeatureSwitches
    {
        void Configure<TFeatureSwitch>(Uri uri) where TFeatureSwitch : IFeatureSwitch;
        Uri Get<TFeatureSwitch>();
    }

    public class FeatureSwitchConfiguration : IConfigureFeatureSwitches
    {
        private readonly Dictionary<Type, Uri> _switches = new Dictionary<Type, Uri>();
        public void Configure<TFeatureSwitch>(Uri uri) where TFeatureSwitch : IFeatureSwitch
        {
            _switches[typeof(TFeatureSwitch)] = uri;
        }

        public Uri Get<TFeatureSwitch>()
        {
            var type = typeof(TFeatureSwitch);
            if(!_switches.ContainsKey(type))
                throw new FeatureNotConfiguredException(type);

            return _switches[type];
        }
    }

    public class FeatureNotConfiguredException : Exception
    {
        public FeatureNotConfiguredException(Type featureType)
            : base(string.Format(@"Feature ""{0}"" is not configured.", featureType.Name))
        {
        }
    }

    public class FeatureA : IFeatureSwitch
    {
    }

    public interface IFeatureSwitch
    {
    }

    public class SwitcharooClient
    {
        private readonly ILookupFeatureSwitches _featureSwichLookup;
        private readonly IConfigureFeatureSwitches _configuration;

        public SwitcharooClient(ILookupFeatureSwitches featureSwichLookup, IConfigureFeatureSwitches configuration)
        {
            _featureSwichLookup = featureSwichLookup;
            _configuration = configuration;
        }

        public void For<TFeatureSwitch>(Action action) where TFeatureSwitch : IFeatureSwitch
        {
            var uri = _configuration.Get<TFeatureSwitch>();
            var isActive = _featureSwichLookup.IsActive(uri);
            if (isActive)
            {
                action();
            }
        }
    }

    public interface ILookupFeatureSwitches
    {
        bool IsActive(Uri featureUri);
    }
}
