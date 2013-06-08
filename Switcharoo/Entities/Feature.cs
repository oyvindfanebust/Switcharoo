using System;

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
        public bool IsActive { get; private set; }

        public void Activate()
        {
            IsActive = true;
        }
    }
}