// Copyright (c) Richasy. All rights reserved.

using System;
using System.Reactive;
using ReactiveUI;
using Splat;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;
using Windows.ApplicationModel.DataTransfer;

namespace Wfa.ViewModel.Tools
{
    /// <summary>
    /// 翻译条目视图模型.
    /// </summary>
    public sealed class TranslateItemViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateItemViewModel"/> class.
        /// </summary>
        public TranslateItemViewModel(string tipText, string primaryText)
        {
            TipText = tipText;
            PrimaryText = primaryText;
            CopyCommand = ReactiveCommand.Create(Copy);
        }

        /// <summary>
        /// 复制文本的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> CopyCommand { get; }

        /// <summary>
        /// 搜索的文本.
        /// </summary>
        public string TipText { get; set; }

        /// <summary>
        /// 翻译后的文本.
        /// </summary>
        public string PrimaryText { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is TranslateItemViewModel model && PrimaryText == model.PrimaryText;

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(PrimaryText);

        private void Copy()
        {
            var dp = new DataPackage();
            dp.SetText(PrimaryText);
            Clipboard.SetContent(dp);
            var appVM = Locator.Current.GetService<AppViewModel>();
            var resourceToolkit = Locator.Current.GetService<IResourceToolkit>();
            appVM.ShowTip(resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.Copied), Models.Enums.InfoType.Success);
        }
    }
}
