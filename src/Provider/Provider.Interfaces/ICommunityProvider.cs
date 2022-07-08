// Copyright (c) Richasy. All rights reserved.

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wfa.Models.Data.Local;
using Wfa.Models.Enums;

namespace Wfa.Provider.Interfaces
{
    /// <summary>
    /// WFCD 社区数据提供器接口.
    /// </summary>
    public interface ICommunityProvider
    {
        /// <summary>
        /// 获取指定内容的列表.
        /// </summary>
        /// <typeparam name="T">指定的数据类型.</typeparam>
        /// <param name="dataType">社区数据类型.</param>
        /// <returns><see cref="IEnumerable{T}"/>.</returns>
        Task<IEnumerable<T>> GetDataListAsync<T>(CommunityDataType dataType);

        /// <summary>
        /// 检查更新.
        /// </summary>
        /// <param name="ignoreDate">是否忽略日期检查.</param>
        /// <returns>更新结果.</returns>
        /// <remarks>
        /// 由于 Github 的更新比较频繁，默认机制是如果今天更新了，即便版本号不同，也不再更新.
        /// 可以通过 ignoreDate 参数改变这一策略.
        /// </remarks>
        Task<CommunityUpdateCheckResult> CheckUpdateAsync(bool ignoreDate);

        /// <summary>
        /// 缓存资料库所需文件.
        /// </summary>
        /// <returns>缓存结果，前者为文件名，后者为是否成功.</returns>
        Task<ConcurrentDictionary<string, bool>> CacheLibraryFilesAsync();

        /// <summary>
        /// 提交资料库版本.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        Task CommitLibraryVersionAsync(string version);

        /// <summary>
        /// 更新资料数据.
        /// </summary>
        /// <param name="dataType">数据类型.</param>
        /// <returns>更新是否成功.</returns>
        Task<bool> UpdateDataAsync(CommunityDataType dataType);

        /// <summary>
        /// 更新字典翻译数据.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        Task UpdateDictAsync();
    }
}
