// Copyright (c) Richasy. All rights reserved.

using System;
using Richasy.FluentIcon.Uwp;
using Wfa.Models.Enums;
using Windows.UI.Xaml.Data;

namespace Wfa.App.Resources.Converters
{
    /// <summary>
    /// 工具图标转换器.
    /// </summary>
    internal sealed class ToolIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is ToolType type)
            {
                return type switch
                {
                    ToolType.Translate => RegularFluentSymbol.ArrowRepeatAll16,
                    ToolType.FriendLinks => RegularFluentSymbol.Link16,
                    ToolType.BiliSearch => RegularFluentSymbol.Tv16,
                    _ => throw new NotImplementedException(),
                };
            }

            return RegularFluentSymbol.Dismiss16;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
