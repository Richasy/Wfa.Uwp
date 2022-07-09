// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Wfa.Models.Data.Center;
using Wfa.Models.Data.Constants;
using Wfa.Models.Data.Context;
using Wfa.Models.Enums;
using Wfa.Models.Market;
using Wfa.Provider.Interfaces;
using Wfa.Toolkit.Interfaces;

using static Wfa.Models.Data.Constants.ServiceConstants.Query;

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
            ISettingsToolkit settingsToolkit,
            LibraryDbContext dbContext)
        {
            _httpProvider = httpProvider;
            _settingsToolkit = settingsToolkit;
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public Task<bool> UpdateDataAsync(MarketDataType dataType)
        {
            return dataType switch
            {
                MarketDataType.Items => UpdateDataAsync(
                    ServiceConstants.Market.MarketItems,
                    "items",
                    _dbContext.MarketItems,
                    nameof(_dbContext.MarketItems)),
                MarketDataType.LichWeapons => UpdateDataAsync(
                    ServiceConstants.Market.LichWeapons,
                    "weapons",
                    _dbContext.LichWeapons,
                    nameof(_dbContext.LichWeapons)),
                MarketDataType.LichEpemeras => UpdateDataAsync(
                    ServiceConstants.Market.LichEphemeras,
                    "ephemeras",
                    _dbContext.LichEphemeras,
                    nameof(_dbContext.LichEphemeras)),
                MarketDataType.LichQuirks => UpdateDataAsync(
                    ServiceConstants.Market.LichQuirks,
                    "quirks",
                    _dbContext.LichQuirks,
                    nameof(_dbContext.LichQuirks)),
                MarketDataType.RivenWeapons => UpdateDataAsync(
                    ServiceConstants.Market.RivenWeapons,
                    "items",
                    _dbContext.RivenWeapons,
                    nameof(_dbContext.RivenWeapons)),
                MarketDataType.RivenAttributes => UpdateDataAsync(
                    ServiceConstants.Market.RivenAttributes,
                    "attributes",
                    _dbContext.RivenAttributes,
                    nameof(_dbContext.RivenAttributes)),
                _ => throw new NotImplementedException(),
            };
        }

        /// <inheritdoc/>
        public async Task CommitUpdateTimeAsync()
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

        /// <inheritdoc/>
        public async Task<IEnumerable<ItemOrder>> GetItemOrdersAsync(string itemIdentifier)
        {
            var uri = ServiceConstants.Market.ItemOrders(itemIdentifier);
            var platform = _settingsToolkit.ReadLocalSetting(SettingNames.Platform, AppConstants.PlartformPc);
            var request = _httpProvider.GetRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Platform", platform);
            var response = await _httpProvider.SendAsync(request);
            var json = await _httpProvider.ParseAsync<string>(response);
            var jobj = JObject.Parse(json);
            var ordersStr = jobj["payload"]["orders"].ToString();
            var orders = JsonConvert.DeserializeObject<List<ItemOrder>>(ordersStr);
            return orders;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<AuctionRivenOrder>> GetRivenOrdersAsync(
            string itemIdentifier,
            MarketBuyoutPolicy buyoutPolicy,
            IEnumerable<string> positiveStats,
            IEnumerable<string> negativeStats,
            int minMasteryRank,
            int maxMasteryRank,
            int minReroll,
            int maxReroll,
            RivenRankType modRank,
            RivenModPolarity polarity,
            MarketSortType sortType)
        {
            var buyoutStr = buyoutPolicy switch
            {
                MarketBuyoutPolicy.DirectSale => "direct",
                MarketBuyoutPolicy.Auction => "with",
                _ => string.Empty,
            };
            var positiveStr = positiveStats?.Any() ?? false
                ? string.Join(",", positiveStats)
                : string.Empty;
            var negativeStr = negativeStats?.Any() ?? false
                ? string.Join(",", negativeStats)
                : string.Empty;
            var rankStr = modRank switch
            {
                RivenRankType.Max => "maxed",
                _ => "any",
            };
            var polarityStr = polarity.ToString().ToLower();
            var sortStr = sortType switch
            {
                MarketSortType.PriceDescending => "price_desc",
                MarketSortType.PriceAscending => "price_asc",
                MarketSortType.PositiveAttributeDescending => "positive_attr_desc",
                MarketSortType.PositiveAttributeAscending => "positive_attr_asc",
                _ => throw new ArgumentOutOfRangeException(),
            };

            var queryParameters = new Dictionary<string, string>
            {
                { ServiceConstants.Query.Type, "riven" },
                { WeaponIdentifier, itemIdentifier },
                { SortBy, sortStr },
                { ModRank, rankStr },
                { Polarity, polarityStr },
            };

            if (!string.IsNullOrEmpty(buyoutStr))
            {
                queryParameters.Add(BuyoutPolicy, buyoutStr);
            }

            if (!string.IsNullOrEmpty(positiveStr))
            {
                queryParameters.Add(PositiveStats, positiveStr);
            }

            if (!string.IsNullOrEmpty(negativeStr))
            {
                queryParameters.Add(NegativeStats, negativeStr);
            }

            if (minMasteryRank > 0)
            {
                queryParameters.Add(MinMastery, minMasteryRank.ToString());
            }

            if (maxMasteryRank > 0)
            {
                queryParameters.Add(MaxMastery, maxMasteryRank.ToString());
            }

            if (minReroll > 0)
            {
                queryParameters.Add(MinRerolls, minReroll.ToString());
            }

            if (maxReroll > 0)
            {
                queryParameters.Add(MaxRerolls, maxReroll.ToString());
            }

            var query = string.Join("&", queryParameters.Select(p => $"{p.Key}={p.Value}"));
            var uri = $"{ServiceConstants.Market.AuctionOrders}?{query}";
            var request = _httpProvider.GetRequestMessage(HttpMethod.Get, uri);
            await FillRequestHeaderAsync(request);
            var response = await _httpProvider.SendAsync(request);
            var json = await _httpProvider.ParseAsync<string>(response);
            var jobj = JObject.Parse(json);
            var ordersStr = jobj["payload"]["auctions"].ToString();
            var orders = JsonConvert.DeserializeObject<List<AuctionRivenOrder>>(ordersStr);
            return orders;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<AuctionLichOrder>> GetLichOrdersAsync(
            string itemIdentifier,
            MarketBuyoutPolicy buyoutPolicy,
            string elementIdentifier,
            bool hasEphemera,
            int minDamage,
            int maxDamage,
            string quirkIdentifier,
            MarketSortType sortType)
        {
            var buyoutStr = buyoutPolicy switch
            {
                MarketBuyoutPolicy.DirectSale => "direct",
                MarketBuyoutPolicy.Auction => "with",
                _ => string.Empty,
            };

            var sortStr = sortType switch
            {
                MarketSortType.PriceDescending => "price_desc",
                MarketSortType.PriceAscending => "price_asc",
                MarketSortType.DamageDescending => "damage_desc",
                MarketSortType.DamageAscending => "damage_asc",
                _ => throw new ArgumentOutOfRangeException(),
            };

            var queryParameters = new Dictionary<string, string>
            {
                { ServiceConstants.Query.Type, "lich" },
                { WeaponIdentifier, itemIdentifier },
                { SortBy, sortStr },
                { Ephemera, hasEphemera.ToString() },
            };

            if (!string.IsNullOrEmpty(buyoutStr))
            {
                queryParameters.Add(BuyoutPolicy, buyoutStr);
            }

            if (!string.IsNullOrEmpty(elementIdentifier))
            {
                queryParameters.Add(Element, elementIdentifier);
            }

            if (minDamage > 0)
            {
                queryParameters.Add(MinDamage, minDamage.ToString());
            }

            if (maxDamage > 0)
            {
                queryParameters.Add(MaxDamage, maxDamage.ToString());
            }

            if (!string.IsNullOrEmpty(quirkIdentifier))
            {
                queryParameters.Add(Quirk, quirkIdentifier);
            }

            var query = string.Join("&", queryParameters.Select(p => $"{p.Key}={p.Value}"));
            var uri = $"{ServiceConstants.Market.AuctionOrders}?{query}";
            var request = _httpProvider.GetRequestMessage(HttpMethod.Get, uri);
            await FillRequestHeaderAsync(request);
            var response = await _httpProvider.SendAsync(request);
            var json = await _httpProvider.ParseAsync<string>(response);
            var jobj = JObject.Parse(json);
            var ordersStr = jobj["payload"]["auctions"].ToString();
            var orders = JsonConvert.DeserializeObject<List<AuctionLichOrder>>(ordersStr);
            return orders;
        }

        /// <inheritdoc/>
        public async Task<DucatsPayload> GetDucatsInformationAsync()
        {
            var request = _httpProvider.GetRequestMessage(HttpMethod.Get, ServiceConstants.Market.Ducats);
            await FillRequestHeaderAsync(request);
            var response = await _httpProvider.SendAsync(request);
            var json = await _httpProvider.ParseAsync<string>(response);
            var jobj = JObject.Parse(json);
            var payloadStr = jobj["payload"].ToString();
            var data = JsonConvert.DeserializeObject<DucatsPayload>(payloadStr);
            return data;
        }
    }
}
