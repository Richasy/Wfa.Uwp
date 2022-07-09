// Copyright (c) Richasy. All rights reserved.

using Wfa.Models.Enums;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Wfa.App.Controls.App
{
    /// <summary>
    /// 包含部分 Warframe icon 的图标.
    /// </summary>
    public sealed class WfaIcon : FontIcon
    {
        /// <summary>
        /// Dependency property of <see cref="Symbol"/>.
        /// </summary>
        public static readonly DependencyProperty SymbolProperty =
            DependencyProperty.Register(nameof(Symbol), typeof(WfaSymbol), typeof(WfaIcon), new PropertyMetadata(default, new PropertyChangedCallback(OnSymbolChanged)));

        /// <summary>
        /// Initializes a new instance of the <see cref="WfaIcon"/> class.
        /// </summary>
        public WfaIcon()
            => FontFamily = new Windows.UI.Xaml.Media.FontFamily("ms-appx:///Assets/Wfa.ttf#Wfa");

        /// <summary>
        /// Symbol enumeration value corresponding to the icon.
        /// </summary>
        public WfaSymbol Symbol
        {
            get { return (WfaSymbol)GetValue(SymbolProperty); }
            set { SetValue(SymbolProperty, value); }
        }

        private static void OnSymbolChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is WfaSymbol symbol)
            {
                var instance = d as WfaIcon;
                instance.Glyph = ((char)symbol).ToString();
            }
        }
    }
}
