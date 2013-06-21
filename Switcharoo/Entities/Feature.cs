using System;
using System.Collections.Generic;
using System.Linq;

namespace Switcharoo.Entities
{
    public class Feature
    {
        public Feature(Guid id, DateTime addedOn, string name)
        {
            Id = id;
            AddedOn = addedOn;
            Name = name;
        }

        public Guid Id { get; private set; }
        public DateTime AddedOn { get; private set; }
        public string Name { get; private set; }

        private readonly List<Switch> _switches = new List<Switch>(); 

        public bool IsActiveFor(ISatisfyConditions context)
        {
            return _switches.Any(s => s.Matches(context));
        }

        public void AddSwitch(Switch @switch)
        {
            _switches.Add(@switch);
        }
    }
}