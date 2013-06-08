using System;
using Switcharoo.Commands;

namespace Switcharoo.Tests
{
    public static class New
    {
        public static CreateFeature CreateFeature(Guid id)
        {
            const string featureName = "Feature A";
            return new CreateFeature(id, featureName);
        }

        public static Command CreateFeatureWithEnvironments(Guid id)
        {
            const string featureName = "Feature A";
            return new CreateFeature(id, featureName, new[] { Environments.Dev, Environments.Test, Environments.Prod });
        }
    }
}