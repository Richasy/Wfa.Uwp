// Copyright (c) Richasy. All rights reserved.

namespace Wfa.Models.Data.Constants
{
    /// <summary>
    /// 服务相关的常量.
    /// </summary>
    public static class ServiceConstants
    {
#pragma warning disable SA1600 // Elements should be documented
        public const string DefaultAcceptString = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
        public const string DefaultUserAgentString = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.107 Safari/537.36 Edg/92.0.902.62";

        public static class Community
        {
            public const string GithubApiBase = "https://api.github.com";
            public const string ItemsRepoRawUrl = "https://raw.githubusercontent.com/WFCD/warframe-items/master/data/json/";
            public const string CommunityName = "WFCD";
            public const string ItemsRepoName = "warframe-items";

            public const string ArcaneFileName = "Arcanes.json";
            public const string ArchGunFileName = "Arch-Gun.json";
            public const string ArchMeleeFileName = "Arch-Melee.json";
            public const string ArchwingFileName = "Archwing.json";
            public const string MeleeFileName = "Melee.json";
            public const string ModFileName = "Mods.json";
            public const string PrimaryFileName = "Primary.json";
            public const string SecondaryFileName = "Secondary.json";
            public const string WarframeFileName = "Warframes.json";
            public const string TranslateFileName = "i18n.json";
            public const string AllDataFileName = "All.json";
        }

        public static class Market
        {
            public const string MarketApiBase = "https://api.warframe.market/v1";

            public static string MarketItems => MarketApiBase + "/items";
        }

        public static class State
        {
            public const string StateApiBase = "https://api.warframestat.us/";

            public static string StateInformation(string platform, string language = "en")
                => $"{StateApiBase}{platform}?language={language}";
        }
    }
}
