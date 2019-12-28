using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpRestApi
{
    public class HttpRestApiClient : HttpClient
    {
        public HttpRestApiRequest Request { get; set; }

        public HttpRestApiClient(string baseAddress)
        {
            BaseAddress = new Uri(baseAddress);
            Request = new HttpRestApiRequest();
        }

        #region Get
        public HttpResponseMessage Get()
        {
            AddAllHeaders();
            return GetAsync(Request.Path).Result;
        }

        public T Get<T>()
        {
            AddAllHeaders();
            var response = GetAsync(Request.Path).Result;
            return JsonDeserializeObject<T>(response);
        }

        public async Task<HttpResponseMessage> GetAsync()
        {
            AddAllHeaders();
            return await GetAsync(Request.Path)
                .ConfigureAwait(false);
        }

        public async Task<T> GetAsync<T>()
        {
            AddAllHeaders();
            var response = await GetAsync(Request.Path)
                .ConfigureAwait(false);
            return await JsonDeserializeObjectAsync<T>(response)
                .ConfigureAwait(false);
        }
        #endregion

        #region Post
        public HttpResponseMessage Post(object httpContent)
        {
            using (HttpContent content = JsonSerializeObject(httpContent))
            {
                AddAllHeaders();
                return PostAsync(Request.Path, content).Result;
            }
        }

        public T Post<T>(object httpContent)
        {
            using (HttpContent content = JsonSerializeObject(httpContent))
            {
                AddAllHeaders();
                var response = PostAsync(Request.Path, content).Result;
                return JsonDeserializeObject<T>(response);
            }
        }

        public async Task<T> PostAsync<T>(object httpContent)
        {
            using (HttpContent content = JsonSerializeObject(httpContent))
            {
                AddAllHeaders();
                var response = await PostAsync(Request.Path, content)
                    .ConfigureAwait(false);

                return await JsonDeserializeObjectAsync<T>(response).ConfigureAwait(false);
            }
        }

        public async Task<HttpResponseMessage> PostAsync(object httpContent)
        {
            using (HttpContent content = JsonSerializeObject(httpContent))
            {
                AddAllHeaders();
                return await PostAsync(Request.Path, content)
                    .ConfigureAwait(false);
            }
        }
        #endregion

        #region Put

        public HttpResponseMessage Put(object httpContent)
        {
            using (HttpContent content = JsonSerializeObject(httpContent))
            {
                AddAllHeaders();
                return PutAsync(Request.Path, content).Result;
            }
        }

        public T Put<T>(Object httpContent)
        {
            using (HttpContent content = JsonSerializeObject(httpContent))
            {
                AddAllHeaders();
                var response = PutAsync(Request.Path, content).Result;
                return JsonDeserializeObject<T>(response);
            }
        }

        public async Task<T> PutAsync<T>(object httpContent)
        {
            using (HttpContent content = JsonSerializeObject(httpContent))
            {
                AddAllHeaders();
                var response = await PutAsync(Request.Path, content)
                    .ConfigureAwait(false);
                return await JsonDeserializeObjectAsync<T>(response)
                    .ConfigureAwait(false);
            }
        }

        public async Task<HttpResponseMessage> PutAsync(HttpContent httpContent)
        {
            using (HttpContent content = JsonSerializeObject(httpContent))
            {
                AddAllHeaders();
                return await PutAsync(Request.Path, content)
                    .ConfigureAwait(false);
            }
        }
        #endregion

        #region Delete
        public HttpResponseMessage Delete(Uri requestUri)
        {
            AddAllHeaders();
            return base.DeleteAsync(requestUri).Result;
        }

        public Task<HttpResponseMessage> Delete(string requestUri)
        {
            AddAllHeaders();
            return base.DeleteAsync(new Uri(requestUri));
        }

        public new async Task<HttpResponseMessage> DeleteAsync(Uri requestUri)
        {
            AddAllHeaders();
            return await base.DeleteAsync(requestUri)
                .ConfigureAwait(false);
        }

        public new async Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            AddAllHeaders();
            return await base.DeleteAsync(new Uri(requestUri))
                .ConfigureAwait(false);
        }
        #endregion

        #region Patch
        public HttpResponseMessage Patch(object httpContent)
        {
            using (HttpContent content = JsonSerializeObject(httpContent))
            {
                AddAllHeaders();
                return PatchAsync(Request.Path, content).Result;
            }
        }

        public T Patch<T>(object httpContent)
        {
            using (HttpContent content = JsonSerializeObject(httpContent))
            {
                AddAllHeaders();
                var response = PatchAsync(Request.Path, content).Result;
                return JsonDeserializeObject<T>(response);
            }
        }

        public async Task<T> PatchAsync<T>(object httpContent)
        {
            using (HttpContent content = JsonSerializeObject(httpContent))
            {
                AddAllHeaders();
                var response = await PatchAsync(Request.Path, content)
                    .ConfigureAwait(false);

                return await JsonDeserializeObjectAsync<T>(response)
                    .ConfigureAwait(false);
            }
        }

        public async Task<HttpResponseMessage> PatchAsync(object httpContent)
        {
            using (HttpContent content = JsonSerializeObject(httpContent))
            {
                AddAllHeaders();
                return await PatchAsync(Request.Path, content)
                    .ConfigureAwait(false);
            }
        }
        #endregion

        #region Private Methods
        private T JsonDeserializeObject<T>(HttpResponseMessage httpResponseMessage)
        {
            var objectSerialized = JsonConvert.DeserializeObject<T>(httpResponseMessage.Content.ReadAsStringAsync().Result,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return objectSerialized;
        }

        private async Task<T> JsonDeserializeObjectAsync<T>(HttpResponseMessage httpResponseMessage)
        {
            return await Task.FromResult<T>(JsonConvert.DeserializeObject<T>(await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false),
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })).ConfigureAwait(false);
        }

        public static HttpContent JsonSerializeObject(object httpContent)
        {
            return httpContent == null
                ? null
                : new StringContent(
                    JsonConvert.SerializeObject(httpContent),
                    Encoding.UTF8,
                    "application/json");
        }

        private void AddAllHeaders()
        {
            foreach (var header in Request.Headers)
            {
                switch (header.TypeHeader)
                {
                    case TypeHeader.AuthorizationBasic:
                    case TypeHeader.AuthorizationBearer:
                        DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(header.Key, header.Value);
                        break;
                    case TypeHeader.DefaulHeader:
                    default:
                        DefaultRequestHeaders.Add(header.Key, header.Value);
                        break;
                }
            }
        }
        #endregion

    }
}
