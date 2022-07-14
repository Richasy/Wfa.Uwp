// Copyright (c) Richasy. All rights reserved.

using System;
using Wfa.Models.Enums;
using Windows.UI.Xaml.Data;

namespace Wfa.App.Resources.Converters
{
    internal sealed class PolarityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value is string str
                ? str.ToLower() switch
                {
                    "madurai" => WfaSymbol.Madurai,
                    "vazarin" => WfaSymbol.Vazarin,
                    "naramon" => WfaSymbol.Naramon,
                    "zenurik" => WfaSymbol.Zenurik,
                    "penjaga" => WfaSymbol.Penjaga,
                    "knoeksi" => WfaSymbol.Koneksi,
                    "unairu" => WfaSymbol.Unairu,
                    "Umbra" => WfaSymbol.Umbra,
                    _ => WfaSymbol.Arcane,
                }
                : WfaSymbol.Arcane;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
