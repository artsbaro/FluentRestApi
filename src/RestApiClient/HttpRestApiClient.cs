using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpRestApi
{
    public class HttpRestApiClient : HttpClient
    {
        public HttpRestApiRequest Request { get; set; }

        public HttpRestApiClient(string baseAddress)
        {
            base.BaseAddress = new Uri(baseAddress);
            this.Request = new HttpRestApiRequest();
        }

        #region Get
        public Task<HttpResponseMessage> Get()
        {
            AddAllHeaders();
            return base.GetAsync(Request.Path);
        }

        public T Get<T>()
        {
            AddAllHeaders();
            var response = base.GetAsync(Request.Path).Result;
            return JsonDeserializeObject<T>(response);
        }

        public async Task<HttpResponseMessage> GetAsync()
        {
            AddAllHeaders();
            return await base.GetAsync(Request.Path)
                .ConfigureAwait(false);
        }

        public async Task<T> GetAsync<T>()
        {
            AddAllHeaders();
            var response = await base.GetAsync(Request.Path)
                .ConfigureAwait(false);
            return await JsonDeserializeObjectAsync<T>(response)
                .ConfigureAwait(false);
        }
        #endregion

        #region Post
        public Task<HttpResponseMessage> Post(object httpContent)
        {
            using (HttpContent content = JsonSerializeObject(httpContent))
            {
                AddAllHeaders();
                return base.PostAsync(Request.Path, content);
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

        public async Task<T> PostAsync<T>(Object httpContent)
        {
            using (HttpContent content = JsonSerializeObject(httpContent))
            {
                AddAllHeaders();
                var response = await PostAsync(Request.Path, content)
                    .ConfigureAwait(false);

                return await JsonDeserializeObjectAsync<T>(response).ConfigureAwait(false);
            }
        }

        public async Task<HttpResponseMessage> PostAsync(Object httpContent)
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

        public Task<HttpResponseMessage> Put(Object httpContent)
        {
            using (HttpContent content = JsonSerializeObject(httpContent))
            {
                AddAllHeaders();
                return PutAsync(Request.Path, content);
            }
        }

        public T Put<T>(Object httpContent)
        {
            using (HttpContent content = JsonSerializeObject(httpContent))
            {
                AddAllHeaders();
                var response = base.PutAsync(Request.Path, content).Result;
                return JsonDeserializeObject<T>(response);
            }
        }

        public async Task<T> PutAsync<T>(Object httpContent)
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
        public Task<HttpResponseMessage> Delete(Uri requestUri)
        {
            AddAllHeaders();
            return base.DeleteAsync(requestUri);
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
        public Task<HttpResponseMessage> Patch(Object httpContent)
        {
            using (HttpContent content = JsonSerializeObject(httpContent))
            {
                AddAllHeaders();
                return base.PatchAsync(Request.Path, content);
            }
        }

        public T Patch<T>(Object httpContent)
        {
            using (HttpContent content = JsonSerializeObject(httpContent))
            {
                AddAllHeaders();
                var response = PatchAsync(Request.Path, content).Result;
                return JsonDeserializeObject<T>(response);
            }
        }

        public async Task<T> PatchAsync<T>(Object httpContent)
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

        public async Task<HttpResponseMessage> PatchAsync(Object httpContent)
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

        private HttpContent JsonSerializeObject(Object httpContent)
        {
            if (httpContent == null)
                return null;

            return new StringContent(
            JsonConvert.SerializeObject(httpContent),
            Encoding.UTF8,
            "application/json");
        }

        private void AddAllHeaders()
        {
            foreach (var header in Request.Headers)
            {
                base.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }
        #endregion

    }
}
