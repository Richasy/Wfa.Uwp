// Copyright (c) Richasy. All rights reserved.

using ReactiveUI;
using Splat;
using Wfa.ViewModel;

namespace Wfa.App.Controls.Settings
{
    /// <summary>
    /// 设置区块基类.
    /// </summary>
    public class SettingSectionControl : ReactiveUserControl<SettingsPageViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingSectionControl"/> class.
        /// </summary>
        public SettingSectionControl()
        {
            ViewModel = Locator.Current.GetService<SettingsPageViewModel>();
            IsTabStop = false;
        }

        /// <summary>
        /// 核心视图模型.
        /// </summary>
        public AppViewModel CoreViewModel { get; } = Locator.Current.GetService<AppViewModel>();
    }
}
