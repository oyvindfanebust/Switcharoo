using System.Net;
using System.Net.Http;
using Should;
using Xunit;

namespace Switcharoo.Tests.Api
{
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

        [Fact]
        public void getting_root_returns_ok()
        {
            using (var client = HttpClientFactory.Create())
            {
                var response = client.GetAsync("/").Result;

                response.IsSuccessStatusCode.ShouldBeTrue(string.Format("Actual status code: {0}", response.StatusCode));
            }
        }

        [Fact]
        public void posting_new_feature_switch_returns_uri_for_created_resource()
        {
            using (var client = HttpClientFactory.Create())
            {
                var content = new JsonContent(new { name = "Feature A" });
                content.Headers.ContentType.MediaType = "application/json";
                var result = client.PostAsync("/", content).Result;

                result.StatusCode.ShouldEqual(HttpStatusCode.Created, result.ToString());
                result.Headers.Location.ShouldNotBeNull();
            }
        }

        [Fact]
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

                name.ShouldEqual("Feature A");
            }
        }
    }
}