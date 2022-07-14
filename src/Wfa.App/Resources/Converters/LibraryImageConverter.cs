// Copyright (c) Richasy. All rights reserved.

using System;
using Windows.UI.Xaml.Data;

namespace Wfa.App.Resources.Converters
{
    internal sealed class LibraryImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
            => $"https://cdn.warframestat.us/img/{value}";

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
