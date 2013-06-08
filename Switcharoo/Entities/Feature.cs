using System;
using System.Collections.Generic;

namespace Switcharoo.Entities
{
    public class Feature
    {
        public Feature(Guid id, DateTime addedOn, string name, IList<string> environments)
        {
            Id = id;
            AddedOn = addedOn;
            Name = name;
            Environments = environments;
            ActiveEnvironments = new List<string>();
        }

        public Guid Id { get; private set; }
        public DateTime AddedOn { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }

        public IList<string> Environments { get; private set; }
        private IList<string> ActiveEnvironments { get; set; }

        public void Activate()
        {
            IsActive = true;
        }

        public void Activate(string environment)
        {
            if(!Environments.Contains(environment))
                throw new EnvironmentNotFoundException(Name, environment);
            
            ActiveEnvironments.Add(environment);
        }

        public bool IsActiveIn(string environment)
        {
            return ActiveEnvironments.Contains(environment);
        }
    }
}