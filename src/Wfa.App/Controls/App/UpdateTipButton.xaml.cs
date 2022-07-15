// Copyright (c) Richasy. All rights reserved.

using ReactiveUI;
using Splat;
using Wfa.ViewModel;

namespace Wfa.App.Controls.App
{
    /// <summary>
    /// 更新提示按钮.
    /// </summary>
    public sealed partial class UpdateTipButton : UpdateTipButtonBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTipButton"/> class.
        /// </summary>
        public UpdateTipButton()
        {
            InitializeComponent();
            ViewModel = Locator.Current.GetService<AppViewModel>();
        }

        private void OnButtonClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var flyout = (sender as CardPanel).ContextFlyout;
            flyout.ShowAt(sender as CardPanel);
        }
    }

    /// <summary>
    /// <see cref="UpdateTipButton"/> 的基类.
    /// </summary>
    public class UpdateTipButtonBase : ReactiveUserControl<AppViewModel>
    {
    }
}
