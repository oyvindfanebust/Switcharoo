using System.Net.Http;
using Newtonsoft.Json;

namespace Switcharoo.Tests.Api
{
    public class JsonContent : StringContent
    {
        public JsonContent(object value)
            : base(SerializeToJson(value))
        {
        }

        private static string SerializeToJson(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}