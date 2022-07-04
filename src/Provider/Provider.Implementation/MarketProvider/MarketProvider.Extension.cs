// Copyright (c) Richasy. All rights reserved.

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
    }
}
