// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReactiveUI;
using Wfa.Models.Data.Local;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.Interfaces;
using Windows.System;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 帮助支持的视图模型.
    /// </summary>
    public sealed partial class HelpPageViewModel : ViewModelBase, IPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpPageViewModel"/> class.
        /// </summary>
        public HelpPageViewModel(
            IFileToolkit fileToolkit,
            IAppToolkit appToolkit)
        {
            _appToolkit = appToolkit;
            _fileToolkit = fileToolkit;

            QuestionCollection = new ObservableCollection<QuestionModule>();
            LinkCollection = new ObservableCollection<KeyValue>();
            InitializeLinks();

            AskIssueCommand = ReactiveCommand.CreateFromTask(AskIssueAsync);
            GotoProjectHomeCommand = ReactiveCommand.CreateFromTask(GotoProjectHomeAsync);
            GotoDeveloperBiliBiliHomePageCommand = ReactiveCommand.CreateFromTask(GotoDeveloperBiliBiliHomePageAsync);
            ActiveCommand = ReactiveCommand.CreateFromTask(InitializeQuestionsAsync);
            DeactiveCommand = ReactiveCommand.Create(() => { });
        }

        /// <summary>
        /// 初始化问题.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        private async Task InitializeQuestionsAsync()
        {
            if (QuestionCollection.Count > 0)
            {
                return;
            }

            var languageCode = _appToolkit.GetLanguageCode(true);
            languageCode = languageCode.Contains("zh")
                ? languageCode.Contains("cn", StringComparison.OrdinalIgnoreCase) || languageCode.Contains("hans", StringComparison.OrdinalIgnoreCase)
                    ? "zh-CHS"
                    : "zh-CHT"
                : "en-US";

            var dataStr = await _fileToolkit.GetFileContentFromLocalPath($"ms-appx:///Resources/Misc/Question.{languageCode}.json");
            if (!string.IsNullOrEmpty(dataStr))
            {
                var data = JsonConvert.DeserializeObject<List<QuestionModule>>(dataStr);
                data.ForEach(p => QuestionCollection.Add(p));
                await Task.Delay(100);
                CurrentQuestionModule = QuestionCollection.FirstOrDefault();
            }
        }

        private void InitializeLinks()
        {
            var links = new List<KeyValue>
            {
                new KeyValue("Warframe Community Developers", "https://github.com/WFCD"),
                new KeyValue("Warframe Market", "https://warframe.market/"),
                new KeyValue("Warframe 中文维基", "https://warframe.huijiwiki.com/wiki/Mainpage"),
                new KeyValue("Fandom Wiki", "https://warframe.fandom.com/wiki/WARFRAME_Wiki"),
                new KeyValue("Windows UI Library", "https://github.com/microsoft/microsoft-ui-xaml"),
                new KeyValue("Win2D", "https://github.com/microsoft/Win2D"),
                new KeyValue("Windows Community Toolkit", "https://github.com/CommunityToolkit/WindowsCommunityToolkit"),
                new KeyValue("ReactiveUI", "https://www.reactiveui.net/"),
                new KeyValue("Fluent UI System Icons", "https://github.com/microsoft/fluentui-system-icons"),
            };

            links.ForEach(p => LinkCollection.Add(p));
        }

        private async Task AskIssueAsync()
            => await Launcher.LaunchUriAsync(new Uri("https://github.com/Richasy/Wfa.Uwp/issues/new/choose")).AsTask();

        private async Task GotoProjectHomeAsync()
            => await Launcher.LaunchUriAsync(new Uri("https://github.com/Richasy/Wfa.Uwp/")).AsTask();

        private async Task GotoDeveloperBiliBiliHomePageAsync()
            => await Launcher.LaunchUriAsync(new Uri("https://space.bilibili.com/5992670")).AsTask();
    }
}
