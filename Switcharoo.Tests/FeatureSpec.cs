using System;
using NUnit.Framework;
using Raven.Client;
using Raven.Client.Embedded;
using Switcharoo.Commands;

namespace Switcharoo.Tests
{
    public class FeatureSpec
    {
        private IDocumentStore _documentStore;

        [SetUp]
        public void SetUp()
        {
            _documentStore = new EmbeddableDocumentStore { RunInMemory = true };
            _documentStore.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            _documentStore.Dispose();
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