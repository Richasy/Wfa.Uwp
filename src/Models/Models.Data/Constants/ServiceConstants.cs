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

            public const string ArchGunFileName = "Arch-Gun.json";
            public const string ArchMeleeFileName = "Arch-Melee.json";
            public const string ArchwingFileName = "Archwing.json";
            public const string MeleeFileName = "Melee.json";
            public const string ModFileName = "Mods.json";
            public const string PrimaryFileName = "Primary.json";
            public const string SecondaryFileName = "Secondary.json";
            public const string WarframeFileName = "Warframes.json";
            public const string TranslateFileName = "i18n.json";
        }

        public static class Market
        {
            public const string MarketApiBase = "https://api.warframe.market/v1";

            public static string MarketItems => MarketApiBase + "/items";

            public static string LichWeapons => MarketApiBase + "/lich/weapons";

            public static string LichEphemeras => MarketApiBase + "/lich/ephemeras";

            public static string LichQuirks => MarketApiBase + "/lich/quirks";

            public static string RivenWeapons => MarketApiBase + "/riven/items";

            public static string RivenAttributes => MarketApiBase + "/riven/attributes";

            public static string AuctionOrders => MarketApiBase + "/auctions/search";

            public static string Ducats => MarketApiBase + "/tools/ducats";

            public static string Statistics(string itemName) => $"{MarketItems}/{itemName}/statistics";

            public static string Profile(string userName) => $"{MarketApiBase}/profile/{userName}/orders?include=profile";

            public static string ItemOrders(string itemName) => $"{MarketItems}/{itemName}/orders";
        }

        public static class State
        {
            public const string StateApiBase = "https://api.warframestat.us/";

            public static string StateInformation(string platform, string language = "en")
                => $"{StateApiBase}{platform}?language={language}";

            public static string SyndicateMissions(string platform)
                => $"{StateApiBase}{platform}/syndicateMissions?language=en";
        }

        public static class Query
        {
            public const string Type = "type";
            public const string BuyoutPolicy = "buyout_policy";
            public const string WeaponIdentifier = "weapon_url_name";
            public const string PositiveStats = "positive_stats";
            public const string NegativeStats = "negative_stats";
            public const string MinMastery = "mastery_rank_min";
            public const string MaxMastery = "mastery_rank_max";
            public const string MinRerolls = "re_rolls_min";
            public const string MaxRerolls = "re_rolls_max";
            public const string ModRank = "mod_rank";
            public const string Polarity = "polarity";
            public const string SortBy = "sort_by";
            public const string Element = "element";
            public const string Ephemera = "ephemera";
            public const string MinDamage = "damage_min";
            public const string MaxDamage = "damage_max";
            public const string Quirk = "quirk";
        }
    }
}
