// Copyright (c) Richasy. All rights reserved.

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Wfa.App.Controls.State
{
    /// <summary>
    /// 游戏活动面板.
    /// </summary>
    public sealed partial class EventsPanel : UserControl
    {
        /// <summary>
        /// <see cref="ItemsSource"/> 的依赖属性.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(EventsPanel), new PropertyMetadata(default));

        /// <summary>
        /// <see cref="IsEmpty"/> 的依赖属性.
        /// </summary>
        public static readonly DependencyProperty IsEmptyProperty =
            DependencyProperty.Register(nameof(IsEmpty), typeof(bool), typeof(EventsPanel), new PropertyMetadata(default));

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsPanel"/> class.
        /// </summary>
        public EventsPanel() => InitializeComponent();

        /// <summary>
        /// 数据源.
        /// </summary>
        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// 活动是否为空.
        /// </summary>
        public bool IsEmpty
        {
            get { return (bool)GetValue(IsEmptyProperty); }
            set { SetValue(IsEmptyProperty, value); }
        }
    }
}
