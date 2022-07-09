// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Wfa.Models.Data.Constants;
using Wfa.Models.Data.Context;
using Wfa.Models.Enums;
using Wfa.Provider.Interfaces;
using Wfa.Toolkit.Interfaces;

namespace Wfa.Provider
{
    /// <summary>
    /// 商店数据提供器.
    /// </summary>
    public sealed partial class MarketProvider
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IHttpProvider _httpProvider;
        private readonly ISettingsToolkit _settingsToolkit;

        private async Task<bool> UpdateDataAsync<T>(string url, string key, DbSet<T> dataSet, string tableName, Action<T> injectAction = null)
            where T : class
        {
            try
            {
                var request = _httpProvider.GetRequestMessage(HttpMethod.Get, url);
                await FillRequestHeaderAsync(request);
                var response = await _httpProvider.SendAsync(request);
                var content = await _httpProvider.ParseAsync<string>(response);
                var jobj = JObject.Parse(content);
                var itemsStr = jobj["payload"][key].ToString();

                var list = JsonConvert.DeserializeObject<List<T>>(itemsStr);

                if (injectAction != null)
                {
                    list.ForEach(p => injectAction.Invoke(p));
                }

                // 移除表格数据.
                await _dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM {tableName};DELETE FROM sqlite_sequence Where name = \'{tableName}\'");

                // 写入数据.
                await dataSet.AddRangeAsync(list);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task FillRequestHeaderAsync(HttpRequestMessage request)
        {
            var language = (await _dbContext.Metas.FirstOrDefaultAsync(p => p.Name == AppConstants.LanguageKey))?.Value;
            var lan = language switch
            {
                "zh" => "zh-hans",
                "tc" => "zh-hant",
                _ => "en",
            };

            var platform = _settingsToolkit.ReadLocalSetting(SettingNames.Platform, AppConstants.PlartformPc);

            if (!string.IsNullOrEmpty(lan))
            {
                request.Headers.Add("Language", lan);
            }

            request.Headers.Add("Platform", platform);
        }
    }
}
