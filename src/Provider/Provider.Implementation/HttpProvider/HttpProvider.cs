// Copyright (c) Richasy. All rights reserved.

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wfa.Provider.Interfaces;

namespace Wfa.Provider
{
    /// <summary>
    /// 网络服务提供器.
    /// </summary>
    public partial class HttpProvider : IHttpProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpProvider"/> class.
        /// </summary>
        public HttpProvider() => InitHttpClient();

        /// <inheritdoc/>
        public TimeSpan OverallTimeout
        {
            get => _httpClient.Timeout;
            set => _httpClient.Timeout = value;
        }

        /// <inheritdoc/>
        public HttpClient HttpClient => _httpClient;

        /// <inheritdoc/>
        public HttpRequestMessage GetRequestMessage(HttpMethod method, string url, HttpContent content = default)
        {
            HttpRequestMessage requestMessage;
            requestMessage = new HttpRequestMessage(method, url);
            if (content != null)
            {
                requestMessage.Content = content;
            }

            return requestMessage;
        }

        /// <inheritdoc/>
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
            => SendAsync(request, CancellationToken.None);

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await SendRequestAsync(request, cancellationToken);
            return response;
        }

        /// <inheritdoc/>
        public async Task<T> ParseAsync<T>(HttpResponseMessage response)
            where T : class
        {
            var responseString = await response.Content.ReadAsStringAsync();
            return typeof(T).Equals(typeof(string))
                ? responseString as T
                : JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}
