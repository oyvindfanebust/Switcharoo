using System.Net;
using System.Net.Http;
using NUnit.Framework;

namespace Switcharoo.Tests
{
    [TestFixture]
    public class FeaturesApiTests
    {
        //List feature switches
        // - paging via next / previous links
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
        public void posting_new_feature_switch_returns_uri_for_created_resource()
        {
            using (var client = HttpClientFactory.Create())
            {
                var content = new JsonContent(new { name = "Feature A" });
                content.Headers.ContentType.MediaType = "application/json";
                var result = client.PostAsync("/", content).Result;

                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created), result.ToString());
                Assert.That(result.Headers.Location, Is.Not.Null);
            }
        }

        [Test]
        public void can_get_created_feature()
        {
            using (var client = HttpClientFactory.Create())
            {
                var content = new JsonContent(new { name = "Feature A" });
                content.Headers.ContentType.MediaType = "application/json";
                var resourceLocation = client.PostAsync("/", content).Result.Headers.Location;

                HttpResponseMessage result = client.GetAsync(resourceLocation).Result;
                dynamic json = result.Content.ReadAsJsonAsync().Result;
                string name = json.name;

                Assert.That(name, Is.EqualTo("Feature A"));
            }
        }
    }
}