// Copyright (c) Richasy. All rights reserved.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wfa.Models.State
{
    /// <summary>
    /// 午夜电波.
    /// </summary>
    public sealed class Nightwave : DurationEntityBase
    {
        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// 是否正在进行.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "active", Required = Required.Default)]
        public bool IsActive { get; set; }

        /// <summary>
        /// 赛季.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "season", Required = Required.Default)]
        public int Season { get; set; }

        /// <summary>
        /// 挑战列表.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "activeChallenges", Required = Required.Default)]
        public List<NightwaveChallenge> Challenges { get; set; }
    }

    /// <summary>
    /// 午夜电波挑战项.
    /// </summary>
    public class NightwaveChallenge : DurationEntityBase
    {
        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// 是否正在进行.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "active", Required = Required.Default)]
        public bool IsActive { get; set; }

        /// <summary>
        /// 是否为每日挑战.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "isDaily", Required = Required.Default)]
        public bool IsDaily { get; set; }

        /// <summary>
        /// 是否为精英挑战.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "isElite", Required = Required.Default)]
        public bool IsElite { get; set; }

        /// <summary>
        /// 任务描述.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "desc", Required = Required.Default)]
        public string Description { get; set; }

        /// <summary>
        /// 任务名称.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "title", Required = Required.Default)]
        public string Title { get; set; }

        /// <summary>
        /// 声望值.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "reputation", Required = Required.Default)]
        public int Reputation { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is NightwaveChallenge challenge && Id == challenge.Id;

        /// <inheritdoc/>
        public override int GetHashCode() => Id.GetHashCode();
    }
}
