// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// 紫卡订单视图模型.
    /// </summary>
    public sealed class RivenOrderViewModel : ViewModelBase
    {
        private readonly List<RivenAttribute> _attributes;
        private readonly RivenWeapon _weapon;

        /// <summary>
        /// Initializes a new instance of the <see cref="RivenOrderViewModel"/> class.
        /// </summary>
        public RivenOrderViewModel(AuctionRivenOrder order, List<RivenAttribute> attributes, RivenWeapon weapon)
        {
            PositiveAttributes = new ObservableCollection<string>();
            NegativeAttributes = new ObservableCollection<string>();
            _weapon = weapon;
            _attributes = attributes;
            Initialize(order);
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
        public AuctionRivenOrder Data { get; set; }

        /// <summary>
        /// 用户当前状态.
        /// </summary>
        [Reactive]
        public string Status { get; set; }

        /// <summary>
        /// 增益属性.
        /// </summary>
        public ObservableCollection<string> PositiveAttributes { get; }

        /// <summary>
        /// 减益属性.
        /// </summary>
        public ObservableCollection<string> NegativeAttributes { get; }

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

        /// <summary>
        /// 名称.
        /// </summary>
        [Reactive]
        public string Name { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is RivenOrderViewModel model && EqualityComparer<AuctionRivenOrder>.Default.Equals(Data, model.Data);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Data);

        private void Initialize(AuctionRivenOrder order)
        {
            Name = $"{_weapon.Name} {order.Item.Name}";
            var resourceToolkit = Locator.Current.GetService<IResourceToolkit>();
            Data = order;
            Status = order.Owner.Status switch
            {
                "offline" => resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.Offline),
                "online" => resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.Online),
                "ingame" => resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.InGame),
                _ => resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.Unknown),
            };

            if (order.Item.Attributes?.Any() ?? false)
            {
                foreach (var attr in order.Item.Attributes)
                {
                    var sourceAttr = _attributes.FirstOrDefault(p => p.Identifier == attr.Identifier);
                    if (sourceAttr == null)
                    {
                        continue;
                    }

                    var prefix = attr.Value > 0 ? "+" : string.Empty;
                    var text = $"{prefix}{attr.Value}% {sourceAttr.Effect}";
                    if (attr.IsPositive)
                    {
                        PositiveAttributes.Add(text);
                    }
                    else
                    {
                        NegativeAttributes.Add(text);
                    }
                }
            }

            BuyoutPrice = order.BuyoutPrice == null || order.BuyoutPrice.Value <= 0 ? string.Empty : order.BuyoutPrice.ToString();
            StartPrice = order.StartingPrice <= 0 || order.IsDirectSell ? string.Empty : order.StartingPrice.ToString();
        }

        private async Task GotoProfileAsync()
            => await Launcher.LaunchUriAsync(new Uri($"https://warframe.market/profile/{Data.Owner.GameName}"));

        private async Task OpenOrderAsync()
            => await Launcher.LaunchUriAsync(new Uri($"https://warframe.market/auction/{Data.Id}"));
    }
}
