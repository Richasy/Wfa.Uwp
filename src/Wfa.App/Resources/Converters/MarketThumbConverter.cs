// Copyright (c) Richasy. All rights reserved.

using System;
using Windows.UI.Xaml.Data;

namespace Wfa.App.Resources.Converters
{
    internal sealed class MarketThumbConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
            => value == null || string.IsNullOrEmpty(value?.ToString()) ? null : $"https://warframe.market/static/assets/{value}";

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
