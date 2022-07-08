// Copyright (c) Richasy. All rights reserved.

using System.Threading.Tasks;

namespace Wfa.Provider.Interfaces
{
    /// <summary>
    /// 维基数据提供器接口定义.
    /// </summary>
    public interface IWikiProvider
    {
        /// <summary>
        /// 更新翻译数据.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        Task UpdateTranslatesAsync();

        /// <summary>
        /// 更新增补翻译数据.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        Task UpdatePatchesAsync();
    }
}
