using System;
using Raven.Client;
using Raven.Client.Embedded;
using Switcharoo.Commands;

namespace Switcharoo.Tests
{
    public class SwitcharooSpec
    {
        private static readonly IDocumentStore _documentStore;

        static SwitcharooSpec()
        {
            _documentStore = new EmbeddableDocumentStore { RunInMemory = true };
            _documentStore.Initialize();
        }

        public void Execute<TCommand>(TCommand command) where TCommand : Command
        {
            using (var session = _documentStore.OpenSession())
            {
                command.Session = session;
                command.Execute();
                session.SaveChanges();
            }
        }

        public T Load<T>(ValueType id)
        {
            using (var session = _documentStore.OpenSession())
            {
                return session.Load<T>(id);
            }
        }
    }
}