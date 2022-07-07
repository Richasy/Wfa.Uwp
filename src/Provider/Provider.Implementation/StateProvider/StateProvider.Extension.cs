// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using Wfa.Models.State;
using Wfa.Provider.Interfaces;

namespace Wfa.Provider
{
    /// <summary>
    /// 世界状态提供器.
    /// </summary>
    public sealed partial class StateProvider
    {
        private readonly IHttpProvider _httpProvider;

        private DateTime _cacheTime;
        private IEnumerable<News> _news;
        private IEnumerable<Event> _events;
        private Sortie _sortie;
        private SyndicateMission _ostronSyndicateMission;
        private SyndicateMission _entratiSyndicateMission;
        private SyndicateMission _solarisSyndicateMission;
        private IEnumerable<Fissure> _fissures;
        private IEnumerable<Invasion> _invasions;
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
    }
}
