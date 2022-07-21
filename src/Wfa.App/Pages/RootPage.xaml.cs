// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wfa.App.Controls.App;
using Wfa.App.Controls.Library;
using Wfa.App.Controls.State;
using Wfa.App.Pages.Overlay;
using Wfa.Models.Community;
using Wfa.Models.Data.Local;
using Wfa.Models.Enums;
using Wfa.Models.State;
using Wfa.ViewModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Wfa.App.Pages
{
    /// <summary>
    /// The page is used for default loading.
    /// </summary>
    public sealed partial class RootPage : RootPageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RootPage"/> class.
        /// </summary>
        public RootPage()
        {
            InitializeComponent();
            Current = this;
            Loaded += OnLoaded;
            SizeChanged += OnSizeChanged;
            ViewModel.Navigating += OnNavigating;
            CoreViewModel.RequestShowTip += OnRequestShowTip;
            CoreViewModel.RequestShowVoidTraderItems += OnRequestShowVoidTraderItems;
            CoreViewModel.RequestShowLibraryItem += OnRequestShowLibraryItem;
            CoreViewModel.RequestShowAppUpgradeDialog += OnRequestShowAppUpgradeDialogAsync;

            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }

        /// <summary>
        /// 根页面实例.
        /// </summary>
        public static RootPage Current { get; private set; }

        /// <summary>
        /// 显示顶层视图.
        /// </summary>
        /// <param name="element">要显示的元素.</param>
        public void ShowOnHolder(UIElement element)
        {
            if (!HolderContainer.Children.Contains(element))
            {
                HolderContainer.Children.Add(element);
            }

            HolderContainer.Visibility = Visibility.Visible;

            ViewModel.AddBackStack(
                BackBehavior.ShowHolder,
                ele =>
                {
                    RemoveFromHolder((UIElement)ele);
                },
                element);

            if (element is Control e)
            {
                e.Focus(FocusState.Programmatic);
            }
        }

        /// <summary>
        /// 显示提示信息，并在指定延时后关闭.
        /// </summary>
        /// <param name="element">要插入的元素.</param>
        /// <param name="delaySeconds">延时时间(秒).</param>
        /// <returns><see cref="Task"/>.</returns>
        public async Task ShowTipAsync(UIElement element, double delaySeconds)
        {
            TipContainer.Visibility = Visibility.Visible;
            TipContainer.Children.Add(element);
            element.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
            element.Visibility = Visibility.Collapsed;
            TipContainer.Children.Remove(element);
            if (TipContainer.Children.Count == 0)
            {
                TipContainer.Visibility = Visibility.Collapsed;
            }
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
            => CoreViewModel.InitializePadding();

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            CoreViewModel.InitializePadding();
#if !DEBUG
            CoreViewModel.CheckAppUpgradeCommand.Execute().Subscribe();
#endif
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            Back();
        }

        private void OnNavigating(object sender, AppNavigationEventArgs e)
        {
            if (e.Type == NavigationType.Secondary)
            {
                var type = GetSecondaryViewType(e.PageId);
                SecondaryFrame.Navigate(type, e.Parameter, new DrillInNavigationTransitionInfo());
            }
            else if (e.Type == NavigationType.Main && SecondaryFrame.Content is AppPage)
            {
                SecondaryFrame.Navigate(typeof(Page));
            }
        }

        private void OnRequestShowTip(object sender, AppTipNotificationEventArgs e)
           => new TipPopup(e.Message).ShowAsync(e.Type);

        private void OnRequestShowVoidTraderItems(object sender, IEnumerable<VoidTraderItem> e)
        {
            var popup = new VoidTraderItemsView();
            popup.Show(e);
        }

        private void OnRequestShowLibraryItem(object sender, EntryBase e)
        {
            if (e is Warframe warframe)
            {
                var popup = new WarframeView();
                popup.Show(warframe);
            }
            else if (e is Archwing archwing)
            {
                var popup = new ArchwingView();
                popup.Show(archwing);
            }
            else if (e is ArchGun archGun)
            {
                var popup = new ArchGunView();
                popup.Show(archGun);
            }
            else if (e is Primary primary)
            {
                var popup = new PrimaryView();
                popup.Show(primary);
            }
            else if (e is Secondary secondary)
            {
                var popup = new SecondaryView();
                popup.Show(secondary);
            }
            else if (e is Melee melee)
            {
                var popup = new MeleeView();
                popup.Show(melee);
            }
            else if (e is ArchMelee archMelee)
            {
                var popup = new ArchMeleeView();
                popup.Show(archMelee);
            }
            else if (e is Mod mod)
            {
                var popup = new ModView();
                popup.Show(mod);
            }
        }

        private async void OnRequestShowAppUpgradeDialogAsync(object sender, AppUpgradeEventArgs e)
        {
            try
            {
                await new AppUpgradeDialog(e).ShowAsync();
            }
            catch (Exception)
            {
            }
        }

        private void Back()
        {
            if (ViewModel.CanBack)
            {
                ViewModel.BackCommand.Execute().Subscribe();
            }
        }

        private async void OnNavViewFirstLoadedAsync(object sender, EventArgs e)
        {
            ViewModel.NavigateToMainView(PageIds.WorldStateHome);
            await CoreViewModel.InitializeLanguageAsync();
            CoreViewModel.CheckLibraryDatabaseCommand.Execute().Subscribe();
            CoreViewModel.CheckWarframeMarketDatabaseCommand.Execute().Subscribe();
            CoreViewModel.CheckTranslateDatabaseCommand.Execute().Subscribe();
            CoreViewModel.CheckPatchDatabaseCommand.Execute().Subscribe();
            CoreViewModel.BeginLoopWorldStateCommand.Execute().Subscribe();
        }

        /// <summary>
        /// 从顶层视图中移除元素.
        /// </summary>
        /// <param name="element">UI元素.</param>
        private void RemoveFromHolder(UIElement element)
            => HolderContainer.Children.Remove(element);

        private Type GetSecondaryViewType(PageIds pageId)
        {
            return pageId switch
            {
                PageIds.LibraryDetail => typeof(LibraryDetailPage),
                PageIds.MarketItemOrder => typeof(ItemOrderPage),
                PageIds.MarketLichOrder => typeof(LichOrderPage),
                PageIds.MarketRivenOrder => typeof(RivenOrderPage),
                _ => typeof(Page),
            };
        }
    }

    /// <summary>
    /// <see cref="RootPage"/> 的基类.
    /// </summary>
    public class RootPageBase : AppPage<NavigationViewModel>
    {
    }
}
