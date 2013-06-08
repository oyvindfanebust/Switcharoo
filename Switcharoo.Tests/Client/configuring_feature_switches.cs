using System;
using NUnit.Framework;

namespace Switcharoo.Tests.Client
{
    [TestFixture]
    public class configuring_feature_switches
    {
        //overwrite existing?

        [Test]
        public void returns_uri_for_configured_feature_swich()
        {
            var featureUri = new Uri("http://localhost:1337/features/08FEB265-207D-4840-96B2-018A70CAC74A");
            var configuration = new FeatureSwitchConfiguration();
            configuration.Configure<FeatureA>(featureUri);

            var uri = configuration.Get<FeatureA>();

            Assert.That(uri, Is.EqualTo(featureUri));
        }

        [Test]
        public void get_should_throw_when_feature_is_not_configured()
        {
            var configuration = new FeatureSwitchConfiguration();
            var exception = Assert.Throws<FeatureNotConfiguredException>(() => configuration.Get<FeatureA>());
            Assert.That(exception.Message, Is.StringContaining(typeof(FeatureA).Name));
        }

    }
}