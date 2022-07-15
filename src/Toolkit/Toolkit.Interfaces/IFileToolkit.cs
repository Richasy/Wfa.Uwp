// Copyright (c) Richasy. All rights reserved.

using System.IO;
using System.Threading.Tasks;

namespace Wfa.Toolkit.Interfaces
{
    /// <summary>
    /// 文件工具接口定义.
    /// </summary>
    public interface IFileToolkit
    {
        /// <summary>
        /// 将文本内容写入到缓存文件中.
        /// </summary>
        /// <param name="fileName">文件名.</param>
        /// <param name="content">文件内容.</param>
        /// <returns><see cref="Task"/>.</returns>
        Task WriteContentToCacheAsync(string fileName, string content);

        /// <summary>
        /// 从缓存文件中获取内容.
        /// </summary>
        /// <param name="fileName">文件名.</param>
        /// <returns>文本内容.</returns>
        Task<string> GetContentFromCacheAsync(string fileName);

        /// <summary>
        /// 清空缓存文件夹.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        Task ClearCacheFolderAsync();

        /// <summary>
        /// 从本地路径获取文件流.
        /// </summary>
        /// <param name="path">本地文件路径.</param>
        /// <returns><see cref="Stream"/>.</returns>
        Task<Stream> GetFileStreamFromLocalPath(string path);

        /// <summary>
        /// 从本地路径获取文件内容.
        /// </summary>
        /// <param name="path">本地文件路径.</param>
        /// <returns>文本内容.</returns>
        Task<string> GetFileContentFromLocalPath(string path);
    }
}
