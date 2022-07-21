// Copyright (c) Richasy. All rights reserved.

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wfa.Models.Data.Constants;
using Wfa.Models.Data.Local;
using Wfa.Provider.Interfaces;

namespace Wfa.Provider
{
    /// <summary>
    /// 应用更新相关.
    /// </summary>
    public class UpdateProvider : IUpdateProvider
    {
        private const string LatestReleaseUrl = "https://api.github.com/repos/Richasy/Wfa.Uwp/releases/latest";

        /// <inheritdoc/>
        public async Task<GithubReleaseResponse> GetGithubLatestReleaseAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", ServiceConstants.DefaultUserAgentString);
                var request = new HttpRequestMessage(HttpMethod.Get, LatestReleaseUrl);
                var response = await httpClient.SendAsync(request, new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GithubReleaseResponse>(content);
            }
        }
    }
}
