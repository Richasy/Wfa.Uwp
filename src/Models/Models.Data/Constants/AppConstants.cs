﻿// Copyright (c) Richasy. All rights reserved.

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

        public const string PlartformPc = "pc";
        public const string PlartformXbox = "xb1";
        public const string PlartformPs = "ps4";
        public const string PlartformSwitch = "sw1";

        public const string LanguageChs = "zh";
        public const string LanguageCht = "tc";
        public const string LanguageEn = "en";

        public const string SettingContainerName = "Richasy.Wfa.Uwp";
        public const string StartupTaskId = "Richasy.Wfa";
        public const string LibraryDbVersion = "v1.0";
        public const string DictVersion = "220726";
        public const string PatchVersion = "220726";

        public const int AppMinWidth = 500;
        public const int AppMinHeight = 500;

        public const string LoggerFolder = "Logger";
        public const string LoggerName = "AppLog.log";

        public const string WarframeItemsReleaseIdKey = "WarframeItemsReleaseId";
        public const string WarframeItemsUpdateTimeKey = "WarframeItemsUpdateTime";
        public const string WarframeMarketUpdateTimeKey = "WarframeMarketUpdateTime";
        public const string WikiDictUpdateTimeKey = "WikiDictUpdateTime";
        public const string WikiPatchUpdateTimeKey = "WikiPatchUpdateTime";
        public const string LanguageKey = "Language";

        /// <summary>
        /// 伪加载时间.
        /// </summary>
        public const int FakeLoadingMilliseconds = 300;

        public static class Market
        {
            public const string PriceAscending = "price_asc";
            public const string PriceDescending = "price_desc";
            public const string CountAscending = "count_asc";
            public const string CountDescending = "count_desc";
            public const string DamageAscending = "damage_asc";
            public const string DamageDescending = "damage_desc";
            public const string PositiveAscending = "positive_asc";
            public const string PositiveDescending = "positive_desc";
            public const string ModRankAscending = "modrank_asc";
            public const string ModRankDescending = "modrank_desc";

            public const string InGame = "ingame";
            public const string Online = "online";
            public const string Offline = "offline";

            public const string Seller = "seller";
            public const string Buyer = "buyer";
            public const string Buyout = "buyout";
            public const string Auction = "auction";
        }
    }
}
