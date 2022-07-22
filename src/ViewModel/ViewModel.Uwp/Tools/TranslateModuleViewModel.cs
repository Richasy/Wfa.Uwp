// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.Data.Center;
using Wfa.Models.Data.Constants;
using Wfa.Models.Data.Context;
using Wfa.Models.Enums;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.Tools;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 翻译模块视图模型.
    /// </summary>
    public sealed partial class TranslateModuleViewModel : ViewModelBase
    {
        private readonly LibraryDbContext _dbContext;
        private readonly ObservableAsPropertyHelper<bool> _isLoading;
        private List<Translate> _localDicts;
        private string _language;

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateModuleViewModel"/> class.
        /// </summary>
        public TranslateModuleViewModel(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
            Translates = new ObservableCollection<TranslateItemViewModel>();
            Types = new ObservableCollection<TranslateType> { TranslateType.ZhToEn, TranslateType.EnToZh };

            ActiveCommand = ReactiveCommand.CreateFromTask(ActiveAsync);
            _isLoading = ActiveCommand.IsExecuting.ToProperty(this, x => x.IsLoading);

            this.WhenAnyValue(x => x.Keyword)
                .Subscribe(Translate);
        }

        /// <summary>
        /// 激活命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> ActiveCommand { get; }

        /// <summary>
        /// 翻译待选集合.
        /// </summary>
        public ObservableCollection<TranslateItemViewModel> Translates { get; }

        /// <summary>
        /// 翻译类型集合.
        /// </summary>
        public ObservableCollection<TranslateType> Types { get; }

        /// <summary>
        /// 关键词.
        /// </summary>
        [Reactive]
        public string Keyword { get; set; }

        /// <summary>
        /// 翻译类型.
        /// </summary>
        [Reactive]
        public TranslateType TranslateType { get; set; }

        /// <summary>
        /// 是否正在加载中.
        /// </summary>
        public bool IsLoading => _isLoading.Value;

        private async Task ActiveAsync()
        {
            if (_localDicts == null)
            {
                _language = (await _dbContext.Metas.FirstOrDefaultAsync(p => p.Name == AppConstants.LanguageKey)).Value;
                TranslateType = _language == AppConstants.LanguageEn
                    ? TranslateType.EnToZh
                    : TranslateType.ZhToEn;
                _localDicts = await _dbContext.Translates.ToListAsync();
            }
        }

        private void Translate(string keyword)
        {
            TryClear(Translates);
            if (!(_localDicts?.Any() ?? false) || string.IsNullOrEmpty(keyword))
            {
                return;
            }

            IEnumerable<Translate> translates = null;
            translates = TranslateType == TranslateType.EnToZh
                ? _localDicts.Where(p => p.En.Contains(keyword, System.StringComparison.OrdinalIgnoreCase))
                : _localDicts.Where(p => p.Zh.Contains(keyword, System.StringComparison.OrdinalIgnoreCase));

            if (translates.Any())
            {
                foreach (var item in translates)
                {
                    var tipText = TranslateType == TranslateType.EnToZh
                        ? item.En
                        : item.Zh;
                    var primaryText = TranslateType == TranslateType.EnToZh
                        ? item.Zh
                        : item.En;
                    Translates.Add(new TranslateItemViewModel(tipText, primaryText));
                }
            }
        }
    }
}
