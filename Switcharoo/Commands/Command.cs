using Raven.Client;

namespace Switcharoo.Commands
{
    public abstract class Command
    {
        public IDocumentSession Session { protected get; set; }
        public abstract void Execute();
    }
}