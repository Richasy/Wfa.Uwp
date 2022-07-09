// Copyright (c) Richasy. All rights reserved.

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Wfa.App.Controls.State
{
    /// <summary>
    /// 新闻面板.
    /// </summary>
    public sealed partial class NewsPanel : UserControl
    {
        /// <summary>
        /// <see cref="ItemsSource"/> 的依赖属性.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(NewsPanel), new PropertyMetadata(default));

        /// <summary>
        /// <see cref="NewsCount"/> 的依赖属性.
        /// </summary>
        public static readonly DependencyProperty NewsCountProperty =
            DependencyProperty.Register(nameof(NewsCount), typeof(int), typeof(NewsPanel), new PropertyMetadata(0));

        /// <summary>
        /// <see cref="ImageHeight"/> 的依赖属性.
        /// </summary>
        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register(nameof(ImageHeight), typeof(double), typeof(NewsPanel), new PropertyMetadata(160d));

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsPanel"/> class.
        /// </summary>
        public NewsPanel() => InitializeComponent();

        /// <summary>
        /// 数据源.
        /// </summary>
        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// 新闻数.
        /// </summary>
        public int NewsCount
        {
            get { return (int)GetValue(NewsCountProperty); }
            set { SetValue(NewsCountProperty, value); }
        }

        /// <summary>
        /// 图片高度.
        /// </summary>
        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }
    }
}
