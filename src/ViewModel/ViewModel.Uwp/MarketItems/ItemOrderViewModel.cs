// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Wfa.Models.Market;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;

namespace Wfa.ViewModel.MarketItems
{
    /// <summary>
    /// 条目订单视图模型.
    /// </summary>
    public sealed class ItemOrderViewModel : ViewModelBase
    {
        private readonly ItemBase _targetItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemOrderViewModel"/> class.
        /// </summary>
        public ItemOrderViewModel(ItemOrder order, ItemBase item)
        {
            _targetItem = item;
            Initialize(order);
            GotoProfileCommand = ReactiveCommand.CreateFromTask(GotoProfileAsync);
            CopyMessageCommand = ReactiveCommand.Create(CopyMessage);
        }

        /// <summary>
        /// 打开个人主页的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> GotoProfileCommand { get; }

        /// <summary>
        /// 复制消息命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> CopyMessageCommand { get; }

        /// <summary>
        /// 数据.
        /// </summary>
        [Reactive]
        public ItemOrder Data { get; set; }

        /// <summary>
        /// 用户当前状态.
        /// </summary>
        [Reactive]
        public string Status { get; set; }

        /// <summary>
        /// 订单类型的可读文本.
        /// </summary>
        [Reactive]
        public string OrderTypeText { get; set; }

        /// <summary>
        /// 是否为销售订单.
        /// </summary>
        [Reactive]
        public bool IsSell { get; set; }

        /// <summary>
        /// 发给对方的消息.
        /// </summary>
        [Reactive]
        public string Message { get; set; }

        /// <summary>
        /// 是否为 Mod.
        /// </summary>
        [Reactive]
        public bool IsMod { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is ItemOrderViewModel model && EqualityComparer<ItemOrder>.Default.Equals(Data, model.Data);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Data);

        private void Initialize(ItemOrder order)
        {
            var resourceToolkit = Locator.Current.GetService<IResourceToolkit>();
            Data = order;
            Status = order.User.Status switch
            {
                "offline" => resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.Offline),
                "online" => resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.Online),
                "ingame" => resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.InGame),
                _ => resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.Unknown),
            };
            OrderTypeText = order.OrderType == "sell"
                ? "WTS"
                : "WTB";
            IsSell = order.OrderType == "sell";
            IsMod = order.ModRank != null;

            var sp = _targetItem.Identifier.Split("_").ToList();
            for (var i = 0; i < sp.Count; i++)
            {
                sp[i] = sp[i].Substring(0, 1).ToUpper() + sp[i].Substring(1);
            }

            var name = string.Join(' ', sp);

            Message = IsSell
                ? $"/w {Data.User.GameName} Hi! I want to buy: {name} for {Data.Platinum} platinum. (warframe.market)"
                : $"/w {Data.User.GameName} Hi! I want to sell: {name} for {Data.Platinum} platinum. (warframe.market)";
        }

        private async Task GotoProfileAsync()
            => await Launcher.LaunchUriAsync(new Uri($"https://warframe.market/profile/{Data.User.GameName}"));

        private void CopyMessage()
        {
            var dp = new DataPackage();
            dp.SetText(Message);
            Clipboard.SetContent(dp);
            var resourceToolkit = Locator.Current.GetService<IResourceToolkit>();
            var appVM = Locator.Current.GetService<AppViewModel>();
            appVM.ShowTip(resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.MarketMessageCopied), Models.Enums.InfoType.Success);
        }
    }
}
