// Copyright (c) Richasy. All rights reserved.

using System;
using Wfa.Models.Market;
using Wfa.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace Wfa.App.Pages.Overlay
{
    /// <summary>
    /// 紫卡订单页面.
    /// </summary>
    public sealed partial class RivenOrderPage : RivenOrderPageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemOrderPage"/> class.
        /// </summary>
        public RivenOrderPage()
        {
            InitializeComponent();
            Unloaded += OnUnloaded;
        }

        /// <inheritdoc/>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is RivenWeapon item)
            {
                ViewModel.SetData(item);
                ViewModel.ActiveCommand.Execute().Subscribe();
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
            => ViewModel.DeactiveCommand.Execute().Subscribe();
    }

    /// <summary>
    /// <see cref="RivenOrderPage"/> 的基类.
    /// </summary>
    public class RivenOrderPageBase : AppPage<RivenOrderPageViewModel>
    {
    }
}
