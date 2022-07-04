// Copyright (c) Richasy. All rights reserved.

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wfa.Models.Data.Constants;
using Wfa.Models.Data.Context;
using Wfa.Models.Enums;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;
using Windows.Globalization;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 应用视图模型.
    /// </summary>
    public sealed partial class AppViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppViewModel"/> class.
        /// </summary>
        public AppViewModel(
            ISettingsToolkit settingsToolkit,
            IResourceToolkit resourceToolkit,
            LibraryDbContext dbContext)
        {
            _settingsToolkit = settingsToolkit;
            _resourceToolkit = resourceToolkit;
            _dbContext = dbContext;
        }

        /// <summary>
        /// 初始化语言选项.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        public async Task InitializeLanguageAsync()
        {
            var appLanguage = ApplicationLanguages.Languages.First();
            var supportLan = "en";
            if (appLanguage.Contains("zh", System.StringComparison.OrdinalIgnoreCase))
            {
                supportLan = appLanguage.Contains("cn") || appLanguage.Contains("hans")
                    ? "zh"
                    : "tc";
            }

            var localLan = await _dbContext.Metas.FirstOrDefaultAsync(p => p.Name == AppConstants.LanguageKey);
            var lanChanged = localLan == null || localLan.Value != supportLan;
            _settingsToolkit.WriteLocalSetting(SettingNames.LanguageNeedInitialized, lanChanged);
            if (localLan == null)
            {
                await _dbContext.Metas.AddAsync(new Models.Data.Center.Meta()
                {
                    Name = AppConstants.LanguageKey,
                    Value = supportLan,
                });
            }
            else
            {
                localLan.Value = supportLan;
                _dbContext.Metas.Update(localLan);
            }

            if (lanChanged)
            {
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
