// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ToolGood.Words;
using Wfa.Models.Data.Constants;
using Wfa.Models.Data.Context;
using Wfa.Models.State;
using Wfa.Provider.Interfaces;
using Wfa.Toolkit.Interfaces;

namespace Wfa.Provider
{
    /// <summary>
    /// 世界状态提供器.
    /// </summary>
    public sealed partial class StateProvider
    {
        private readonly IHttpProvider _httpProvider;
        private readonly ISettingsToolkit _settingsToolkit;
        private readonly LibraryDbContext _dbContext;

        private DateTime _cacheTime;
        private IEnumerable<News> _news;
        private IEnumerable<Event> _events;
        private IEnumerable<Fissure> _fissures;
        private IEnumerable<Invasion> _invasions;
        private Sortie _sortie;
        private SyndicateMission _ostronSyndicateMission;
        private SyndicateMission _entratiSyndicateMission;
        private SyndicateMission _solarisSyndicateMission;
        private VoidTrader _voidTrader;
        private DailySale _dailySale;
        private EarthStatus _earthStatus;
        private CetusStatus _cetusStatus;
        private CambionStatus _cambionStatus;
        private ZarimanStatus _zarimanStatus;
        private VallisStatus _vallisStatus;
        private Nightwave _nightwave;
        private Arbitration _arbitration;
        private Skirmish _skirmish;
        private SteelPath _steelPath;
        private ConstructionProgress _constructionProgress;

        private T GetParsedData<T>(JObject totalObj, string key)
        {
            if (!totalObj.ContainsKey(key))
            {
                return default;
            }

            var json = totalObj[key].ToString();
            return JsonConvert.DeserializeObject<T>(json);
        }

        private void InitializeSyndicateMissions(JObject totalObj, string key, string language)
        {
            if (!totalObj.ContainsKey(key))
            {
                return;
            }

            var syndicateMissions = totalObj[key] as JArray;
            _ostronSyndicateMission = GetSyndicateMission(syndicateMissions, "Ostrons", language);
            _entratiSyndicateMission = GetSyndicateMission(syndicateMissions, "Entrati", language);
            _solarisSyndicateMission = GetSyndicateMission(syndicateMissions, "Solaris United", language);
        }

        private SyndicateMission GetSyndicateMission(JArray missionsArr, string key, string language)
        {
            var mission = missionsArr
                .OfType<JObject>()
                .FirstOrDefault(p => p.ContainsKey("syndicateKey") && p["syndicateKey"].ToString() == key);

            if (mission == null)
            {
                return default;
            }

            var data = JsonConvert.DeserializeObject<SyndicateMission>(mission.ToString());

            if (language != AppConstants.LanguageEn)
            {
                foreach (var job in data.Jobs)
                {
                    var rewards = job.RewardPool;
                    for (var i = 0; i < rewards.Count; i++)
                    {
                        var text = TranslateSyndicateReward(rewards[i], language);
                        if (text != rewards[i] && language == AppConstants.LanguageCht)
                        {
                            text = WordsHelper.ToTraditionalChinese(text);
                        }

                        rewards[i] = text;
                    }
                }
            }

            return data;
        }

        private void InitializeNews(string language)
        {
            if (language != AppConstants.LanguageCht)
            {
                return;
            }

            if (_news?.Any() ?? false)
            {
                foreach (var item in _news)
                {
                    item.Message = WordsHelper.ToTraditionalChinese(item.Message);
                }
            }
        }

        private void InitializeSortie(string language)
        {
            if (_sortie == null)
            {
                return;
            }

            if (language == AppConstants.LanguageChs)
            {
                var variants = _sortie.Variants;
                foreach (var v in variants)
                {
                    v.MissionType = WordsHelper.ToSimplifiedChinese(v.MissionType);
                    v.Modifier = WordsHelper.ToSimplifiedChinese(v.Modifier);
                    v.ModifierDescription = WordsHelper.ToSimplifiedChinese(v.ModifierDescription);
                    v.Node = WordsHelper.ToSimplifiedChinese(v.Node);
                }
            }
        }

        private void InitializeFissures(string language)
        {
            if (language != AppConstants.LanguageCht)
            {
                return;
            }

            if (_fissures?.Any() ?? false)
            {
                foreach (var item in _fissures)
                {
                    item.Tier = WordsHelper.ToTraditionalChinese(item.Tier);
                    item.Node = WordsHelper.ToTraditionalChinese(item.Node);
                    item.MissionType = WordsHelper.ToTraditionalChinese(item.MissionType);
                }
            }
        }

        private void InitializeInvasions(string language)
        {
            if (language == AppConstants.LanguageEn)
            {
                return;
            }

            if (_invasions?.Any() ?? false)
            {
                return;
            }

            foreach (var item in _invasions)
            {
                if (language == AppConstants.LanguageChs)
                {
                    item.Node = WordsHelper.ToSimplifiedChinese(item.Node);
                }
                else if (language == AppConstants.LanguageCht)
                {
                    if (!string.IsNullOrEmpty(item.Attacker?.Reward?.Content))
                    {
                        item.Attacker.Reward.Content = WordsHelper.ToTraditionalChinese(item.Attacker.Reward.Content);
                    }

                    if (!string.IsNullOrEmpty(item.Defender?.Reward?.Content))
                    {
                        item.Defender.Reward.Content = WordsHelper.ToTraditionalChinese(item.Attacker.Reward.Content);
                    }
                }
            }
        }

        private void InitializeVoidTrader(string language)
        {
            if (_voidTrader == null)
            {
                return;
            }

            if ((_voidTrader.Inventory?.Any() ?? false) && language != AppConstants.LanguageEn)
            {
                foreach (var item in _voidTrader.Inventory)
                {
                    if (!HasChinese(item.Name))
                    {
                        item.Name = TranslateItem(item.Name, language);
                    }
                }
            }

            if (language == AppConstants.LanguageCht)
            {
                _voidTrader.Location = WordsHelper.ToTraditionalChinese(_voidTrader.Location);
            }
        }

        private void InitializeDailySale(string language)
        {
            if (_dailySale == null)
            {
                return;
            }

            if (!HasChinese(_dailySale.Item) && language != AppConstants.LanguageEn)
            {
                _dailySale.Item = TranslateItem(_dailySale.Item, language);
            }
        }

        private void InitializeNightwave(string language)
        {
            if (_nightwave == null || !(_nightwave?.Challenges?.Any() ?? false) || language == AppConstants.LanguageEn)
            {
                return;
            }

            foreach (var item in _nightwave.Challenges)
            {
                if (language == AppConstants.LanguageChs)
                {
                    item.Title = WordsHelper.ToSimplifiedChinese(item.Title);
                    item.Description = WordsHelper.ToSimplifiedChinese(item.Description);
                }
                else if (language == AppConstants.LanguageCht)
                {
                    item.Title = WordsHelper.ToTraditionalChinese(item.Title);
                    item.Description = WordsHelper.ToTraditionalChinese(item.Description);
                }
            }
        }

        private void InitializeArbitration(string language)
        {
            if (_arbitration == null || language == AppConstants.LanguageEn)
            {
                return;
            }

            if (language == AppConstants.LanguageChs)
            {
                _arbitration.Type = WordsHelper.ToSimplifiedChinese(_arbitration.Type ?? string.Empty);
                _arbitration.Node = WordsHelper.ToSimplifiedChinese(_arbitration.Node ?? string.Empty);
            }
            else if (language == AppConstants.LanguageCht)
            {
                _arbitration.Type = WordsHelper.ToTraditionalChinese(_arbitration.Type ?? string.Empty);
                _arbitration.Node = WordsHelper.ToTraditionalChinese(_arbitration.Node ?? string.Empty);
            }
        }

        private void InitializeSkirmish(string language)
        {
            if (_skirmish?.Mission == null || language == AppConstants.LanguageEn)
            {
                return;
            }

            if (language == AppConstants.LanguageChs)
            {
                _skirmish.Mission.Node = WordsHelper.ToSimplifiedChinese(_skirmish.Mission.Node);
                _skirmish.Mission.Type = WordsHelper.ToSimplifiedChinese(_skirmish.Mission.Type);
            }
            else if (language == AppConstants.LanguageCht)
            {
                _skirmish.Mission.Node = WordsHelper.ToTraditionalChinese(_skirmish.Mission.Node);
                _skirmish.Mission.Type = WordsHelper.ToTraditionalChinese(_skirmish.Mission.Type);
            }
        }

        private void InitializeSteelPath(string language)
        {
            if (_steelPath == null || language != AppConstants.LanguageCht)
            {
                return;
            }

            _steelPath.CurrentReward.Name = WordsHelper.ToTraditionalChinese(_steelPath.CurrentReward.Name);
            foreach (var item in _steelPath.Rotation)
            {
                item.Name = WordsHelper.ToTraditionalChinese(item.Name);
            }

            foreach (var item in _steelPath.Evergreens)
            {
                item.Name = WordsHelper.ToTraditionalChinese(item.Name);
            }
        }

        private string TranslateSyndicateReward(string text, string language)
        {
            if (string.IsNullOrEmpty(text))
            {
                return default;
            }

            if (char.IsNumber(text.First()))
            {
                // 第一种情况，包含数字的物品，比如 100X Oxium.
                var number = text.Split(' ').First();
                var itemName = text.Replace(number, string.Empty).Trim();
                itemName = TranslateItem(itemName, language);
                return string.Join(" ", number, itemName);
            }
            else if (text.EndsWith("Relic"))
            {
                // 第二种情况，包含遗物，比如 Lith C8 Relic.
                var splits = text.Split(' ');
                var identifier = splits[1];
                var suffix = "遗物";
                var tier = splits.First() switch
                {
                    "Lith" => "古纪",
                    "Meso" => "前纪",
                    "Neo" => "中纪",
                    "Axi" => "后纪",
                    _ => "安魂",
                };
                return string.Join(" ", tier, identifier, suffix);
            }
            else
            {
                var itemName = TranslateItem(text, language);
                if (itemName == text && itemName.Contains(" "))
                {
                    // 第三种情况，文本翻译得不到结果，此时考虑需要分拆翻译，比如 Gara Systems Blueprint.
                    var splits = itemName.Split(' ');
                    for (var i = 0; i < splits.Length; i++)
                    {
                        splits[i] = TranslateItem(splits[i], language);
                    }

                    itemName = string.Join(" ", splits);
                }

                return itemName;
            }
        }

        private string TranslateItem(string name, string language)
        {
            var translate = _dbContext.Translates.FirstOrDefault(p => p.En.Equals(name));
            if (translate != null)
            {
                var zh = translate.Zh;
                if (language == AppConstants.LanguageCht)
                {
                    var chtItem = _dbContext.Patches.FirstOrDefault(p => p.Chs == zh);
                    zh = chtItem?.Cht ?? WordsHelper.ToTraditionalChinese(zh);
                }

                return zh;
            }

            return name;
        }

        private bool HasChinese(string str)
            => Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
    }
}
