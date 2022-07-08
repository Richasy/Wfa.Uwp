// Copyright (c) Richasy. All rights reserved.

using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wfa.Models.Data.Center;
using Wfa.Models.Data.Constants;
using Wfa.Models.Data.Context;
using Wfa.Provider.Interfaces;
using Wfa.Toolkit.Interfaces;

namespace Wfa.Provider
{
    /// <summary>
    /// 维基数据提供器.
    /// </summary>
    public sealed partial class WikiProvider : IWikiProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WikiProvider"/> class.
        /// </summary>
        public WikiProvider(LibraryDbContext dbContext, IFileToolkit fileToolkit)
        {
            _dbContext = dbContext;
            _fileToolkit = fileToolkit;
        }

        /// <inheritdoc/>
        public async Task UpdatePatchesAsync()
        {
            var patches = await GetPatchesFromLocalAsync();

            // 移除表格数据.
            await _dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM Patches;\nDELETE FROM sqlite_sequence Where name = \'Patches\'");

            // 写入数据.
            await _dbContext.Patches.AddRangeAsync(patches);

            var now = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            var localId = await _dbContext.Metas.FirstOrDefaultAsync(p => p.Name == AppConstants.WikiPatchUpdateTimeKey);
            if (localId == null)
            {
                localId = new Meta(AppConstants.WikiPatchUpdateTimeKey, now);
                await _dbContext.Metas.AddAsync(localId);
            }
            else
            {
                localId.Value = now;
                _dbContext.Metas.Update(localId);
            }

            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateTranslatesAsync()
        {
            var translates = await GetTranslatesFromLocalAsync();

            // 移除表格数据.
            await _dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM Translates;\nDELETE FROM sqlite_sequence Where name = \'Translates\'");

            // 写入数据.
            await _dbContext.Translates.AddRangeAsync(translates);

            var now = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            var localId = await _dbContext.Metas.FirstOrDefaultAsync(p => p.Name == AppConstants.WikiDictUpdateTimeKey);
            if (localId == null)
            {
                localId = new Meta(AppConstants.WikiDictUpdateTimeKey, now);
                await _dbContext.Metas.AddAsync(localId);
            }
            else
            {
                localId.Value = now;
                _dbContext.Metas.Update(localId);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
