// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.Community;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.Items
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
            Image = data.WikiaThumbnail ?? string.Empty;
            Name = data.Name;
        }

        /// <summary>
        /// 源数据.
        /// </summary>
        public object Data { get; }

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
        public override bool Equals(object obj) => obj is LibraryItemViewModel model && EqualityComparer<object>.Default.Equals(Data, model.Data);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Data);
    }
}
