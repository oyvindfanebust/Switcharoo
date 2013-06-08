using System;
using NUnit.Framework;
using Switcharoo.Client;

namespace Switcharoo.Tests.Client
{
    [TestFixture]
    public class configuring_feature_switches
    {
        [Test]
        public void get_returns_uri_for_configured_feature_swich()
        {
            var featureUri = new Uri("http://localhost:1337/features/08FEB265-207D-4840-96B2-018A70CAC74A");
            var configuration = new FeatureSwitchConfiguration();
            configuration.Configure<FeatureA>(featureUri);

            var uri = configuration.Get<FeatureA>();

            Assert.That(uri, Is.EqualTo(featureUri));
        }

        [Test]
        public void get_throws_when_feature_is_not_configured()
        {
            var configuration = new FeatureSwitchConfiguration();
            var exception = Assert.Throws<FeatureNotConfiguredException>(() => configuration.Get<FeatureA>());
            Assert.That(exception.Message, Is.StringContaining(typeof(FeatureA).Name));
        }

        [Test]
        public void configuring_feature_twice_overwrites_first_feature()
        {
            var featureUri1 = new Uri("http://localhost:1337/features/08FEB265-207D-4840-96B2-018A70CAC74A");
            var featureUri2 = new Uri("http://localhost:1337/features/6AE913D0-D289-4533-ACD4-8B6DE3C72223");
            var configuration = new FeatureSwitchConfiguration();
            configuration.Configure<FeatureA>(featureUri1);
            configuration.Configure<FeatureA>(featureUri2);

            var uri = configuration.Get<FeatureA>();

            Assert.That(uri, Is.EqualTo(featureUri2));
        }
    }
}