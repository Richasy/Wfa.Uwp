// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Wfa.Adapter.Interfaces;
using Wfa.Models.Community;
using Wfa.Models.Data.Constants;
using Wfa.Models.Data.Context;
using Wfa.Provider.Interfaces;
using Wfa.Toolkit.Interfaces;

namespace Wfa.Provider
{
    /// <summary>
    /// WFCD 社区数据提供器.
    /// </summary>
    public sealed partial class CommunityProvider
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IFileToolkit _fileToolkit;
        private readonly ISettingsToolkit _settingsToolkit;
        private readonly ICommunityAdapter _communityAdapter;
        private readonly IHttpProvider _httpProvider;

        private async Task<string> GetWarframeItemsLatestReleaseIdAsync()
        {
            var url = $"{ServiceConstants.Community.GithubApiBase}/repos/{ServiceConstants.Community.CommunityName}/{ServiceConstants.Community.ItemsRepoName}/releases/latest";
            var request = _httpProvider.GetRequestMessage(HttpMethod.Get, url);
            var response = await _httpProvider.SendAsync(request);
            var json = await _httpProvider.ParseAsync<string>(response);
            var jobj = JObject.Parse(json);
            return jobj["id"].ToString();
        }

        /// <summary>
        /// 从 warframe-items 仓库下载所需的文件.
        /// </summary>
        /// <param name="fileNames">文件名列表.</param>
        /// <returns>下载结果，文件名及对应文件是否下载完成.</returns>
        private async Task<ConcurrentDictionary<string, bool>> DownloadWarframeItemsFilesAsync(IEnumerable<string> fileNames)
        {
            var tasks = new List<Task>();
            var result = new ConcurrentDictionary<string, bool>();
            fileNames = fileNames.Distinct();
            foreach (var fileName in fileNames)
            {
                var fn = fileName.TrimStart('/');

                var task = Task.Run(async () =>
                {
                    var uri = ServiceConstants.Community.ItemsRepoRawUrl + fn;
                    var request = _httpProvider.GetRequestMessage(HttpMethod.Get, uri);
                    var response = await _httpProvider.SendAsync(request);
                    var content = await _httpProvider.ParseAsync<string>(response);
                    await _fileToolkit.WriteContentToCacheAsync(fn, content);
                }).ContinueWith(t =>
                {
                    result.TryAdd(fn, t.IsCompleted);
                });

                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            return result;
        }

        /// <summary>
        /// 获取指定文件名的内容，如果该文件在本地未被缓存，则先执行缓存.
        /// </summary>
        /// <param name="fileName">文件名.</param>
        /// <returns>文件内容.</returns>
        /// <exception cref="TaskCanceledException">尝试从远程下载文件失败.</exception>
        private async Task<string> GetFileContentAsync(string fileName)
        {
            var content = await _fileToolkit.GetContentFromCacheAsync(fileName);
            if (string.IsNullOrEmpty(content))
            {
                var response = await DownloadWarframeItemsFilesAsync(new[] { fileName });
                if (!response.TryGetValue(fileName, out _))
                {
                    throw new TaskCanceledException("Download failed");
                }
            }

            var fileContent = await _fileToolkit.GetContentFromCacheAsync(fileName);
            return fileContent;
        }

        private async Task<List<Warframe>> GetWarframeListAsync()
        {
            var warframes = await _dbContext.Warframes.Include(p => p.Abilities).ToListAsync();
            return await ConvertAllAsync(warframes);
        }

        private async Task<List<ArchGun>> GetArchGunListAsync()
        {
            var archGuns = await _dbContext.ArchGun.Include(p => p.Attacks).ToListAsync();
            return await ConvertAllAsync(archGuns);
        }

        private async Task<List<ArchMelee>> GetArchMeleeListAsync()
        {
            var archMelees = await _dbContext.ArchMelee.Include(p => p.Attacks).ThenInclude(d => d.Damage).ToListAsync();
            return await ConvertAllAsync(archMelees);
        }

        private async Task<List<Archwing>> GetArchwingListAsync()
        {
            var archwing = await _dbContext.Archwing.Include(p => p.Abilities).ToListAsync();
            return await ConvertAllAsync(archwing);
        }

        private async Task<List<Melee>> GetMeleeListAsync()
        {
            var melee = await _dbContext.Melee.Include(p => p.Attacks).ThenInclude(d => d.Damage).ToListAsync();
            return await ConvertAllAsync(melee);
        }

        private async Task<List<Primary>> GetPrimaryListAsync()
        {
            var primaries = await _dbContext.Primaries.Include(p => p.Attacks).ThenInclude(d => d.Damage).ToListAsync();
            return await ConvertAllAsync(primaries);
        }

        private async Task<List<Secondary>> GetSecondaryListAsync()
        {
            var secondaries = await _dbContext.Secondaries.Include(p => p.Attacks).ThenInclude(d => d.Damage).ToListAsync();
            return await ConvertAllAsync(secondaries);
        }

        private async Task<List<Mod>> GetModListAsync()
        {
            var mods = await _dbContext.Mods.Include(p => p.Effects).ToListAsync();
            return await ConvertAllAsync(mods);
        }

        private async Task<List<T>> ConvertAllAsync<T>(List<T> items)
            where T : EntryBase
        {
            var result = new List<T>();
            foreach (var item in items)
            {
                var newOne = await ConvertAsync(item);
                result.Add(newOne);
            }

            return result;
        }

        private async Task<T> ConvertAsync<T>(T data)
            where T : EntryBase
        {
            var dict = await _dbContext.Dicts.FirstOrDefaultAsync(p => p.UniqueName.Equals(data.UniqueName));
            var json = JsonConvert.SerializeObject(data);
            var i18n = dict?.Content ?? string.Empty;
            var newOne = _communityAdapter.ConvertToEntry<T>(json, i18n);
            return newOne;
        }

        private async Task<bool> UpdateLibAsync<T>(string libFileName, DbSet<T> dataSet, Action<T> injectAction, params string[] tableNames)
            where T : EntryBase
        {
            try
            {
                var content = await GetFileContentAsync(libFileName);
                var dataList = new List<T>();
                var jArr = JArray.Parse(content);
                foreach (var jToken in jArr)
                {
                    var data = _communityAdapter.ConvertToEntry<T>(jToken.ToString());
                    if (data == null)
                    {
                        continue;
                    }

                    injectAction?.Invoke(data);
                    dataList.Add(data);
                }

                var tableRows = new List<string>();
                foreach (var name in tableNames)
                {
                    tableRows.Add($"DELETE FROM {name};\nDELETE FROM sqlite_sequence Where name = \'{name}\';");
                }

                // 移除表格数据.
                await _dbContext.Database.ExecuteSqlRawAsync($"{string.Join("\n", tableRows)}");

                // 写入数据.
                await dataSet.AddRangeAsync(dataList);
                var rows = await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
