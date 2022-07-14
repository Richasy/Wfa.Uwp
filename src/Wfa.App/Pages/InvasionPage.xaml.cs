// Copyright (c) Richasy. All rights reserved.

using System;
using Wfa.ViewModel;
using Windows.UI.Xaml.Navigation;

namespace Wfa.App.Pages
{
    /// <summary>
    /// 入侵页面.
    /// </summary>
    public sealed partial class InvasionPage : InvasionPageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvasionPage"/> class.
        /// </summary>
        public InvasionPage() => InitializeComponent();
    }

    /// <summary>
    /// <see cref="InvasionPage"/> 的基类.
    /// </summary>
    public class InvasionPageBase : AppPage<InvasionPageViewModel>
    {
    }
}
