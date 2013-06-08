using System;
using NUnit.Framework;
using Raven.Abstractions;
using Raven.Client.Embedded;
using Switcharoo.Commands;
using Switcharoo.Entities;

namespace Switcharoo.Tests
{
    [TestFixture]
    public class when_activating_a_feature
    {
        [Test]
        public void feature_is_active()
        {
            using (var documentStore = new EmbeddableDocumentStore { RunInMemory = true })
            {
                documentStore.Initialize();
                var featureId = Guid.NewGuid();
                using (var session = documentStore.OpenSession())
                {
                    const string featureName = "Feature A";
                    var currentTime = new DateTime(2013, 6, 8);
                    var command = new CreateFeature(featureId, featureName) {Session = session};
                    SystemTime.UtcDateTime = () => currentTime;

                    command.Execute();
                    session.SaveChanges();
                }
                using (var session = documentStore.OpenSession())
                {
                    var activateFeature = new ActivateFeature(featureId){Session = session};
                    activateFeature.Execute();

                    var feature = session.Load<Feature>(featureId);

                    Assert.That(feature.IsActive, Is.True);
                }
            }
        }
    }
}