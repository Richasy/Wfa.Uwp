// Copyright (c) Richasy. All rights reserved.

using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wfa.Models.Data.Center;
using Wfa.Models.Data.Constants;
using Wfa.Models.Data.Context;
using Wfa.Provider.Interfaces;

namespace Wfa.Provider
{
    /// <summary>
    /// 商店数据提供器.
    /// </summary>
    public sealed partial class MarketProvider
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IHttpProvider _httpProvider;

        private async Task CommitWarframeMarketUpdateTimeAsync()
        {
            var currentTime = DateTimeOffset.Now.ToLocalTime().ToUnixTimeSeconds().ToString();
            var localData = await _dbContext.Metas.FirstOrDefaultAsync(p => p.Name == AppConstants.WarframeMarketUpdateTimeKey);
            if (localData == null)
            {
                localData = new Meta(AppConstants.WarframeMarketUpdateTimeKey, currentTime);
                await _dbContext.Metas.AddAsync(localData);
            }
            else
            {
                localData.Value = currentTime;
                _dbContext.Metas.Update(localData);
            }
        }
    }
}
