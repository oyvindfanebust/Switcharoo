namespace Switcharoo
{
    public class FeatureSwitch
    {
        public string Name { get; private set; }

        public FeatureSwitch(string name)
        {
            Name = name;
        }
    }
}