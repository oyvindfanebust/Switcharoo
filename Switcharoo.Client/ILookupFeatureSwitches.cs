using System;

namespace Switcharoo.Client
{
    public interface ILookupFeatureSwitches
    {
        bool IsActive(Uri featureUri);
    }
}