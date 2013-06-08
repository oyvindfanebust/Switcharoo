using System;
using NUnit.Framework;
using Raven.Abstractions;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Switcharoo.Commands;
using Switcharoo.Entities;

namespace Switcharoo.Tests
{
    [TestFixture]
    public class when_creating_a_feature : FeatureSpec
    {
        [Test]
        public void can_load_feature()
        {
            using (var documentStore = new EmbeddableDocumentStore { RunInMemory = true })
            {
                documentStore.Initialize();
                var featureId = Guid.NewGuid();
                const string featureName = "Feature A";
                var currentTime = new DateTime(2013, 6, 8);
                using (var session = documentStore.OpenSession())
                {
                    var command = new CreateFeature(featureId, featureName) {Session = session};
                    SystemTime.UtcDateTime = () => currentTime;

                    command.Execute();
                    session.SaveChanges();
                }
                using (var session = documentStore.OpenSession())
                {
                    var feature = session.Load<Feature>(featureId);

                    Assert.That(feature, Is.Not.Null);
                    Assert.That(feature.Name, Is.EqualTo(featureName));
                    Assert.That(feature.AddedOn, Is.EqualTo(currentTime));
                }
            }
        }
    }

    public class FeatureSpec
    {
        private IDocumentStore _documentStore;

        [SetUp]
        public void SetUp()
        {
            _documentStore = new EmbeddableDocumentStore{ RunInMemory = true };
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
                command.
            }
        }
    }
}