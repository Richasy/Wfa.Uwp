// Copyright (c) Richasy. All rights reserved.

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Wfa.Models.Data.Center;
using Wfa.Models.Data.Context;
using Wfa.Toolkit.Interfaces;

namespace Wfa.Provider
{
    /// <summary>
    /// 维基数据提供器.
    /// </summary>
    public sealed partial class WikiProvider
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IFileToolkit _fileToolkit;

        private async Task<List<Translate>> GetTranslatesFromLocalAsync()
        {
            var path = "ms-appx:///Assets/dict.csv";
            var stream = await _fileToolkit.GetFileStreamFromLocalPath(path);

            using (stream)
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                };
                using var reader = new StreamReader(stream);
                using var csv = new CsvReader(reader, config);
                var items = csv.GetRecords<InternalTranslate>();
                return items.Select(p => new Translate()
                {
                    En = p.En,
                    Zh = p.Zh.Replace("\"", string.Empty),
                }).ToList();
            }
        }

        private async Task<List<Patch>> GetPatchesFromLocalAsync()
        {
            var path = "ms-appx:///Assets/patch.csv";
            var stream = await _fileToolkit.GetFileStreamFromLocalPath(path);

            using (stream)
            {
                using var reader = new StreamReader(stream);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                var items = csv.GetRecords<InternalPatch>();
                return items.Select(p => new Patch()
                {
                    Origin = p.Origin.Replace("_", " "),
                    Chs = p.CN,
                    Cht = p.TC,
                }).ToList();
            }
        }

        private class InternalTranslate
        {
            [Index(0)]
            public string En { get; set; }

            [Index(1)]
            public string Zh { get; set; }
        }

        private class InternalPatch
        {
            [Name("Origin")]
            public string Origin { get; set; }

            [Name("cn")]
            public string CN { get; set; }

            [Name("tc")]
            public string TC { get; set; }
        }
    }
}
