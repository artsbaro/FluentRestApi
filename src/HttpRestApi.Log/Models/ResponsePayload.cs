﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HttpRestApi.Log.Models
{
    public class ResponsePayload
    {
        public ResponsePayload(Guid requestId, HttpResponseMessage response)
        {

            Id = requestId;
            ResponseHeaders = response.Headers;
            ContentHeaders = response.Content.Headers;
            JsonContent = response.Content.ReadAsStringAsync().Result;
            Created = DateTime.Now;
        }

        public Guid Id { get; set; }        
        public HttpHeaders ResponseHeaders { get; private set; }
        public HttpHeaders ContentHeaders { get; private set; }
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
