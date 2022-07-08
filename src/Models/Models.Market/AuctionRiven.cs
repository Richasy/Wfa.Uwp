// Copyright (c) Richasy. All rights reserved.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wfa.Models.Market
{
    /// <summary>
    /// 拍卖订单中的紫卡信息.
    /// </summary>
    public class AuctionRiven
    {
        /// <summary>
        /// 物品类型.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "type", Required = Required.Default)]
        public string Type { get; set; }

        /// <summary>
        /// 极性.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "polarity", Required = Required.Default)]
        public string Polarity { get; set; }

        /// <summary>
        /// 段位等级.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "mastery_level", Required = Required.Default)]
        public int MasteryLevel { get; set; }

        /// <summary>
        /// 紫卡属性列表.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "attributes", Required = Required.Default)]
        public List<AuctionRivenAttribute> Attributes { get; set; }

        /// <summary>
        /// 物品标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "weapon_url_name", Required = Required.Default)]
        public string Identifier { get; set; }

        /// <summary>
        /// 重置次数.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "re_rolls", Required = Required.Default)]
        public int RerollCount { get; set; }

        /// <summary>
        /// 物品类型.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "mod_rank", Required = Required.Default)]
        public int ModRank { get; set; }

        /// <summary>
        /// 紫卡前缀，后缀名.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "name", Required = Required.Default)]
        public string Name { get; set; }
    }

    /// <summary>
    /// 紫卡属性.
    /// </summary>
    public class AuctionRivenAttribute
    {
        /// <summary>
        /// 属性值.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "value", Required = Required.Default)]
        public double Value { get; set; }

        /// <summary>
        /// 是否为正向属性.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "positive", Required = Required.Default)]
        public bool IsPositive { get; set; }

        /// <summary>
        /// 属性标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "url_name", Required = Required.Default)]
        public string Identifier { get; set; }
    }
}
