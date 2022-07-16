// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
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
    public sealed partial class StateProvider : IStateProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateProvider"/> class.
        /// </summary>
        public StateProvider(
            IHttpProvider httpProvider,
            ISettingsToolkit settingsToolkit,
            LibraryDbContext dbContext)
        {
            _httpProvider = httpProvider;
            _settingsToolkit = settingsToolkit;
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public event EventHandler<EventArgs> StateChanged;

        /// <inheritdoc/>
        public async Task CacheWorldStateAsync()
        {
            var meta = await _dbContext.Metas.FirstOrDefaultAsync(p => p.Name == AppConstants.LanguageKey);
            var language = meta?.Value ?? AppConstants.LanguageEn;
            var lan = language;
            if (lan == AppConstants.LanguageCht)
            {
                lan = "zh";
            }

            var platform = _settingsToolkit.ReadLocalSetting(Models.Enums.SettingNames.Platform, AppConstants.PlartformPc);
            var totalRequest = _httpProvider.GetRequestMessage(HttpMethod.Get, ServiceConstants.State.StateInformation(platform, lan));
            var totalResponse = await _httpProvider.SendAsync(totalRequest);
            var jsonStr = await _httpProvider.ParseAsync<string>(totalResponse);
            await RequestEnSyndicateMissionIfNeededAsync(language, platform);
            var totalObj = JObject.Parse(jsonStr);
            _cacheTime = DateTime.Now;

            _news = GetParsedData<List<News>>(totalObj, "news");
            _events = GetParsedData<List<Event>>(totalObj, "events");
            _fissures = GetParsedData<List<Fissure>>(totalObj, "fissures").Where(p => p.IsActive && !p.IsExpired).OrderBy(p => p.TierIndex).ToList();
            _invasions = GetParsedData<List<Invasion>>(totalObj, "invasions").Where(p => !p.IsCompleted).ToList();
            _sortie = GetParsedData<Sortie>(totalObj, "sortie");
            _voidTrader = GetParsedData<VoidTrader>(totalObj, "voidTrader");
            _dailySale = GetParsedData<List<DailySale>>(totalObj, "dailyDeals").FirstOrDefault();
            _earthStatus = GetParsedData<EarthStatus>(totalObj, "earthCycle");
            _cetusStatus = GetParsedData<CetusStatus>(totalObj, "cetusCycle");
            _cambionStatus = GetParsedData<CambionStatus>(totalObj, "cambionCycle");
            _zarimanStatus = GetParsedData<ZarimanStatus>(totalObj, "zarimanCycle");
            _vallisStatus = GetParsedData<VallisStatus>(totalObj, "vallisCycle");
            _constructionProgress = GetParsedData<ConstructionProgress>(totalObj, "constructionProgress");
            _nightwave = GetParsedData<Nightwave>(totalObj, "nightwave");
            _arbitration = GetParsedData<Arbitration>(totalObj, "arbitration");
            _skirmish = GetParsedData<Skirmish>(totalObj, "sentientOutposts");
            _steelPath = GetParsedData<SteelPath>(totalObj, "steelPath");
            _alert = GetParsedData<List<Alert>>(totalObj, "alerts").FirstOrDefault();

            InitializeNews(language);
            InitializeSortie(language);
            InitializeFissures(language);
            InitializeArbitration(language);
            InitializeDailySale(language);
            InitializeInvasions(language);
            InitializeNightwave(language);
            InitializeSkirmish(language);
            InitializeSteelPath(language);
            InitializeVoidTrader(language);

            if (!string.IsNullOrEmpty(_enSyndicateMissions))
            {
                InitializeSyndicateMissions(_enSyndicateMissions, language);
            }
            else if (totalObj.ContainsKey("syndicateMissions"))
            {
                InitializeSyndicateMissions(totalObj["syndicateMissions"].ToString(), language);
            }

            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc/>
        public Arbitration GetArbitration()
            => _arbitration;

        /// <inheritdoc/>
        public CambionStatus GetCambionStatus()
            => _cambionStatus;

        /// <inheritdoc/>
        public ConstructionProgress GetConstructionProgress()
            => _constructionProgress;

        /// <inheritdoc/>
        public DailySale GetDailySale()
            => _dailySale;

        /// <inheritdoc/>
        public EarthStatus GetEarthStatus()
            => _earthStatus;

        /// <inheritdoc/>
        public CetusStatus GetCetusStatus()
            => _cetusStatus;

        /// <inheritdoc/>
        public SyndicateMission GetEntratiSyndicateMission()
            => _entratiSyndicateMission;

        /// <inheritdoc/>
        public IEnumerable<Event> GetEvents()
            => _events;

        /// <inheritdoc/>
        public IEnumerable<Fissure> GetFissures()
            => _fissures;

        /// <inheritdoc/>
        public IEnumerable<Invasion> GetInvasions()
            => _invasions;

        /// <inheritdoc/>
        public DateTime GetLastCacheTime()
            => _cacheTime;

        /// <inheritdoc/>
        public IEnumerable<News> GetNews()
            => _news;

        /// <inheritdoc/>
        public Nightwave GetNightwave()
            => _nightwave;

        /// <inheritdoc/>
        public SyndicateMission GetOstronSyndicateMission()
            => _ostronSyndicateMission;

        /// <inheritdoc/>
        public Skirmish GetSkirmish()
            => _skirmish;

        /// <inheritdoc/>
        public SyndicateMission GetSolarisSyndicateMission()
            => _solarisSyndicateMission;

        /// <inheritdoc/>
        public Sortie GetSortie()
            => _sortie;

        /// <inheritdoc/>
        public SteelPath GetSteelPath()
            => _steelPath;

        /// <inheritdoc/>
        public VallisStatus GetVallisStatus()
            => _vallisStatus;

        /// <inheritdoc/>
        public VoidTrader GetVoidTrader()
            => _voidTrader;

        /// <inheritdoc/>
        public ZarimanStatus GetZarimanStatus()
            => _zarimanStatus;

        /// <inheritdoc/>
        public Alert GetAlert()
            => _alert;
    }
}
