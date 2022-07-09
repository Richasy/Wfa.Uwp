// Copyright (c) Richasy. All rights reserved.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wfa.Models.Market
{
    /// <summary>
    /// 杜卡德散件响应负载.
    /// </summary>
    public sealed class DucatsPayload
    {
        /// <summary>
        /// 小时榜.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "previous_hour", Required = Required.Default)]
        public List<DucatState> HourList { get; set; }

        /// <summary>
        /// 日榜.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "previous_day", Required = Required.Default)]
        public List<DucatState> DayList { get; set; }
    }
}
