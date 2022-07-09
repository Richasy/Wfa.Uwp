// Copyright (c) Richasy. All rights reserved.

using System.Windows.Input;
using Splat;
using Wfa.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Wfa.App.Controls.App
{
    /// <summary>
    /// 应用页面头部.
    /// </summary>
    public sealed partial class AppPageHeader : UserControl
    {
        /// <summary>
        /// <see cref="Element"/> 的依赖属性.
        /// </summary>
        public static readonly DependencyProperty ElementProperty =
            DependencyProperty.Register(nameof(Element), typeof(object), typeof(AppPageHeader), new PropertyMetadata(default));

        /// <summary>
        /// <see cref="Title"/> 的依赖属性.
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(object), typeof(AppPageHeader), new PropertyMetadata(default));

        /// <summary>
        /// <see cref="RefreshCommand"/> 的依赖属性.
        /// </summary>
        public static readonly DependencyProperty RefreshCommandProperty =
            DependencyProperty.Register(nameof(RefreshCommand), typeof(ICommand), typeof(AppPageHeader), new PropertyMetadata(default));

        /// <summary>
        /// <see cref="IsShowLogo"/> 的依赖属性.
        /// </summary>
        public static readonly DependencyProperty IsShowLogoProperty =
            DependencyProperty.Register(nameof(IsShowLogo), typeof(bool), typeof(AppPageHeader), new PropertyMetadata(default));

        private readonly AppViewModel _appViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageHeader"/> class.
        /// </summary>
        public AppPageHeader()
        {
            this.InitializeComponent();
            _appViewModel = Splat.Locator.Current.GetService<AppViewModel>();
        }

        /// <summary>
        /// 标题.
        /// </summary>
        public object Title
        {
            get { return (object)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// 右侧的控制元素.
        /// </summary>
        public object Element
        {
            get { return (object)GetValue(ElementProperty); }
            set { SetValue(ElementProperty, value); }
        }

        /// <summary>
        /// 刷新命令.
        /// </summary>
        public ICommand RefreshCommand
        {
            get { return (ICommand)GetValue(RefreshCommandProperty); }
            set { SetValue(RefreshCommandProperty, value); }
        }

        /// <summary>
        /// 是否显示应用图标.
        /// </summary>
        public bool IsShowLogo
        {
            get { return (bool)GetValue(IsShowLogoProperty); }
            set { SetValue(IsShowLogoProperty, value); }
        }
    }
}
