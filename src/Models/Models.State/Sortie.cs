// Copyright (c) Richasy. All rights reserved.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wfa.Models.State
{
    /// <summary>
    /// 突击信息.
    /// </summary>
    public sealed class Sortie : DurationEntityBase
    {
        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// 是否正在进行中.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "active", Required = Required.Default)]
        public bool IsActive { get; set; }

        /// <summary>
        /// 突击任务列表.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "variants", Required = Required.Default)]
        public List<SortieVariant> Variants { get; set; }

        /// <summary>
        /// 突击对应的 BOSS.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "boss", Required = Required.Default)]
        public string Boss { get; set; }

        /// <summary>
        /// 突击阵营.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "faction", Required = Required.Default)]
        public string Faction { get; set; }

        /// <summary>
        /// 是否已经过期.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "expired", Required = Required.Default)]
        public bool IsExpired { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Sortie sortie && Id == sortie.Id;

        /// <inheritdoc/>
        public override int GetHashCode() => Id.GetHashCode();
    }

    /// <summary>
    /// 突击任务.
    /// </summary>
    public sealed class SortieVariant
    {
        /// <summary>
        /// 任务类型.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "missionType", Required = Required.Default)]
        public string MissionType { get; set; }

        /// <summary>
        /// 增幅类型.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "modifier", Required = Required.Default)]
        public string Modifier { get; set; }

        /// <summary>
        /// 增幅描述.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "modifierDescription", Required = Required.Default)]
        public string ModifierDescription { get; set; }

        /// <summary>
        /// 星球节点.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "node", Required = Required.Default)]
        public string Node { get; set; }
    }
}
