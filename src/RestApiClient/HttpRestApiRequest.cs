using System.Collections.Generic;

namespace HttpRestApi
{
    public class HttpRestApiRequest
    {
        public ICollection<KeyValuePair> Headers { get; set; }
        public string Path { get; set; }

        public HttpRestApiRequest()
        {
            Headers = new List<KeyValuePair>();
        }

        public HttpRestApiRequest(string path)
        {
            Headers = new List<KeyValuePair>();
            Path = path;
        }

        public HttpRestApiRequest AddHeader(string name, string value)
        {
            Headers.Add(new KeyValuePair { Key = name, Value = value });
            return this;
        }
    }

    public static class RaaSClientFactory
    {
        public static HttpRestApiRequest Create()
        {
            return new HttpRestApiRequest();
        }

        public static HttpRestApiRequest Create(string name, string value)
        {
            return new HttpRestApiRequest().AddHeader(name, value);
        }
    }

    public class KeyValuePair
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
