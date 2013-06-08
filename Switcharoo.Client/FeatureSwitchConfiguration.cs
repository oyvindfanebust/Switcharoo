using System;
using System.Collections.Generic;

namespace Switcharoo.Client
{
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
}