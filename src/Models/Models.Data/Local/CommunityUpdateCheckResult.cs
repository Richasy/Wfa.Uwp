// Copyright (c) Richasy. All rights reserved.

namespace Wfa.Models.Data.Local
{
    /// <summary>
    /// WFCD 社区数据更新检查结果.
    /// </summary>
    public sealed class CommunityUpdateCheckResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommunityUpdateCheckResult"/> class.
        /// </summary>
        public CommunityUpdateCheckResult(bool needUpdate, string remoteVersion)
        {
            NeedUpdate = needUpdate;
            RemoteVersion = remoteVersion;
        }

        /// <summary>
        /// 是否需要更新.
        /// </summary>
        public bool NeedUpdate { get; }

        /// <summary>
        /// 云端的版本.
        /// </summary>
        public string RemoteVersion { get; }
    }
}
