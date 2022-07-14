// Copyright (c) Richasy. All rights reserved.

using System;
using Wfa.Models.Market;
using Wfa.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace Wfa.App.Pages.Overlay
{
    /// <summary>
    /// 玄骸订单页面.
    /// </summary>
    public sealed partial class LichOrderPage : LichOrderPageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemOrderPage"/> class.
        /// </summary>
        public LichOrderPage()
        {
            InitializeComponent();
            Unloaded += OnUnloaded;
        }

        /// <inheritdoc/>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is LichWeapon item)
            {
                ViewModel.SetData(item);
                ViewModel.ActiveCommand.Execute().Subscribe();
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
            => ViewModel.DeactiveCommand.Execute().Subscribe();
    }

    /// <summary>
    /// <see cref="LichOrderPage"/> 的基类.
    /// </summary>
    public class LichOrderPageBase : AppPage<LichOrderPageViewModel>
    {
    }
}
