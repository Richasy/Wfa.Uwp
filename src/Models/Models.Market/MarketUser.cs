// Copyright (c) Richasy. All rights reserved.

using System;
using Newtonsoft.Json;

namespace Wfa.Models.Market
{
    /// <summary>
    /// 市场的用户信息.
    /// </summary>
    public class MarketUser
    {
        /// <summary>
        /// 声望.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "reputation", Required = Required.Default)]
        public int Reputation { get; set; }

        /// <summary>
        /// 地区代码.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "region", Required = Required.Default)]
        public string Region { get; set; }

        /// <summary>
        /// 上次上线时间.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "last_seen", Required = Required.Default)]
        public DateTime LastSeenTime { get; set; }

        /// <summary>
        /// 游戏名.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "ingame_name", Required = Required.Default)]
        public string GameName { get; set; }

        /// <summary>
        /// 头像.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "avatar", Required = Required.Default)]
        public string Avatar { get; set; }

        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// 用户当前状态.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "status", Required = Required.Default)]
        public string Status { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is MarketUser user && Id == user.Id;

        /// <inheritdoc/>
        public override int GetHashCode() => Id.GetHashCode();
    }
}
