// Copyright (c) Richasy. All rights reserved.

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Wfa.Adapter.Interfaces;
using Wfa.Models.Data.Center;
using Wfa.Models.Data.Constants;
using Wfa.Models.Data.Context;
using Wfa.Models.Data.Local;
using Wfa.Models.Enums;
using Wfa.Provider.Interfaces;

using static Wfa.Models.Data.Constants.ServiceConstants.Community;

namespace Wfa.Provider
{
    /// <summary>
    /// WFCD 社区数据提供器.
    /// </summary>
    public sealed partial class CommunityProvider : ICommunityProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommunityProvider"/> class.
        /// </summary>
        public CommunityProvider(
            LibraryDbContext dbContext,
            ICommunityAdapter communityAdapter,
            IHttpProvider httpProvider)
        {
            _dbContext = dbContext;
            _communityAdapter = communityAdapter;
            _httpProvider = httpProvider;
        }

        /// <inheritdoc/>
        public async Task<ConcurrentDictionary<string, bool>> CacheLibraryFilesAsync()
        {
            var fileNames = new[]
            {
                AllDataFileName,
                ArcaneFileName,
                ArchGunFileName,
                ArchMeleeFileName,
                ArchwingFileName,
                MeleeFileName,
                ModFileName,
                PrimaryFileName,
                SecondaryFileName,
                TranslateFileName,
                WarframeFileName,
            };

            var result = await DownloadWarframeItemsFilesAsync(fileNames);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CommunityUpdateCheckResult> CheckUpdateAsync()
        {
            var localId = await _dbContext.Metas.FirstOrDefaultAsync(p => p.Name == AppConstants.WarframeItemsReleaseIdKey);
            var cloudId = await GetWarframeItemsLatestReleaseIdAsync();
            var needUpdate = string.IsNullOrEmpty(localId?.Value) || cloudId != localId.Value;
            return new CommunityUpdateCheckResult(needUpdate, cloudId);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetDataListAsync<T>(CommunityDataType dataType)
        {
            var result = dataType switch
            {
                CommunityDataType.Warframe => await GetWarframeListAsync() as List<T>,
                CommunityDataType.ArchGun => await GetArchGunListAsync() as List<T>,
                CommunityDataType.ArchMelee => await GetArchMeleeListAsync() as List<T>,
                CommunityDataType.Archwing => await GetArchwingListAsync() as List<T>,
                CommunityDataType.Melee => await GetMeleeListAsync() as List<T>,
                CommunityDataType.Primary => await GetPrimaryListAsync() as List<T>,
                CommunityDataType.Secondary => await GetSecondaryListAsync() as List<T>,
                CommunityDataType.Mod => await GetModListAsync() as List<T>,
                _ => throw new System.NotImplementedException(),
            };

            return result;
        }

        /// <inheritdoc/>
        public Task<bool> UpdateDataAsync(CommunityDataType dataType)
        {
            var result = dataType switch
            {
                CommunityDataType.Warframe => UpdateLibAsync(
                                                WarframeFileName,
                                                _dbContext.Warframes,
                                                default,
                                                "warframes",
                                                "warframeability"),
                CommunityDataType.ArchGun => UpdateLibAsync(
                                                ArchGunFileName,
                                                _dbContext.ArchGun,
                                                default,
                                                "archgun",
                                                "archgunattack",
                                                "archgundamage"),
                CommunityDataType.ArchMelee => UpdateLibAsync(
                                                ArchMeleeFileName,
                                                _dbContext.ArchMelee,
                                                default,
                                                "archmelee",
                                                "archmeleeattack",
                                                "archmeleedamage"),
                CommunityDataType.Archwing => UpdateLibAsync(
                                                ArchwingFileName,
                                                _dbContext.Archwing,
                                                default,
                                                "archwing",
                                                "archwingability"),
                CommunityDataType.Melee => UpdateLibAsync(
                                                MeleeFileName,
                                                _dbContext.Melee,
                                                default,
                                                "melee",
                                                "meleeattack",
                                                "meleedamage"),
                CommunityDataType.Primary => UpdateLibAsync(
                                                PrimaryFileName,
                                                _dbContext.Primaries,
                                                default,
                                                "primaries",
                                                "primaryattack",
                                                "primarydamage"),
                CommunityDataType.Secondary => UpdateLibAsync(
                                                SecondaryFileName,
                                                _dbContext.Secondaries,
                                                default,
                                                "secondaries",
                                                "secondaryattack",
                                                "secondarydamage"),
                CommunityDataType.Mod => UpdateLibAsync(
                                                ModFileName,
                                                _dbContext.Mods,
                                                default,
                                                "mods",
                                                "modeffect"),
                _ => throw new System.NotImplementedException(),
            };

            return result;
        }

        /// <inheritdoc/>
        public async Task UpdateDictAsync()
        {
            var i18nJson = await GetFileContentAsync(TranslateFileName);
            var i18nJobj = JObject.Parse(i18nJson);
            var uniqueNames = i18nJobj.Properties().Select(p => p.Name).ToList();
            var dictList = new List<Dict>();
            var language = (await _dbContext.Metas.FirstOrDefaultAsync(p => p.Name == AppConstants.LanguageKey))?.Value ?? "en";
            foreach (var propName in uniqueNames)
            {
                var dictItem = new Dict();
                var transObj = i18nJobj[propName] as JObject;
                if (transObj.ContainsKey(language))
                {
                    var itemObj = transObj[language] as JObject;
                    dictItem.UniqueName = propName;
                    dictItem.Content = itemObj.ToString();
                    dictList.Add(dictItem);
                }
            }

            // 移除表格数据.
            await _dbContext.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE dicts;");

            // 写入数据.
            await _dbContext.Dicts.AddRangeAsync(dictList);
            var rows = await _dbContext.SaveChangesAsync();
        }
    }
}
