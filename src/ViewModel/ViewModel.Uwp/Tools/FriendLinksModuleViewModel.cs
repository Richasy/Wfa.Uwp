// Copyright (c) Richasy. All rights reserved.

using System.Collections.ObjectModel;
using Wfa.Models.Enums;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.Tools
{
    /// <summary>
    /// 友链模块视图模型.
    /// </summary>
    public sealed class FriendLinksModuleViewModel : ViewModelBase
    {
        private readonly IResourceToolkit _resourceToolkit;

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendLinksModuleViewModel"/> class.
        /// </summary>
        public FriendLinksModuleViewModel(IResourceToolkit resourceToolkit)
        {
            _resourceToolkit = resourceToolkit;
            FriendLinks = new ObservableCollection<FriendLinkItemViewModel>();

            AddLink("Warframe Market", "https://warframe.market", LanguageNames.Market);
            AddLink("Tenno Market", "https://tenno.market", LanguageNames.Market);
            AddLink("Riven Market", "https://riven.market", LanguageNames.Market, "/_img/favicon.png");

            AddLink("Warframe 中文维基", "https://warframe.huijiwiki.com/wiki/Mainpage", LanguageNames.Wiki, "https://av.huijiwiki.com/site_avatar_warframe_l.png");
            AddLink("Warframe Wiki | Fandom", "https://warframe.fandom.com/wiki/WARFRAME_Wiki", LanguageNames.Wiki, "https://static.wikia.nocookie.net/warframe/images/4/4a/Site-favicon.ico/revision/latest");

            AddLink("Warframe吧", "https://tieba.baidu.com/f?kw=warframe", LanguageNames.Community, "https://tieba.baidu.com/favicon.ico");
            AddLink("Warframe中文社区 | KOOK", "https://kaihei.co/FnoYkl", LanguageNames.Community, "https://www.kookapp.cn/favicon.ico");
            AddLink("Warframe Forums", "https://forums.warframe.com", LanguageNames.Community, "https://content.invisioncic.com/Mwarframe/monthly_2020_09/Favicon.ico");
        }

        /// <summary>
        /// 友链集合.
        /// </summary>
        public ObservableCollection<FriendLinkItemViewModel> FriendLinks { get; }

        private void AddLink(string name, string link, LanguageNames tag, string icon = "/favicon.ico")
        {
            var iconUrl = icon.StartsWith("http")
                ? icon
                : link + icon;
            var item = new FriendLinkItemViewModel(link, name, _resourceToolkit.GetLocaleString(tag), iconUrl);
            FriendLinks.Add(item);
        }
    }
}
