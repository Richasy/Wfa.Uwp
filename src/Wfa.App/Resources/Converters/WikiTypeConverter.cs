// Copyright (c) Richasy. All rights reserved.

using System;
using Splat;
using Wfa.Models.Enums;
using Wfa.Toolkit.Interfaces;
using Windows.UI.Xaml.Data;

namespace Wfa.App.Resources.Converters
{
    internal sealed class WikiTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var resourceToolkit = Locator.Current.GetService<IResourceToolkit>();
            if (value is WikiType w)
            {
                return w switch
                {
                    WikiType.Fandom => resourceToolkit.GetLocaleString(LanguageNames.FandomWiki),
                    _ => resourceToolkit.GetLocaleString(LanguageNames.HuijiWiki),
                };
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
