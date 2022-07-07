// Copyright (c) Richasy. All rights reserved.

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Wfa.Models.Data.Constants;

namespace Wfa.Provider
{
    /// <summary>
    /// 网络服务提供器.
    /// </summary>
    public partial class HttpProvider
    {
        private HttpClient _httpClient;
        private bool _disposedValue;
        private CookieContainer _cookieContainer;

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        internal async Task<HttpResponseMessage> SendRequestAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = await _httpClient.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Dispose object.
        /// </summary>
        /// <param name="disposing">Is it disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_httpClient != null)
                    {
                        _httpClient.Dispose();
                    }
                }

                _httpClient = null;
                _disposedValue = true;
            }
        }

        private void InitHttpClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            _cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.None,
                UseCookies = true,
                CookieContainer = _cookieContainer,
            };
            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = false, NoStore = false };
            client.DefaultRequestHeaders.Add("user-agent", ServiceConstants.DefaultUserAgentString);
            client.DefaultRequestHeaders.Add("accept", ServiceConstants.DefaultAcceptString);
            _httpClient = client;
        }
    }
}
