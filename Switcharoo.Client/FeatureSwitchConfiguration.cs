using System;
using System.Collections.Generic;

namespace Switcharoo.Client
{
    public class FeatureSwitchConfiguration : IConfigureFeatureSwitches
    {
        private readonly Uri _baseUri;
        private readonly Dictionary<Type, Uri> _switches = new Dictionary<Type, Uri>();

        public FeatureSwitchConfiguration(string baseUri)
        {
            _baseUri = new Uri(baseUri);
        }

        public void Configure<TFeatureSwitch>(string relativeUri) where TFeatureSwitch : IFeatureSwitch
        {
            _switches[typeof(TFeatureSwitch)] = new Uri(_baseUri, relativeUri);
        }

        public Uri Get<TFeatureSwitch>()
        {
            var type = typeof(TFeatureSwitch);
            if(!_switches.ContainsKey(type))
                throw new FeatureNotConfiguredException(type);

            return _switches[type];
        }
    }
}