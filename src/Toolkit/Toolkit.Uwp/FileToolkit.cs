// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Wfa.Toolkit.Interfaces;
using Windows.Storage;

namespace Wfa.Toolkit
{
    /// <summary>
    /// 文件工具.
    /// </summary>
    public sealed class FileToolkit : IFileToolkit
    {
        /// <inheritdoc/>
        public async Task ClearCacheFolderAsync()
        {
            var cacheFolder = ApplicationData.Current.LocalCacheFolder;
            var files = Directory.GetFiles(cacheFolder.Path);
            if (files.Length > 0)
            {
                var tasks = new List<Task>();
                foreach (var item in files)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        File.Delete(item);
                    }));
                }

                await Task.WhenAll(tasks);
            }
        }

        /// <inheritdoc/>
        public async Task<string> GetContentFromCacheAsync(string fileName)
        {
            var folder = ApplicationData.Current.LocalCacheFolder;
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            return await FileIO.ReadTextAsync(file);
        }

        /// <inheritdoc/>
        public async Task<string> GetFileContentFromLocalPath(string path)
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(path));
            var content = await FileIO.ReadTextAsync(file);
            return content;
        }

        /// <inheritdoc/>
        public async Task<Stream> GetFileStreamFromLocalPath(string path)
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(path));
            var stream = await file.OpenStreamForReadAsync();
            return stream;
        }

        /// <inheritdoc/>
        public async Task WriteContentToCacheAsync(string fileName, string content)
        {
            var folder = ApplicationData.Current.LocalCacheFolder;
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(file, content);
        }
    }
}
