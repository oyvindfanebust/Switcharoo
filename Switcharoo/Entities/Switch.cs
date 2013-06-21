using System.Collections.Generic;
using System.Linq;

namespace Switcharoo.Entities
{
    public class Switch
    {
        public string Name { get; private set; }
        private readonly List<ICondition> _conditions = new List<ICondition>();
 
        public Switch(string name)
        {
            Name = name;
        }

        public void AddCondition(ICondition condition)
        {
            _conditions.Add(condition);
        }

        public bool Matches(ISatisfyConditions context)
        {
            return _conditions.All(context.IsSatisfied);
        }
    }
}