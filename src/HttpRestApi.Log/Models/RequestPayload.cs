using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace HttpRestApi.Log.Models
{
    public class RequestPayload
    {
        public RequestPayload(string uri, string httpVerb, HttpHeaders requestHeaders, HttpContent content)
        {
            Id = Guid.NewGuid();
            Uri = uri;
            HttpVerb = httpVerb;
            RequestHeaders = requestHeaders;
            JsonContent = content.ReadAsStringAsync().Result;
            Created = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Uri { get; private set; }
        public string HttpVerb { get; private set; }
        public HttpHeaders RequestHeaders { get; private set; }
        public string JsonContent { get; private set; }
        public DateTime Created { get; private set; }

        public override string ToString()
        {
            JsonSerializerOptions options = new JsonSerializerOptions { 
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Serialize(this, this.GetType(), options);
        }

    }
}
