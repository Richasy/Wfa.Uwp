// Copyright (c) Richasy. All rights reserved.

using Wfa.Models.Data.Context;
using Wfa.Toolkit.Interfaces;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 应用视图模型.
    /// </summary>
    public sealed partial class AppViewModel
    {
        private readonly ISettingsToolkit _settingsToolkit;
        private readonly IResourceToolkit _resourceToolkit;
        private readonly LibraryDbContext _dbContext;
    }
}
