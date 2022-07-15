// Copyright (c) Richasy. All rights reserved.

using System.Globalization;
using Wfa.Models.Data.Constants;
using Wfa.Models.Enums;
using Wfa.Toolkit.Interfaces;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace Wfa.Toolkit
{
    /// <summary>
    /// Application Toolkit.
    /// </summary>
    public class AppToolkit : IAppToolkit
    {
        private readonly Application _app;
        private readonly ISettingsToolkit _settingsToolkit;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppToolkit"/> class.
        /// </summary>
        public AppToolkit(ISettingsToolkit settingsToolkit)
        {
            _app = Application.Current;
            _settingsToolkit = settingsToolkit;
        }

        /// <inheritdoc/>
        public string GetLanguageCode(bool isWindowsName = false)
        {
            var culture = CultureInfo.CurrentUICulture;
            return culture.Name;
        }

        /// <inheritdoc/>
        public IAppToolkit InitializeTheme()
        {
            var localTheme = _settingsToolkit.ReadLocalSetting(SettingNames.AppTheme, AppConstants.ThemeDefault);

            if (localTheme != AppConstants.ThemeDefault)
            {
                _app.RequestedTheme = localTheme == AppConstants.ThemeLight ?
                                        ApplicationTheme.Light :
                                        ApplicationTheme.Dark;
            }

            return this;
        }

        /// <inheritdoc/>
        public IAppToolkit InitializeTitleBar()
        {
            var view = ApplicationView.GetForCurrentView();
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            if (_app.RequestedTheme == ApplicationTheme.Dark)
            {
                // active
                view.TitleBar.BackgroundColor = Colors.Transparent;
                view.TitleBar.ForegroundColor = Colors.White;

                // inactive
                view.TitleBar.InactiveBackgroundColor = Colors.Transparent;
                view.TitleBar.InactiveForegroundColor = Colors.Gray;

                // button
                view.TitleBar.ButtonBackgroundColor = Colors.Transparent;
                view.TitleBar.ButtonForegroundColor = Colors.White;

                view.TitleBar.ButtonHoverBackgroundColor = Color.FromArgb(255, 20, 20, 20);
                view.TitleBar.ButtonHoverForegroundColor = Colors.White;

                view.TitleBar.ButtonPressedBackgroundColor = Color.FromArgb(255, 40, 40, 40);
                view.TitleBar.ButtonPressedForegroundColor = Colors.White;

                view.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
                view.TitleBar.ButtonInactiveForegroundColor = Colors.Gray;
            }
            else
            {
                // active
                view.TitleBar.BackgroundColor = Colors.Transparent;
                view.TitleBar.ForegroundColor = Colors.Black;

                // inactive
                view.TitleBar.InactiveBackgroundColor = Colors.Transparent;
                view.TitleBar.InactiveForegroundColor = Colors.Gray;

                // button
                view.TitleBar.ButtonBackgroundColor = Colors.Transparent;
                view.TitleBar.ButtonForegroundColor = Colors.DarkGray;

                view.TitleBar.ButtonHoverBackgroundColor = Colors.LightGray;
                view.TitleBar.ButtonHoverForegroundColor = Colors.DarkGray;

                view.TitleBar.ButtonPressedBackgroundColor = Colors.Gray;
                view.TitleBar.ButtonPressedForegroundColor = Colors.DarkGray;

                view.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
                view.TitleBar.ButtonInactiveForegroundColor = Colors.Gray;
            }

            return this;
        }

        /// <inheritdoc/>
        public string GetPackageVersion()
        {
            var appVersion = Package.Current.Id.Version;
            return $"{appVersion.Major}.{appVersion.Minor}.{appVersion.Build}.{appVersion.Revision}";
        }
    }
}
