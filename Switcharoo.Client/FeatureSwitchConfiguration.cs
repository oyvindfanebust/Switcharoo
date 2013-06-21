using System;
using System.Collections.Generic;

namespace Switcharoo.Client
{
    public class FeatureSwitchConfiguration : IConfigureFeatureSwitches
    {
        private readonly Uri _baseUri;
        private readonly Dictionary<Type, Uri> _switches = new Dictionary<Type, Uri>();
        private readonly Dictionary<string, IEnumerable<string>> _instanceProperties = new Dictionary<string, IEnumerable<string>>();
        private readonly Dictionary<string, Func<IEnumerable<string>>> _instancePropertyGetters = new Dictionary<string, Func<IEnumerable<string>>>();

        public FeatureSwitchConfiguration(string baseUri)
        {
            _baseUri = new Uri(baseUri);
        }

        public void ConfigureFeature<TFeatureSwitch>(string relativeUri) where TFeatureSwitch : IFeatureSwitch
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

        public void ConfigureInstanceProperty(string name, Func<IEnumerable<string>> getter)
        {
            _instancePropertyGetters[name] = getter;
        }

        public void ConfigureInstanceProperty(string name, IEnumerable<string> values)
        {
            _instanceProperties[name] = values;
        }

        public IEnumerable<string> GetInstanceProperties(string name)
        {
            if (_instanceProperties.ContainsKey(name))
                return _instanceProperties[name];
            
            if (_instancePropertyGetters.ContainsKey(name))
                return _instancePropertyGetters[name]();

            throw new ArgumentException(string.Format("Instance property {0} is not configured", name), "name");
        }
    }
}