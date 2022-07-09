// Copyright (c) Richasy. All rights reserved.

using System;
using Newtonsoft.Json;

namespace Wfa.Models.Market
{
    /// <summary>
    /// 拍卖订单.
    /// </summary>
    public class AuctionOrder
    {
        /// <summary>
        /// 起拍价.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "starting_price", Required = Required.Default)]
        public int StartingPrice { get; set; }

        /// <summary>
        /// 备注.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "note", Required = Required.Default)]
        public string Note { get; set; }

        /// <summary>
        /// 是否为私有订单.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "private", Required = Required.Default)]
        public bool IsPrivate { get; set; }

        /// <summary>
        /// 一口价.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "buyout_price", Required = Required.Default)]
        public int BuyoutPrice { get; set; }

        /// <summary>
        /// 是否可见.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "visible", Required = Required.Default)]
        public bool IsVisible { get; set; }

        /// <summary>
        /// 参与拍卖所需的最低声望.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "minimal_reputation", Required = Required.Default)]
        public int MinimalReputation { get; set; }

        /// <summary>
        /// 发布者.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "owner", Required = Required.Default)]
        public MarketUser Owner { get; set; }

        /// <summary>
        /// 发布平台.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "platform", Required = Required.Default)]
        public string Platform { get; set; }

        /// <summary>
        /// 订单是否已经关闭.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "closed", Required = Required.Default)]
        public bool Closed { get; set; }

        /// <summary>
        /// 创建时间.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "created", Required = Required.Default)]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 更新时间.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "updated", Required = Required.Default)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 是否为直卖订单.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "is_direct_sell", Required = Required.Default)]
        public bool IsDirectSell { get; set; }

        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is AuctionOrder order && Id == order.Id;

        /// <inheritdoc/>
        public override int GetHashCode() => Id.GetHashCode();
    }

    /// <summary>
    /// 拍卖订单.
    /// </summary>
    /// <typeparam name="T">订单物品信息类型.</typeparam>
    public class AuctionOrder<T> : AuctionOrder
    {
        /// <summary>
        /// 物品信息..
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "item", Required = Required.Default)]
        public T Item { get; set; }
    }

    /// <summary>
    /// 紫卡拍卖订单.
    /// </summary>
    public class AuctionRivenOrder : AuctionOrder<AuctionRiven>
    {
    }

    /// <summary>
    /// 玄骸拍卖订单.
    /// </summary>
    public class AuctionLichOrder : AuctionOrder<AuctionLich>
    {
    }
}
