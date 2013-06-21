using System;

namespace Switcharoo.Client
{
    public interface IConfigureFeatureSwitches
    {
        void ConfigureFeature<TFeatureSwitch>(string uri) where TFeatureSwitch : IFeatureSwitch;
        Uri Get<TFeatureSwitch>();
    }
}