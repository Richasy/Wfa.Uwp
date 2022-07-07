// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.State
{
    /// <summary>
    /// 战舰建造进度.
    /// </summary>
    public sealed class ConstructionProgress
    {
        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// 巨人战舰建造进度.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "fomorianProgress", Required = Required.Default)]
        public string FomorianProgress { get; set; }

        /// <summary>
        /// 利刃豺狼建造进度.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "razorbackProgress", Required = Required.Default)]
        public string RazorbackProgress { get; set; }
    }
}
