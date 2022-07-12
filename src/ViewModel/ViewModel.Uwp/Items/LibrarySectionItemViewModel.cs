// Copyright (c) Richasy. All rights reserved.

using System;
using ReactiveUI.Fody.Helpers;
using Splat;
using Wfa.Models.Enums;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.Items
{
    /// <summary>
    /// 资料库分区条目视图模型.
    /// </summary>
    public sealed class LibrarySectionItemViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibrarySectionItemViewModel"/> class.
        /// </summary>
        public LibrarySectionItemViewModel(CommunityDataType type)
        {
            Type = type;
            var resourceToolkit = Locator.Current.GetService<IResourceToolkit>();
            switch (type)
            {
                case CommunityDataType.Warframe:
                    Icon = WfaSymbol.Robot;
                    Title = resourceToolkit.GetLocaleString(LanguageNames.Warframe);
                    break;
                case CommunityDataType.ArchGun:
                    Icon = WfaSymbol.Primary;
                    Title = resourceToolkit.GetLocaleString(LanguageNames.ArchGun);
                    break;
                case CommunityDataType.ArchMelee:
                    Icon = WfaSymbol.Melee;
                    Title = resourceToolkit.GetLocaleString(LanguageNames.ArchMelee);
                    break;
                case CommunityDataType.Archwing:
                    Icon = WfaSymbol.Archwing;
                    Title = "Archwing";
                    break;
                case CommunityDataType.Melee:
                    Icon = WfaSymbol.Melee;
                    Title = resourceToolkit.GetLocaleString(LanguageNames.Melee);
                    break;
                case CommunityDataType.Primary:
                    Icon = WfaSymbol.Primary;
                    Title = resourceToolkit.GetLocaleString(LanguageNames.Primary);
                    break;
                case CommunityDataType.Secondary:
                    Icon = WfaSymbol.Secondary;
                    Title = resourceToolkit.GetLocaleString(LanguageNames.Secondary);
                    break;
                case CommunityDataType.Mod:
                    Icon = WfaSymbol.Mod;
                    Title = "Mod";
                    break;
                default:
                    break;
            }
        }

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

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is LibrarySectionItemViewModel model && Type == model.Type;

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Type);
    }
}
