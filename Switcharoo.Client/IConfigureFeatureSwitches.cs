using System;

namespace Switcharoo.Client
{
    public interface IConfigureFeatureSwitches
    {
        void Configure<TFeatureSwitch>(string uri) where TFeatureSwitch : IFeatureSwitch;
        Uri Get<TFeatureSwitch>();
    }
}