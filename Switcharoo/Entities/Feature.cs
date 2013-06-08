using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Abstractions.Extensions;

namespace Switcharoo.Entities
{
    public class Feature
    {
        private bool _isActive;

        public Feature(Guid id, DateTime addedOn, string name, IEnumerable<string> environments)
        {
            Id = id;
            AddedOn = addedOn;
            Name = name;
            Environments = new HashSet<string>();
            if (environments != null)
                environments.ForEach(e => Environments.Add(e));
            ActiveEnvironments = new HashSet<string>();
        }

        public Guid Id { get; private set; }
        public DateTime AddedOn { get; private set; }
        public string Name { get; private set; }

        public bool IsActive
        {
            get { return Environments.Any() ? Environments.SetEquals(ActiveEnvironments) : _isActive; }
            private set { _isActive = value; }
        }

        public HashSet<string> Environments { get; private set; }
        private HashSet<string> ActiveEnvironments { get; set; }

        public void Activate()
        {
            foreach (var environment in Environments)
            {
                ActiveEnvironments.Add(environment);
            }
            IsActive = true;
        }

        public void Activate(string environment)
        {
            if (!Environments.Contains(environment))
                throw new EnvironmentNotFoundException(Name, environment);

            ActiveEnvironments.Add(environment);
        }

        public bool IsActiveIn(string environment)
        {
            return ActiveEnvironments.Contains(environment);
        }
    }
}