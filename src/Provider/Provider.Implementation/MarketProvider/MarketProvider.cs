// Copyright (c) Richasy. All rights reserved.

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Wfa.Models.Data.Center;
using Wfa.Models.Data.Constants;
using Wfa.Models.Data.Context;
using Wfa.Provider.Interfaces;

namespace Wfa.Provider
{
    /// <summary>
    /// 商店数据提供器.
    /// </summary>
    public sealed partial class MarketProvider : IMarketProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarketProvider"/> class.
        /// </summary>
        public MarketProvider(
            IHttpProvider httpProvider,
            LibraryDbContext dbContext)
        {
            _httpProvider = httpProvider;
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task UpdateMarketItemsAsync()
        {
            var language = (await _dbContext.Metas.FirstOrDefaultAsync(p => p.Name == AppConstants.LanguageKey))?.Value;
            var lan = language switch
            {
                "zh" => "zh-hans",
                "tc" => "zh-hant",
                _ => string.Empty
            };

            var request = _httpProvider.GetRequestMessage(HttpMethod.Get, ServiceConstants.Market.MarketItems);
            if (!string.IsNullOrEmpty(lan))
            {
                request.Headers.Add("language", lan);
            }

            var response = await _httpProvider.SendAsync(request);
            var content = await _httpProvider.ParseAsync<string>(response);
            var jobj = JObject.Parse(content);
            var items = jobj["payload"]["items"] as JArray;

            var list = new List<MarketItem>();
            for (var i = 0; i < items.Count; i++)
            {
                var obj = items[i] as JObject;
                var saleItem = new MarketItem
                {
                    Thumbnail = obj["thumb"].ToString(),
                    Code = obj["url_name"].ToString(),
                    MarketId = obj["id"].ToString(),
                    Name = obj["item_name"].ToString(),
                };

                list.Add(saleItem);
            }

            // 移除表格数据.
            await _dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM MarketItems;DELETE FROM sqlite_sequence Where name = \'MarketItems\'");

            // 写入数据.
            await _dbContext.MarketItems.AddRangeAsync(list);
            await CommitWarframeMarketUpdateTimeAsync();
            await _dbContext.SaveChangesAsync();
        }
    }
}
