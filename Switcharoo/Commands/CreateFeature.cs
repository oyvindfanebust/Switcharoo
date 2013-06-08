using System;
using Raven.Abstractions;
using Raven.Client;
using Switcharoo.Entities;

namespace Switcharoo.Commands
{
    public class CreateFeature
    {
        public IDocumentSession Session { private get; set; }
        private readonly Guid _id;
        private readonly string _name;

        public CreateFeature(Guid id, string name)
        {
            _id = id;
            _name = name;
        }

        public void Execute()
        {
            Session.Store(new Feature(_id, SystemTime.UtcNow, _name));
        }
    }
}