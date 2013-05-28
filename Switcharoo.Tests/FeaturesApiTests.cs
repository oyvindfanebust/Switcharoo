using System.Net;
using System.Net.Http;
using NUnit.Framework;
using Newtonsoft.Json;

namespace Switcharoo.Tests
{
    [TestFixture]
    public class FeaturesApiTests
    {
        //List feature switches
        // - paging?
        //CRUD feature switch
        //Toggle
        // - in general
        // - for env. x
        //Caching - etag
        //Auth

        [Test]
        public void getting_root_returns_ok()
        {
            using (var client = HttpClientFactory.Create())
            {
                var response = client.GetAsync("/").Result;

                Assert.That(response.IsSuccessStatusCode, Is.True, string.Format("Actual status code: {0}", response.StatusCode));
            }
        }

        [Test]
        public void putting_new_feature_switch_returns_created()
        {
            using (var client = HttpClientFactory.Create())
            {
                var featureSwitch = new FeatureSwitch("Feature A");
                var content = new JsonContent(featureSwitch);
                content.Headers.ContentType.MediaType = "application/json";
                var result = client.PostAsync("/", content).Result;
                
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created), result.ToString());
            }
        }
    }
}