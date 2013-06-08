using System;
using Raven.Abstractions;
using Switcharoo.Entities;

namespace Switcharoo.Commands
{
    public class CreateFeature : Command
    {
        private readonly Guid _id;
        private readonly string _name;

        public CreateFeature(Guid id, string name)
        {
            _id = id;
            _name = name;
        }

        public override void Execute()
        {
            Session.Store(new Feature(_id, SystemTime.UtcNow, _name));
        }
    }
}