// Copyright (c) Richasy. All rights reserved.

using System;
using System.ComponentModel;
using System.Linq;
using ReactiveUI;
using Splat;
using Wfa.App.Pages;
using Wfa.App.Resources.Extensions;
using Wfa.Models.Data.Local;
using Wfa.Models.Enums;
using Wfa.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Wfa.App.Controls.App
{
    /// <summary>
    /// 应用导航视图.
    /// </summary>
    public sealed partial class AppNavigationView : AppNavigationViewBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppNavigationView"/> class.
        /// </summary>
        public AppNavigationView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Navigating += OnNavigating;
            ViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Navigating -= OnNavigating;
            ViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }

        private void OnNavigating(object sender, AppNavigationEventArgs e)
        {
            if (e.Type == NavigationType.Main)
            {
                NavigateToMainView(e.PageId);
            }

            CheckMenuButtonVisibility();
        }

        private void OnRootNavViewItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ViewModel.NavigateToMainView(PageIds.Settings);
            }
            else
            {
                var pageId = NavigationExtension.GetPageId(args.InvokedItemContainer);
                ViewModel.Navigate(pageId);
            }
        }

        private void NavigateToMainView(PageIds pageId, object parameter = null)
        {
            var pageType = pageId switch
            {
                PageIds.Help => throw new NotImplementedException(),
                PageIds.Settings => throw new NotImplementedException(),
                PageIds.WorldStateHome => typeof(WorldStatePage),
                PageIds.Library => throw new NotImplementedException(),
                PageIds.SyndicateMissions => typeof(SyndicateMissionPage),
                PageIds.Invasions => throw new NotImplementedException(),
                PageIds.Nightwave => throw new NotImplementedException(),
                PageIds.Fissure => throw new NotImplementedException(),
                PageIds.Sortie => throw new NotImplementedException(),
                PageIds.SteelPath => throw new NotImplementedException(),
                _ => default,
            };

            if (pageType != null)
            {
                MainFrame.Navigate(pageType, parameter, new DrillInNavigationTransitionInfo());
            }

            if (RootNavView.SelectedItem == null ||
                (RootNavView.SelectedItem is Microsoft.UI.Xaml.Controls.NavigationViewItem navItem &&
                NavigationExtension.GetPageId(navItem) != pageId))
            {
                var shouldSelectedItem = pageId == PageIds.Settings
                    ? RootNavView.SettingsItem as Microsoft.UI.Xaml.Controls.NavigationViewItem
                    : RootNavView.MenuItems.Concat(RootNavView.FooterMenuItems).OfType<Microsoft.UI.Xaml.Controls.NavigationViewItem>()
                                           .Where(p => NavigationExtension.GetPageId(p) == pageId).FirstOrDefault();

                RootNavView.SelectedItem = shouldSelectedItem;
            }
        }

        private void OnFrameLoaded(object sender, RoutedEventArgs e)
        {
            if (!IsFirstLoaded)
            {
                FireFirstLoadedEvent();
                IsFirstLoaded = true;
            }
        }

        private void OnDisplayModeChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewDisplayModeChangedEventArgs args)
            => CheckMenuButtonVisibility();

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.IsMainViewShown))
            {
                CheckMenuButtonVisibility();
            }
        }

        private void CheckMenuButtonVisibility()
        {
            AppViewModel.IsShowMenuButton = RootNavView.DisplayMode != Microsoft.UI.Xaml.Controls.NavigationViewDisplayMode.Expanded
                && ViewModel.IsMainViewShown;
        }
    }

    /// <summary>
    /// <see cref="AppNavigationView"/> 的基类.
    /// </summary>
    public class AppNavigationViewBase : ReactiveUserControl<NavigationViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppNavigationViewBase"/> class.
        /// </summary>
        public AppNavigationViewBase()
        {
            ViewModel = Locator.Current.GetService<NavigationViewModel>();
            AppViewModel = Locator.Current.GetService<AppViewModel>();
        }

        /// <summary>
        /// 在刚加载首页时发生.
        /// </summary>
        public event EventHandler FirstLoaded;

        /// <summary>
        /// 应用视图模型.
        /// </summary>
        protected AppViewModel AppViewModel { get; }

        /// <summary>
        /// 是否为初次加载.
        /// </summary>
        protected bool IsFirstLoaded { get; set; }

        /// <summary>
        /// 触发首次加载事件.
        /// </summary>
        protected void FireFirstLoadedEvent()
            => FirstLoaded?.Invoke(this, EventArgs.Empty);
    }
}
