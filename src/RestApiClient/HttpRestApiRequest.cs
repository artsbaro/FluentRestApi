using System.Collections.Generic;

namespace HttpRestApi
{
    public class HttpRestApiRequest
    {
        /// <summary>
        /// Coleção de Header
        /// </summary>
        public ICollection<KeyValueTypeTriad> Headers { get; }

        /// <summary>
        /// Path do Request
        /// </summary>
        public string Path { get; set; }

        public HttpRestApiRequest()
        {
            Headers = new List<KeyValueTypeTriad>();
        }

        /// <summary>
        /// Inicializa do Request inserindo o path
        /// </summary>
        /// <param name="path">Caminho da requisição</param>
        public HttpRestApiRequest(string path)
        {
            Headers = new List<KeyValueTypeTriad>();
            Path = path;
        }

        /// <summary>
        /// Adiciona um Header Padrão: nome e valor; 
        /// DefaultRequestHeaders.Add(name, value);
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns>HttpRestApiRequest</returns>
        public HttpRestApiRequest AddHeader(string name, string value)
        {
            Headers.Add(new KeyValueTypeTriad { Key = name, Value = value });
            return this;
        }

        /// <summary>
        /// Adiciona um Header Authorization;
        /// DefaultRequestHeaders.Authorization;
        /// TypeHeader: Bearer
        /// </summary>
        /// <param name="value">Valor do Token</param>
        /// <returns>HttpRestApiRequest</returns>
        public HttpRestApiRequest AddBearerToken(string value)
        {
            Headers.Add(new KeyValueTypeTriad { Key = "Bearer", Value = value });
            return this;
        }

        /// <summary>
        /// Adiciona um Header Authorization;
        /// DefaultRequestHeaders.Authorization;
        /// TypeHeader: Basic
        /// </summary>
        /// <param name="value">Valor do Token</param>
        /// <returns>HttpRestApiRequest</returns>
        public HttpRestApiRequest AddBasicToken(string value)
        {
            Headers.Add(new KeyValueTypeTriad { Key = "Basic", Value = value });
            return this;
        }
    }

    /// <summary>
    /// Factory de Request
    /// </summary>
    public static class HttpRestApiRequestFactory
    {
        /// <summary>
        /// Cria uma instancia de HttpRestApiRequest
        /// </summary>
        /// <returns>HttpRestApiRequest</returns>
        public static HttpRestApiRequest Create()
        {
            return new HttpRestApiRequest();
        }

        /// <summary>
        /// Cria uma instancia de HttpRestApiRequest
        /// </summary>
        /// <param name="path">Path do Request</param>
        /// <returns>HttpRestApiRequest</returns>
        public static HttpRestApiRequest Create(string path)
        {
            return new HttpRestApiRequest(path);
        }
    }

    public class KeyValueTypeTriad
    {
        /// <summary>
        /// Chave do Header
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Valor adicionado no Header
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Tipo do Header
        /// </summary>
        public TypeHeader TypeHeader { get; set; }

        /// <summary>
        /// Inicializa o KeyValueTypeTriad
        /// </summary>
        public KeyValueTypeTriad()
        {

        }
    }

    /// <summary>
    /// Tipo do Header:
    /// DefaulHeader = 1;
    /// AuthorizationBasic = 2;
    /// AuthorizationBearer = 3
    /// </summary>
    public enum TypeHeader
    {
        DefaulHeader = 1
        , AuthorizationBasic = 2
        , AuthorizationBearer = 3
    }
}
