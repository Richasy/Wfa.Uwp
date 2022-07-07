// Copyright (c) Richasy. All rights reserved.

using System.Threading.Tasks;
using Wfa.Provider.Interfaces;

namespace Wfa.Provider
{
    /// <summary>
    /// 世界状态提供器.
    /// </summary>
    public sealed partial class StateProvider : IStateProvider
    {
        /// <inheritdoc/>
        public Task CacheWorldStateAsync() => throw new System.NotImplementedException();
    }
}
