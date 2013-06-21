using System.Collections.Generic;
using System.Linq;

namespace Switcharoo.Entities
{
    public class InstanceData : ISatisfyConditions
    {
        private readonly Dictionary<string, IEnumerable<string>> _data = new Dictionary<string, IEnumerable<string>>();

        public void Add(string key, string value)
        {
            _data.Add(key, new[] { value });
        }

        public bool IsSatisfied(ICondition condition)
        {
            return _data.ContainsKey(condition.Key) && _data[condition.Key].Any(condition.IsMatch);
        }
    }
}