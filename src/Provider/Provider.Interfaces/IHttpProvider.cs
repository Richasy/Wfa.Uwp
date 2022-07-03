// Copyright (c) Richasy. All rights reserved.

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Wfa.Provider.Interfaces
{
    /// <summary>
    /// 网络请求提供器接口.
    /// </summary>
    public interface IHttpProvider : IDisposable
    {
        /// <summary>
        /// 内部的超时时长设置，默认为100秒.
        /// </summary>
        TimeSpan OverallTimeout { get; set; }

        /// <summary>
        /// 网络客户端.
        /// </summary>
        HttpClient HttpClient { get; }

        /// <summary>
        /// 获取 <see cref="HttpRequestMessage"/>.
        /// </summary>
        /// <param name="method">请求方法.</param>
        /// <param name="url">请求地址.</param>
        /// <param name="content">请求体.</param>
        /// <returns><see cref="HttpRequestMessage"/>.</returns>
        HttpRequestMessage GetRequestMessage(HttpMethod method, string url, HttpContent content = default);

        /// <summary>
        /// 发送请求.
        /// </summary>
        /// <param name="request">需要发送的 <see cref="HttpRequestMessage"/>.</param>
        /// <returns>返回的 <see cref="HttpResponseMessage"/>.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);

        /// <summary>
        /// 发送请求.
        /// </summary>
        /// <param name="request">需要发送的 <see cref="HttpRequestMessage"/>.</param>
        /// <param name="cancellationToken">请求的 <see cref="CancellationToken"/>.</param>
        /// <returns>返回的 <see cref="HttpResponseMessage"/>.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);

        /// <summary>
        /// 解析响应.
        /// </summary>
        /// <param name="response">得到的 <see cref="HttpResponseMessage"/>.</param>
        /// <typeparam name="T">需要转换的目标类型.</typeparam>
        /// <returns>转换结果.</returns>
        Task<T> ParseAsync<T>(HttpResponseMessage response);
    }
}
