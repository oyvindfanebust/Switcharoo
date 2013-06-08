using System;
using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;

namespace Switcharoo.Tests.Client
{
    [TestFixture]
    public class conditionally_executing_features : FeatureSpec
    {
        //Semantic model vs. DSL

        //Not found        
        //Active env
        //Not active env

        [Test]
        public void should_not_execute_feature_when_feature_is_configured_and_not_active()
        {
            var featureUri = new Uri("http://localhost:1337/features/08FEB265-207D-4840-96B2-018A70CAC74A");
            var someBool = false;

            var fakeLookup = A.Fake<ILookupFeatureSwitches>();
            A.CallTo(() => fakeLookup.IsActive(featureUri)).Returns(false);

            var switcharoo = new SwitcharooClient(fakeLookup);
            switcharoo.Configure<FeatureA>(featureUri);

            switcharoo.WhenActive<FeatureA>(() => someBool = true);

            Assert.That(someBool, Is.False);
        }

        [Test]
        public void should_execute_feature_when_feature_is_configured_and_active()
        {
            var featureUri = new Uri("http://localhost:1337/features/08FEB265-207D-4840-96B2-018A70CAC74A");
            var someBool = false;

            var fakeLookup = A.Fake<ILookupFeatureSwitches>();
            A.CallTo(() => fakeLookup.IsActive(featureUri)).Returns(true);

            var switcharoo = new SwitcharooClient(fakeLookup);
            switcharoo.Configure<FeatureA>(featureUri);

            switcharoo.WhenActive<FeatureA>(() => someBool = true);

            Assert.That(someBool, Is.True);
        }

        [Test]
        public void should_throw_when_feature_is_not_configured()
        {
            var fakeLookup = A.Fake<ILookupFeatureSwitches>();

            var switcharoo = new SwitcharooClient(fakeLookup);

            var exception = Assert.Throws<FeatureNotConfiguredException>(() => switcharoo.WhenActive<FeatureA>(() => { }));
            Assert.That(exception.Message, Is.StringContaining(typeof(FeatureA).Name));
        }

        [Test]
        public void should_overwrite_feature_uri_when_calling_configuring_feature_a_second_time()
        {
            var featureUriA = A.Fake<Uri>();
            var featureUriB = A.Fake<Uri>();

            var lookup = A.Fake<ILookupFeatureSwitches>();

            var switcharoo = new SwitcharooClient(lookup);
            switcharoo.Configure<FeatureA>(featureUriA);
            switcharoo.Configure<FeatureA>(featureUriB);

            switcharoo.WhenActive<FeatureA>(() => {});

            A.CallTo(() => lookup.IsActive(featureUriB)).MustHaveHappened();
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
        private readonly Dictionary<Type, Uri> _switches = new Dictionary<Type, Uri>();
        public SwitcharooClient(ILookupFeatureSwitches featureSwichLookup)
        {
            _featureSwichLookup = featureSwichLookup;
        }

        public void Configure<TFeatureSwitch>(Uri featureUri) where TFeatureSwitch : IFeatureSwitch
        {
            var featureType = typeof(TFeatureSwitch);
            _switches[featureType] = featureUri;
        }

        public void WhenActive<TFeatureSwitch>(Action action) where TFeatureSwitch : IFeatureSwitch
        {
            var featureType = typeof(TFeatureSwitch);
            if(!_switches.ContainsKey(featureType))
                throw new FeatureNotConfiguredException(featureType);
            var uri = _switches[featureType];
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
