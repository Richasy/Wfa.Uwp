// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wfa.Models.State
{
    /// <summary>
    /// 新闻.
    /// </summary>
    public class News
    {
        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// 显示的消息.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "message", Required = Required.Default)]
        public string Message { get; set; }

        /// <summary>
        /// 跳转链接.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "link", Required = Required.Default)]
        public string Link { get; set; }

        /// <summary>
        /// 图片链接.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "imageLink", Required = Required.Default)]
        public string ImageLink { get; set; }

        /// <summary>
        /// 优先显示.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "priority", Required = Required.Default)]
        public bool Priority { get; set; }

        /// <summary>
        /// 发布时间.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "date", Required = Required.Default)]
        public DateTime Date { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is News news && Id == news.Id;

        /// <inheritdoc/>
        public override int GetHashCode() => 2108858624 + EqualityComparer<string>.Default.GetHashCode(Id);
    }
}
