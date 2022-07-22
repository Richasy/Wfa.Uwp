// Copyright (c) Richasy. All rights reserved.

using Wfa.ViewModel.Tools;

namespace Wfa.App.Pages.Overlay
{
    /// <summary>
    /// 友链页面.
    /// </summary>
    public sealed partial class FriendLinksPage : FriendLinksPageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FriendLinksPage"/> class.
        /// </summary>
        public FriendLinksPage() => InitializeComponent();
    }

    /// <summary>
    /// <see cref="FriendLinksPage"/> 的基类.
    /// </summary>
    public class FriendLinksPageBase : AppPage<FriendLinksModuleViewModel>
    {
    }
}
