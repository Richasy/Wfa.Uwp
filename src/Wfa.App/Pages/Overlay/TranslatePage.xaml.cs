// Copyright (c) Richasy. All rights reserved.

using System;
using Wfa.ViewModel;
using Windows.UI.Xaml;

namespace Wfa.App.Pages.Overlay
{
    /// <summary>
    /// 翻译页面.
    /// </summary>
    public sealed partial class TranslatePage : TranslatePageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TranslatePage"/> class.
        /// </summary>
        public TranslatePage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
            => ViewModel.ActiveCommand.Execute().Subscribe();
    }

    /// <summary>
    /// <see cref="TranslatePage"/> 的基类.
    /// </summary>
    public class TranslatePageBase : AppPage<TranslateModuleViewModel>
    {
    }
}
