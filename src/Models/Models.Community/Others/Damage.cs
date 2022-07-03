// Copyright (c) Richasy. All rights reserved.

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 武器伤害类型.
/// </summary>
public class DamageBase
{
    /// <summary>
    /// 标识符.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 火焰伤害.
    /// </summary>
    [JsonProperty("heat")]
    public double? Heat { get; set; }

    /// <summary>
    /// 冲击伤害.
    /// </summary>
    [JsonProperty("impact")]
    public double? Impact { get; set; }

    /// <summary>
    /// 爆炸伤害.
    /// </summary>
    [JsonProperty("blast")]
    public double? Blast { get; set; }

    /// <summary>
    /// 切割伤害.
    /// </summary>
    [JsonProperty("slash")]
    public double? Slash { get; set; }

    /// <summary>
    /// 穿刺伤害.
    /// </summary>
    [JsonProperty("puncture")]
    public double? Puncture { get; set; }

    /// <summary>
    /// 辐射伤害.
    /// </summary>
    [JsonProperty("radiation")]
    public double? Radiation { get; set; }

    /// <summary>
    /// 磁力伤害.
    /// </summary>
    [JsonProperty("magnetic")]
    public double? Magnetic { get; set; }

    /// <summary>
    /// 电伤害.
    /// </summary>
    [JsonProperty("electricity")]
    public double? Electricity { get; set; }

    /// <summary>
    /// 毒素伤害.
    /// </summary>
    [JsonProperty("toxin")]
    public double? Toxin { get; set; }

    /// <summary>
    /// 腐蚀伤害.
    /// </summary>
    [JsonProperty("corrosive")]
    public double? Corrosive { get; set; }

    /// <summary>
    /// 病毒伤害.
    /// </summary>
    [JsonProperty("viral")]
    public double? Viral { get; set; }

    /// <summary>
    /// 冰冻伤害.
    /// </summary>
    [JsonProperty("cold")]
    public double? Cold { get; set; }

    /// <summary>
    /// 虚空伤害.
    /// </summary>
    [JsonProperty("void")]
    public double? Void { get; set; }

    /// <summary>
    /// 毒气伤害.
    /// </summary>
    [JsonProperty("gas")]
    public double Gas { get; set; }
}
