// Copyright (c) Richasy. All rights reserved.

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Wfa.Console
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var sourceFolder = new DirectoryInfo("D:\\Projects\\warframe-items\\data\\img");
            var json = File.ReadAllText("images.json");
            var images = JsonConvert.DeserializeObject<List<string>>(json);
            System.Console.WriteLine($"已读取图片列表，总计 {images.Count} 张图片");
            if (!Directory.Exists("D:\\Projects\\LibraryImages"))
            {
                Directory.CreateDirectory("D:\\Projects\\LibraryImages");
            }

            var targetFolder = new DirectoryInfo("D:\\Projects\\LibraryImages");
            foreach (var name in images)
            {
                if (File.Exists(Path.Combine(sourceFolder.FullName, name)))
                {
                    File.Copy(Path.Combine(sourceFolder.FullName, name), Path.Combine(targetFolder.FullName, name));
                    System.Console.WriteLine($"已复制 {name}");
                }
            }

            System.Console.WriteLine("已全部复制完成");
        }
    }
}
