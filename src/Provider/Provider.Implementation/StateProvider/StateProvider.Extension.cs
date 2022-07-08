// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private SentientBattle _sentientBattle;
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

        private void InitializeSyndicateMissions(JObject totalObj, string key)
        {
            if (!totalObj.ContainsKey(key))
            {
                return;
            }

            var syndicateMissions = totalObj[key] as JArray;
            _ostronSyndicateMission = GetSyndicateMission(syndicateMissions, "Ostrons");
            _entratiSyndicateMission = GetSyndicateMission(syndicateMissions, "Entrati");
            _solarisSyndicateMission = GetSyndicateMission(syndicateMissions, "Solaris United");
        }

        private SyndicateMission GetSyndicateMission(JArray missionsArr, string key)
        {
            var mission = missionsArr
                .OfType<JObject>()
                .FirstOrDefault(p => p.ContainsKey("syndicateKey") && p["syndicateKey"].ToString() == key);

            if (mission == null)
            {
                return default;
            }

            var data = JsonConvert.DeserializeObject<SyndicateMission>(mission.ToString());
            return data;
        }
    }
}
