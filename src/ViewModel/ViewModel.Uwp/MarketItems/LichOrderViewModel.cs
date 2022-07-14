// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Wfa.Models.Market;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;
using Windows.System;

namespace Wfa.ViewModel.MarketItems
{
    /// <summary>
    /// 玄骸订单视图模型.
    /// </summary>
    public sealed class LichOrderViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemOrderViewModel"/> class.
        /// </summary>
        public LichOrderViewModel(AuctionLichOrder order, LichEphemera ephemera)
        {
            Ephemera = ephemera;
            Initialize(order);
            GotoProfileCommand = ReactiveCommand.CreateFromTask(GotoProfileAsync);
            OpenOrderCommand = ReactiveCommand.CreateFromTask(OpenOrderAsync);
        }

        /// <summary>
        /// 打开个人主页的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> GotoProfileCommand { get; }

        /// <summary>
        /// 打开订单命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> OpenOrderCommand { get; }

        /// <summary>
        /// 数据.
        /// </summary>
        [Reactive]
        public AuctionLichOrder Data { get; set; }

        /// <summary>
        /// 用户当前状态.
        /// </summary>
        [Reactive]
        public string Status { get; set; }

        /// <summary>
        /// 订单类型的可读文本.
        /// </summary>
        [Reactive]
        public string Damage { get; set; }

        /// <summary>
        /// 幻纹元素.
        /// </summary>
        [Reactive]
        public LichEphemera Ephemera { get; set; }

        /// <summary>
        /// 买断价.
        /// </summary>
        [Reactive]
        public string BuyoutPrice { get; set; }

        /// <summary>
        /// 起拍价.
        /// </summary>
        [Reactive]
        public string StartPrice { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is LichOrderViewModel model && EqualityComparer<AuctionLichOrder>.Default.Equals(Data, model.Data);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Data);

        private void Initialize(AuctionLichOrder order)
        {
            var resourceToolkit = Locator.Current.GetService<IResourceToolkit>();
            Data = order;
            Status = order.Owner.Status switch
            {
                "offline" => resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.Offline),
                "online" => resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.Online),
                "ingame" => resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.InGame),
                _ => resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.Unknown),
            };

            Damage = order.Item.Damage <= 0 ? string.Empty : order.Item.Damage.ToString();
            BuyoutPrice = order.BuyoutPrice <= 0 ? string.Empty : order.BuyoutPrice.ToString();
            StartPrice = order.StartingPrice <= 0 ? string.Empty : order.StartingPrice.ToString();
        }

        private async Task GotoProfileAsync()
            => await Launcher.LaunchUriAsync(new Uri($"https://warframe.market/profile/{Data.Owner.GameName}"));

        private async Task OpenOrderAsync()
            => await Launcher.LaunchUriAsync(new Uri($"https://warframe.market/auction/{Data.Id}"));
    }
}
