namespace Switcharoo.Entities
{
    public class SimpleCondition : ICondition
    {
        public string Key { get; private set; }
        private readonly string _value;

        public SimpleCondition(string key, string value)
        {
            Key = key;
            _value = value;
        }

        public bool IsMatch(string condition)
        {
            return _value == condition;
        }
    }
}