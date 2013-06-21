using System.Collections.Generic;

namespace Switcharoo.Entities
{
    public class MultiCondition : ICondition
    {
        public string Key { get; private set; }
        private readonly HashSet<string> _satisfiers = new HashSet<string>();

        public MultiCondition(string key)
        {
            Key = key;
        }

        public void AddMatchingCondition(string condition)
        {
            _satisfiers.Add(condition);
        }

        public bool IsMatch(string condition)
        {
            return _satisfiers.Contains(condition);
        }
    }
}