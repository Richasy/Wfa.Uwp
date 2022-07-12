// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Splat;
using Wfa.Models.Data.Constants;
using Wfa.Models.Data.Context;
using Wfa.Models.Enums;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.Interfaces;
using Wfa.ViewModel.Items;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 资料库页面视图模型.
    /// </summary>
    public sealed partial class LibraryPageViewModel : ViewModelBase, IPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryPageViewModel"/> class.
        /// </summary>
        public LibraryPageViewModel(
            IResourceToolkit resourceToolkit,
            ISettingsToolkit settingsToolkit,
            LibraryDbContext dbContext)
        {
            _resourceToolkit = resourceToolkit;
            _settingsToolkit = settingsToolkit;
            _dbContext = dbContext;
            Sections = new ObservableCollection<LibrarySectionViewModel>();
            AddSection(CommunityDataType.Warframe);
            AddSection(CommunityDataType.Archwing);
            AddSection(CommunityDataType.ArchGun);
            AddSection(CommunityDataType.ArchMelee);
            AddSection(CommunityDataType.Primary);
            AddSection(CommunityDataType.Secondary);
            AddSection(CommunityDataType.Melee);
            AddSection(CommunityDataType.Mod);

            ActiveCommand = ReactiveCommand.CreateFromTask(ActiveAsync);
            DeactiveCommand = ReactiveCommand.Create(() => { });
        }

        private async Task ActiveAsync()
        {
            var lastUpdateTime = await _dbContext.Metas.FirstOrDefaultAsync(p => p.Name == AppConstants.WarframeItemsUpdateTimeKey);
            if (lastUpdateTime == null)
            {
                LastUpdateTime = _resourceToolkit.GetLocaleString(LanguageNames.NeverUpdate);
            }
            else
            {
                var time = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(lastUpdateTime.Value)).ToLocalTime();
                LastUpdateTime = string.Format(
                    _resourceToolkit.GetLocaleString(LanguageNames.LastUpdateTimeFormatText),
                    time.ToString("yyyy/MM/dd HH:mm"));
            }
        }

        private void AddSection(CommunityDataType type)
        {
            var vm = Locator.Current.GetService<LibrarySectionViewModel>();
            vm.SetData(type);
            Sections.Add(vm);
        }
    }
}
