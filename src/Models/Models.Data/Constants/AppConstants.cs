// Copyright (c) Richasy. All rights reserved.

namespace Wfa.Models.Data.Constants
{
    /// <summary>
    /// 应用常量.
    /// </summary>
    public static class AppConstants
    {
#pragma warning disable SA1600 // Elements should be documented
        public const string ThemeDefault = "System";
        public const string ThemeLight = "Light";
        public const string ThemeDark = "Dark";

        public const string SettingContainerName = "Richasy.Wfa.Uwp";

        public const string StartupTaskId = "Richasy.Wfa";

        public const int AppMinWidth = 500;
        public const int AppMinHeight = 500;

        public const string LoggerFolder = "Logger";
        public const string LoggerName = "AppLog.log";

        public const string WarframeItemsReleaseIdKey = "WarframeItemsReleaseId";
        public const string WarframeMarketUpdateTimeKey = "WarframeMarketUpdateTime";
        public const string LanguageKey = "Language";

        /// <summary>
        /// 伪加载时间.
        /// </summary>
        public const int FakeLoadingMilliseconds = 300;
    }
}
