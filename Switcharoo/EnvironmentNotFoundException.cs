using System;

namespace Switcharoo
{
    public class EnvironmentNotFoundException : Exception
    {
        public EnvironmentNotFoundException(string feature, string environment) : base(string.Format("Environment {0} does not exist for feature {1}", environment, feature))
        {
            
        }
    }
}