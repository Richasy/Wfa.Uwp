// Copyright (c) Richasy. All rights reserved.

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wfa.Adapter.Interfaces;
using Wfa.Models.Data.Context;
using Wfa.Models.Data.Local;
using Wfa.Models.Enums;
using Wfa.Provider.Interfaces;

namespace Wfa.Provider
{
    /// <summary>
    /// WFCD 社区数据提供器.
    /// </summary>
    public sealed class CommunityProvider : ICommunityProvider
    {
        private readonly LibraryDbContext _dbContext;
        private readonly ICommunityAdapter _communityAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommunityProvider"/> class.
        /// </summary>
        public CommunityProvider(
            LibraryDbContext dbContext,
            ICommunityAdapter communityAdapter)
        {
            _dbContext = dbContext;
            _communityAdapter = communityAdapter;
        }

        /// <inheritdoc/>
        public Task<ConcurrentDictionary<string, bool>> CacheLibraryFilesAsync() => throw new System.NotImplementedException();

        /// <inheritdoc/>
        public Task<CommunityUpdateCheckResult> CheckUpdateAsync() => throw new System.NotImplementedException();

        /// <inheritdoc/>
        public Task<IEnumerable<T>> GetDataListAsync<T>(CommunityDataType dataType) => throw new System.NotImplementedException();

        /// <inheritdoc/>
        public Task<bool> UpdateDataAsync(CommunityDataType dataType) => throw new System.NotImplementedException();
    }
}
