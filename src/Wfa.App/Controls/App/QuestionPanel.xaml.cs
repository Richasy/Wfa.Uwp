// Copyright (c) Richasy. All rights reserved.

using ReactiveUI;
using Splat;
using Wfa.ViewModel;

namespace Wfa.App.Controls.App
{
    /// <summary>
    /// 问题面板.
    /// </summary>
    public sealed partial class QuestionPanel : QuestionPanelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionPanel"/> class.
        /// </summary>
        public QuestionPanel()
        {
            InitializeComponent();
            ViewModel = Locator.Current.GetService<HelpPageViewModel>();
        }
    }

    /// <summary>
    /// <see cref="QuestionPanel"/> 的基类.
    /// </summary>
    public class QuestionPanelBase : ReactiveUserControl<HelpPageViewModel>
    {
    }
}
