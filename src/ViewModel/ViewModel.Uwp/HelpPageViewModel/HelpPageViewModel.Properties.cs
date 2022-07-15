// Copyright (c) Richasy. All rights reserved.

using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.Data.Local;
using Wfa.Toolkit.Interfaces;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 帮助支持的视图模型.
    /// </summary>
    public sealed partial class HelpPageViewModel
    {
        private readonly IFileToolkit _fileToolkit;
        private readonly IAppToolkit _appToolkit;

        /// <summary>
        /// 关联链接集合.
        /// </summary>
        public ObservableCollection<KeyValue> LinkCollection { get; }

        /// <summary>
        /// 问题集合.
        /// </summary>
        public ObservableCollection<QuestionModule> QuestionCollection { get; }

        /// <summary>
        /// 当前问题模块.
        /// </summary>
        [Reactive]
        public QuestionModule CurrentQuestionModule { get; set; }

        /// <summary>
        /// 报告问题命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> AskIssueCommand { get; }

        /// <summary>
        /// 打开项目主页命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> GotoProjectHomeCommand { get; }

        /// <summary>
        /// 打开开发者B站账户页面命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> GotoDeveloperBiliBiliHomePageCommand { get; }

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> ActiveCommand { get; }

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> DeactiveCommand { get; }
    }
}
