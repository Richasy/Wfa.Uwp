// Copyright (c) Richasy. All rights reserved.

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Wfa.App.Controls.State
{
    /// <summary>
    /// 世界周期面板.
    /// </summary>
    public sealed partial class WorldCyclePanel : UserControl
    {
        /// <summary>
        /// <see cref="ItemsSource"/> 的依赖属性.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(WorldCyclePanel), new PropertyMetadata(default));

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldCyclePanel"/> class.
        /// </summary>
        public WorldCyclePanel() => InitializeComponent();

        /// <summary>
        /// 数据源.
        /// </summary>
        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
    }
}
