﻿using System;
using System.Collections.Generic;
using System.Linq;
using Should;
using Xunit;
using Switcharoo.Client;

namespace Switcharoo.Tests.Client
{
    public class configuring_feature_switches
    {
        //Active in env
        //Not active in env

        [Fact]
        public void get_returns_uri_for_configured_feature_swich()
        {
            const string relativeUri = "/features/08FEB265-207D-4840-96B2-018A70CAC74A";
            var configuration = new FeatureSwitchConfiguration("http://localhost:1337/");

            configuration.ConfigureFeature<FeatureA>(relativeUri);

            var uri = configuration.Get<FeatureA>();

            uri.LocalPath.ShouldEqual(relativeUri);
        }

        [Fact]
        public void get_throws_when_feature_is_not_configured()
        {
            var configuration = new FeatureSwitchConfiguration("http://localhost:1337/");
            var exception = Assert.Throws<FeatureNotConfiguredException>(() => configuration.Get<FeatureA>());
            exception.Message.ShouldContain(typeof(FeatureA).Name);
        }

        [Fact]
        public void configuring_feature_twice_overwrites_first_feature()
        {
            const string relativeUri1 = "/features/08FEB265-207D-4840-96B2-018A70CAC74A";
            const string relativeUri2 = "/features/6AE913D0-D289-4533-ACD4-8B6DE3C72223";
            var configuration = new FeatureSwitchConfiguration("http://localhost:1337/");
            configuration.ConfigureFeature<FeatureA>(relativeUri1);
            configuration.ConfigureFeature<FeatureA>(relativeUri2);

            var uri = configuration.Get<FeatureA>();

            uri.LocalPath.ShouldEqual(relativeUri2);
        }

        [Fact]
        public void can_configure_static_instance_property()
        {
            var configuration = new FeatureSwitchConfiguration("http://localhost:1337/");
            configuration.ConfigureInstanceProperty("environment", new[] { "dev" });

            var property = configuration.GetInstanceProperties("environment").Single();

            property.ShouldEqual("dev");
        }

        private static string[] _environments;

        [Fact]
        public void can_configure_dynamic_instance_property()
        {
            _environments = new string[0];
            Func<IEnumerable<string>> func = () => _environments;
            var configuration = new FeatureSwitchConfiguration("http://localhost:1337/");
            configuration.ConfigureInstanceProperty("environment", func);
            _environments = new[] {"dev"};

            var property = configuration.GetInstanceProperties("environment").Single();

            property.ShouldEqual("dev");
        }
    }
}