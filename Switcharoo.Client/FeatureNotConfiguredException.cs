using System;

namespace Switcharoo.Client
{
    public class FeatureNotConfiguredException : Exception
    {
        public FeatureNotConfiguredException(Type featureType)
            : base(string.Format(@"Feature ""{0}"" is not configured.", featureType.Name))
        {
        }
    }
}