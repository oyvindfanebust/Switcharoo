using System;
using Raven.Abstractions;
using Switcharoo.Entities;

namespace Switcharoo.Commands
{
    public class CreateFeature : Command
    {
        private readonly Guid _id;
        private readonly string _name;
        private readonly string[] _environments;

        public CreateFeature(Guid id, string name)
            : this(id, name, null)
        {
        }

        public CreateFeature(Guid id, string name, string[] environments)
        {
            _id = id;
            _name = name;
            _environments = environments;
        }

        public override void Execute()
        {
            Session.Store(new Feature(_id, SystemTime.UtcNow, _name, _environments));
        }
    }
}