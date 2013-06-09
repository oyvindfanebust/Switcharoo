using System;

namespace Switcharoo.Client
{
    public class SwitcharooClient
    {
        private readonly ILookupFeatureSwitches _featureSwichLookup;
        private readonly IConfigureFeatureSwitches _configuration;

        public SwitcharooClient(ILookupFeatureSwitches featureSwichLookup, IConfigureFeatureSwitches configuration)
        {
            _featureSwichLookup = featureSwichLookup;
            _configuration = configuration;
        }

        public void For<TFeatureSwitch>(Action activeAction, Action inactiveAction = null) where TFeatureSwitch : IFeatureSwitch
        {
            var uri = _configuration.Get<TFeatureSwitch>();
            var isActive = _featureSwichLookup.IsActive(uri);
            if (isActive)
            {
                activeAction();
            }
            else if (inactiveAction != null)
            {
                inactiveAction();
            }
        }
    }
}