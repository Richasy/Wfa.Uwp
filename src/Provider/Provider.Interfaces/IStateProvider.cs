// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wfa.Models.State;

namespace Wfa.Provider.Interfaces
{
    /// <summary>
    /// 世界状态提供器接口.
    /// </summary>
    public interface IStateProvider
    {
        /// <summary>
        /// 世界状态已经更新.
        /// </summary>
        event EventHandler<EventArgs> StateChanged;

        /// <summary>
        /// 缓存当前世界状态.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        Task CacheWorldStateAsync();

        /// <summary>
        /// 获取上一次缓存的时间.
        /// </summary>
        /// <returns><see cref="DateTime"/>.</returns>
        DateTime GetLastCacheTime();

        /// <summary>
        /// 获取新闻内容.
        /// </summary>
        /// <returns>新闻列表.</returns>
        IEnumerable<News> GetNews();

        /// <summary>
        /// 获取活动列表.
        /// </summary>
        /// <returns>活动列表.</returns>
        IEnumerable<Event> GetEvents();

        /// <summary>
        /// 获取裂罅列表.
        /// </summary>
        /// <returns>裂罅列表.</returns>
        IEnumerable<Fissure> GetFissures();

        /// <summary>
        /// 获取入侵列表.
        /// </summary>
        /// <returns>入侵列表.</returns>
        IEnumerable<Invasion> GetInvasions();

        /// <summary>
        /// 获取突击内容.
        /// </summary>
        /// <returns>突击信息.</returns>
        Sortie GetSortie();

        /// <summary>
        /// 获取 Ostron 的赏金任务信息.
        /// </summary>
        /// <returns>赏金任务信息.</returns>
        SyndicateMission GetOstronSyndicateMission();

        /// <summary>
        /// 获取英择谛的赏金任务信息.
        /// </summary>
        /// <returns>赏金任务信息.</returns>
        SyndicateMission GetEntratiSyndicateMission();

        /// <summary>
        /// 获取索拉里斯的赏金任务信息.
        /// </summary>
        /// <returns>赏金任务信息.</returns>
        SyndicateMission GetSolarisSyndicateMission();

        /// <summary>
        /// 获取虚空商人信息.
        /// </summary>
        /// <returns>虚空商人信息.</returns>
        VoidTrader GetVoidTrader();

        /// <summary>
        /// 获取每日折扣信息（DARVO）.
        /// </summary>
        /// <returns>每日折扣信息.</returns>
        DailySale GetDailySale();

        /// <summary>
        /// 获取地球状态.
        /// </summary>
        /// <returns>地球状态.</returns>
        EarthStatus GetEarthStatus();

        /// <summary>
        /// 获取希图斯状态.
        /// </summary>
        /// <returns>希图斯状态.</returns>
        CetusStatus GetCetusStatus();

        /// <summary>
        /// 获取魔胎之境状态.
        /// </summary>
        /// <returns>魔胎之境状态.</returns>
        CambionStatus GetCambionStatus();

        /// <summary>
        /// 获取扎里曼号状态.
        /// </summary>
        /// <returns>扎里曼号状态.</returns>
        ZarimanStatus GetZarimanStatus();

        /// <summary>
        /// 获取奥布山谷状态.
        /// </summary>
        /// <returns>奥布山谷状态.</returns>
        VallisStatus GetVallisStatus();

        /// <summary>
        /// 获取午夜电波信息.
        /// </summary>
        /// <returns>午夜电波.</returns>
        Nightwave GetNightwave();

        /// <summary>
        /// 获取仲裁信息.
        /// </summary>
        /// <returns>仲裁.</returns>
        Arbitration GetArbitration();

        /// <summary>
        /// 获取九重天信息.
        /// </summary>
        /// <returns>九重天信息.</returns>
        Skirmish GetSkirmish();

        /// <summary>
        /// 获取钢铁之路信息.
        /// </summary>
        /// <returns>钢铁之路.</returns>
        SteelPath GetSteelPath();

        /// <summary>
        /// 获取战舰建造信息.
        /// </summary>
        /// <returns>战舰进度.</returns>
        ConstructionProgress GetConstructionProgress();

        /// <summary>
        /// 获取警报信息.
        /// </summary>
        /// <returns>警报信息.</returns>
        Alert GetAlert();
    }
}
