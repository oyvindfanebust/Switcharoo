using System;
using NUnit.Framework;
using Raven.Abstractions;
using Raven.Client.Embedded;
using Switcharoo.Commands;
using Switcharoo.Entities;

namespace Switcharoo.Tests
{
    [TestFixture]
    public class when_creating_a_feature
    {
        [Test]
        public void can_load_feature()
        {
            using (var documentStore = new EmbeddableDocumentStore { RunInMemory = true })
            {
                documentStore.Initialize();
                using (var session = documentStore.OpenSession())
                {
                    var featureId = Guid.NewGuid();
                    const string featureName = "Feature A";
                    var currentTime = new DateTime(2013, 6, 8);
                    var command = new CreateFeature(featureId, featureName) { Session = session };
                    SystemTime.UtcDateTime = () => currentTime;

                    command.Execute();

                    var feature = session.Load<Feature>(featureId);

                    Assert.That(feature, Is.Not.Null);
                    Assert.That(feature.Name, Is.EqualTo(featureName));
                    Assert.That(feature.AddedOn, Is.EqualTo(currentTime));
                }
            }
        }
    }
}