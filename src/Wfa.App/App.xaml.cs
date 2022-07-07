// Copyright (c) Richasy. All rights reserved.

using System;
using System.Threading.Tasks;
using Splat;
using Wfa.DI.App;
using Wfa.Models.Data.Constants;
using Wfa.Toolkit.Interfaces;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Wfa.App
{
    /// <summary>
    /// Provide application-specific behaviors to supplement the default application classes.
    /// </summary>
    public sealed partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();
            DIFactory.RegisterBasicServices();
            AppConstants.CacheFolder = ApplicationData.Current.LocalCacheFolder.Path;
            Suspending += OnSuspending;
            UnhandledException += OnUnhandledException;
            Locator.Current.GetService<IAppToolkit>().InitializeTheme();
        }

        /// <summary>
        /// Called when the application is normally launched by the end user.
        /// Will be used in situations such as launching an application to open a specific file.
        /// </summary>
        /// <param name="e">Detailed information about the start request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
            => await OnLaunchedOrActivatedAsync(e);

        /// <summary>
        /// Called when the application is activated by the end user.
        /// </summary>
        /// <param name="args">Detailed information about the active request and process.</param>
        protected override async void OnActivated(IActivatedEventArgs args)
            => await OnLaunchedOrActivatedAsync(args);

        private async Task OnLaunchedOrActivatedAsync(IActivatedEventArgs e)
        {
            var appView = ApplicationView.GetForCurrentView();
            appView.SetPreferredMinSize(new Size(AppConstants.AppMinWidth, AppConstants.AppMinHeight));
            appView.SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (Window.Current.Content is not Frame rootFrame)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
                await DIFactory.RegisterRequiredServicesAsync();
            }

            if (e is LaunchActivatedEventArgs && (e as LaunchActivatedEventArgs).PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(Pages.MainPage), (e as LaunchActivatedEventArgs).Arguments);
                }
            }

            // App launched or activated by link
            else if (e is IProtocolActivatedEventArgs protocalArgs)
            {
                var arg = protocalArgs.Uri.Query.Replace("?", string.Empty);
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(Pages.MainPage), arg);
                }
            }

            // App is launched or activated by a satrtup task
            else if (e.Kind == ActivationKind.StartupTask)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(Pages.MainPage));
                }
            }

            // App is launched or activated by notification
            else if (e is ToastNotificationActivatedEventArgs toastActivationArgs)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(Pages.MainPage));
                }

                // TODO: Parse toastActivationArgs.Argument
            }

            Window.Current.Activate();
            Locator.Current.GetService<IAppToolkit>().InitializeTitleBar();
        }

        /// <summary>
        /// Called when navigation to a specific page fails.
        /// </summary>
        /// <param name="sender">Navigation failure frame.</param>
        /// <param name="e">Details about navigation failure.</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Called when the execution of the application is about to be suspended.
        /// </summary>
        /// <param name="sender">The source of the pending request.</param>
        /// <param name="e">Details about the pending request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        /// <summary>
        /// Called when an uncaught error occurs.
        /// </summary>
        /// <param name="sender">The source of the error throw.</param>
        /// <param name="e">Details about unhandled exception.</param>
        private void OnUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            Locator.Current.GetService<IFullLogger>().Error(e.Exception);
        }
    }
}
