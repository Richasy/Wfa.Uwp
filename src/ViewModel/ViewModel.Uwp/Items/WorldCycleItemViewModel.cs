// Copyright (c) Richasy. All rights reserved.

using System;
using System.Reactive;
using Humanizer;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.Enums;
using Wfa.Models.State;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.Items
{
    /// <summary>
    /// 平原状态条目视图模型.
    /// </summary>
    public sealed class WorldCycleItemViewModel : ViewModelBase
    {
        private readonly IResourceToolkit _resourceToolkit;
        private DateTime _expiryTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldCycleItemViewModel"/> class.
        /// </summary>
        /// <param name="resourceToolkit">资源管理工具.</param>
        public WorldCycleItemViewModel(IResourceToolkit resourceToolkit)
        {
            _resourceToolkit = resourceToolkit;
            UpdateCountdownCommand = ReactiveCommand.Create(UpdateCountdown);
            UpdateDataCommand = ReactiveCommand.Create<object>(UpdateData);
        }

        /// <summary>
        /// 更新倒计时的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> UpdateCountdownCommand { get; }

        /// <summary>
        /// 更新数据命令.
        /// </summary>
        public ReactiveCommand<object, Unit> UpdateDataCommand { get; }

        /// <summary>
        /// 阵营图标.
        /// </summary>
        [Reactive]
        public WfaSymbol FactionSymbol { get; set; }

        /// <summary>
        /// 地名.
        /// </summary>
        [Reactive]
        public string Name { get; set; }

        /// <summary>
        /// 状态信息.
        /// </summary>
        [Reactive]
        public string Status { get; set; }

        /// <summary>
        /// 状态图标.
        /// </summary>
        [Reactive]
        public string StatusIcon { get; set; }

        /// <summary>
        /// 倒计时显示文本.
        /// </summary>
        [Reactive]
        public string Countdown { get; set; }

        /// <summary>
        /// 源数据.
        /// </summary>
        public object Data { get; set; }

        private void UpdateData(object data)
        {
            Data = data;
            if (data is DurationEntityBase baseData)
            {
                _expiryTime = baseData.ExpiryTime.ToLocalTime();
            }

            if (data is EarthStatus earth)
            {
                LoadDataFromEarth(earth);
            }
            else if (data is VallisStatus vallis)
            {
                LoadDataFromVallis(vallis);
            }
            else if (data is CambionStatus cambion)
            {
                LoadDataFromCambion(cambion);
            }
            else if (data is CetusStatus cetus)
            {
                LoadDataFromCetus(cetus);
            }
            else if (data is ZarimanStatus zariman)
            {
                LoadDataFromZariman(zariman);
            }
        }

        private void UpdateCountdown()
        {
            if (_expiryTime == DateTime.MinValue
                || _expiryTime <= DateTime.Now)
            {
                Countdown = TimeSpan.Zero.Humanize(minUnit: Humanizer.Localisation.TimeUnit.Second);
                return;
            }

            var ts = _expiryTime - DateTime.Now;
            Countdown = ts.Humanize(maxUnit: Humanizer.Localisation.TimeUnit.Hour, minUnit: Humanizer.Localisation.TimeUnit.Second);
        }

        private void LoadDataFromEarth(EarthStatus earth)
        {
            Name = _resourceToolkit.GetLocaleString(LanguageNames.Earth);
            FactionSymbol = WfaSymbol.Earth;
            Status = earth.IsDay
                ? _resourceToolkit.GetLocaleString(LanguageNames.Day)
                : _resourceToolkit.GetLocaleString(LanguageNames.Night);
            StatusIcon = earth.IsDay
                ? "ms-appx:///Assets/sun.png"
                : "ms-appx:///Assets/moon.png";
            UpdateCountdown();
        }

        private void LoadDataFromCetus(CetusStatus cetus)
        {
            Name = _resourceToolkit.GetLocaleString(LanguageNames.PlainsOfEidolon);
            FactionSymbol = WfaSymbol.Ostron;
            Status = cetus.IsDay
                ? _resourceToolkit.GetLocaleString(LanguageNames.Day)
                : _resourceToolkit.GetLocaleString(LanguageNames.Night);
            StatusIcon = cetus.IsDay
                ? "ms-appx:///Assets/sun.png"
                : "ms-appx:///Assets/moon.png";
            UpdateCountdown();
        }

        private void LoadDataFromVallis(VallisStatus vallis)
        {
            Name = _resourceToolkit.GetLocaleString(LanguageNames.OrbVallis);
            FactionSymbol = WfaSymbol.Solaris;
            Status = vallis.IsWarm
                ? _resourceToolkit.GetLocaleString(LanguageNames.Warm)
                : _resourceToolkit.GetLocaleString(LanguageNames.Cold);
            StatusIcon = vallis.IsWarm
                ? "ms-appx:///Assets/warm.png"
                : "ms-appx:///Assets/snow.png";
            UpdateCountdown();
        }

        private void LoadDataFromCambion(CambionStatus cambion)
        {
            Name = _resourceToolkit.GetLocaleString(LanguageNames.CambionDrift);
            FactionSymbol = WfaSymbol.Infested;
            Status = cambion.State.ToUpper();
            StatusIcon = cambion.State == "fass"
                ? "ms-appx:///Assets/sun.png"
                : "ms-appx:///Assets/moon.png";
            UpdateCountdown();
        }

        private void LoadDataFromZariman(ZarimanStatus zariman)
        {
            Name = _resourceToolkit.GetLocaleString(LanguageNames.Zariman);
            FactionSymbol = WfaSymbol.Orikin;
            Status = zariman.State.ToUpper();
            StatusIcon = zariman.IsCorpus
                ? "ms-appx:///Assets/boss_Nef_Anyo.png"
                : "ms-appx:///Assets/boss_Tyl_Regor.png";
            UpdateCountdown();
        }
    }
}
