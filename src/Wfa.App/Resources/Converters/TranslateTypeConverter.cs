// Copyright (c) Richasy. All rights reserved.

using System;
using Splat;
using Wfa.Models.Enums;
using Wfa.Toolkit.Interfaces;
using Windows.UI.Xaml.Data;

namespace Wfa.App.Resources.Converters
{
    internal sealed class TranslateTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is TranslateType type)
            {
                var resourceToolkit = Locator.Current.GetService<IResourceToolkit>();
                return type switch
                {
                    TranslateType.ZhToEn => resourceToolkit.GetLocaleString(LanguageNames.ZhToEn),
                    TranslateType.EnToZh => resourceToolkit.GetLocaleString(LanguageNames.EnToZh),
                    _ => throw new NotImplementedException(),
                };
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
