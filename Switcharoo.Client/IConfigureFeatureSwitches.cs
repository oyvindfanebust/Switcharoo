using System;

namespace Switcharoo.Client
{
    public interface IConfigureFeatureSwitches
    {
        void Configure<TFeatureSwitch>(Uri uri) where TFeatureSwitch : IFeatureSwitch;
        Uri Get<TFeatureSwitch>();
    }
}