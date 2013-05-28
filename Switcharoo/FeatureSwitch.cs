using System;

namespace Switcharoo
{
    public class FeatureSwitch
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public FeatureSwitch(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}