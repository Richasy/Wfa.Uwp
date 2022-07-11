// Copyright (c) Richasy. All rights reserved.

using System;
using Splat;
using Wfa.Models.Data.Constants;
using Wfa.Toolkit.Interfaces;
using Windows.UI.Xaml.Data;

namespace Wfa.App.Resources.Converters
{
    /// <summary>
    /// 主题文本转换器.
    /// </summary>
    public class ThemeStringConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var themeStr = value.ToString();
            var resourceToolkit = Locator.Current.GetService<IResourceToolkit>();
            var result = themeStr switch
            {
                AppConstants.ThemeLight => resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.Light),
                AppConstants.ThemeDark => resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.Dark),
                _ => resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.FollowSystem),
            };
            return result;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
