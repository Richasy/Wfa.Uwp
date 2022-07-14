// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Wfa.Models.Community;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.LibraryItems
{
    /// <summary>
    /// 资料条目视图模型.
    /// </summary>
    public sealed class LibraryItemViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryItemViewModel"/> class.
        /// </summary>
        public LibraryItemViewModel(EntryBase data)
        {
            Data = data;
            Image = data.ImageName ?? string.Empty;
            Name = data.Name;

            ShowDetailCommand = ReactiveCommand.Create(ShowDetail);
        }

        /// <summary>
        /// 源数据.
        /// </summary>
        public EntryBase Data { get; }

        /// <summary>
        /// 显示详情的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> ShowDetailCommand { get; }

        /// <summary>
        /// 条目图片.
        /// </summary>
        [Reactive]
        public string Image { get; set; }

        /// <summary>
        /// 条目名.
        /// </summary>
        [Reactive]
        public string Name { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is LibraryItemViewModel model && EqualityComparer<EntryBase>.Default.Equals(Data, model.Data);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Data);

        private void ShowDetail()
        {
            var appVM = Locator.Current.GetService<AppViewModel>();
            appVM.ShowLibraryItem(Data);
        }
    }
}
