// Copyright (c) Richasy. All rights reserved.

using Wfa.ViewModel;

namespace Wfa.App.Pages
{
    /// <summary>
    /// 设置页面.
    /// </summary>
    public sealed partial class SettingsPage : SettingsPageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsPage"/> class.
        /// </summary>
        public SettingsPage() => InitializeComponent();
    }

    /// <summary>
    /// <see cref="SettingsPage"/> 的基类.
    /// </summary>
    public class SettingsPageBase : AppPage<SettingsPageViewModel>
    {
    }
}
