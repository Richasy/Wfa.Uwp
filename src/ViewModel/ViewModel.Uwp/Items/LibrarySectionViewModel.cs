// Copyright (c) Richasy. All rights reserved.

using System;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.Enums;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.Items
{
    /// <summary>
    /// 资料库分区条目视图模型.
    /// </summary>
    public sealed class LibrarySectionViewModel : ViewModelBase
    {
        private readonly IResourceToolkit _resourceToolkit;
        private readonly NavigationViewModel _navigationViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="LibrarySectionViewModel"/> class.
        /// </summary>
        public LibrarySectionViewModel(
            IResourceToolkit resourceToolkit,
            NavigationViewModel navigationViewModel)
        {
            _resourceToolkit = resourceToolkit;
            _navigationViewModel = navigationViewModel;

            NavigateToDetailCommand = ReactiveCommand.Create(NavigateToLibraryDetailPage);
        }

        /// <summary>
        /// 导航到详情页的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> NavigateToDetailCommand { get; }

        /// <summary>
        /// 标题.
        /// </summary>
        [Reactive]
        public string Title { get; set; }

        /// <summary>
        /// 社区数据类型.
        /// </summary>
        [Reactive]
        public CommunityDataType Type { get; set; }

        /// <summary>
        /// 图标.
        /// </summary>
        [Reactive]
        public WfaSymbol Icon { get; set; }

        /// <summary>
        /// 设置数据.
        /// </summary>
        /// <param name="type">社区数据类型.</param>
        public void SetData(CommunityDataType type)
        {
            Type = type;
            switch (type)
            {
                case CommunityDataType.Warframe:
                    Icon = WfaSymbol.Robot;
                    Title = _resourceToolkit.GetLocaleString(LanguageNames.Warframe);
                    break;
                case CommunityDataType.ArchGun:
                    Icon = WfaSymbol.Primary;
                    Title = _resourceToolkit.GetLocaleString(LanguageNames.ArchGun);
                    break;
                case CommunityDataType.ArchMelee:
                    Icon = WfaSymbol.Melee;
                    Title = _resourceToolkit.GetLocaleString(LanguageNames.ArchMelee);
                    break;
                case CommunityDataType.Archwing:
                    Icon = WfaSymbol.Archwing;
                    Title = "Archwing";
                    break;
                case CommunityDataType.Melee:
                    Icon = WfaSymbol.Melee;
                    Title = _resourceToolkit.GetLocaleString(LanguageNames.Melee);
                    break;
                case CommunityDataType.Primary:
                    Icon = WfaSymbol.Primary;
                    Title = _resourceToolkit.GetLocaleString(LanguageNames.Primary);
                    break;
                case CommunityDataType.Secondary:
                    Icon = WfaSymbol.Secondary;
                    Title = _resourceToolkit.GetLocaleString(LanguageNames.Secondary);
                    break;
                case CommunityDataType.Mod:
                    Icon = WfaSymbol.Mod;
                    Title = "Mod";
                    break;
                default:
                    break;
            }
        }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is LibrarySectionViewModel model && Type == model.Type;

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Type);

        private void NavigateToLibraryDetailPage()
            => _navigationViewModel.NavigateToSecondaryView(PageIds.LibraryDetail, this);
    }
}
