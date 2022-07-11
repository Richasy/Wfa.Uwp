// Copyright (c) Richasy. All rights reserved.

using System;
using Splat;
using Wfa.Models.Enums;
using Wfa.Toolkit.Interfaces;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Wfa.App.Resources.Converters
{
    internal sealed class FactionColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var resourceToolkit = Locator.Current.GetService<IResourceToolkit>();
            return value is WfaSymbol symbol
                ? symbol switch
                {
                    WfaSymbol.Grineer => resourceToolkit.GetResource<Brush>("GrineerGreenGradient"),
                    WfaSymbol.Corpus => resourceToolkit.GetResource<Brush>("CorpusGradient"),
                    WfaSymbol.Infested => resourceToolkit.GetResource<Brush>("InfestedRedGradient"),
                    WfaSymbol.Orikin => resourceToolkit.GetResource<Brush>("OrikinGradient"),
                    WfaSymbol.Ostron => resourceToolkit.GetResource<Brush>("OrikinGradient"),
                    WfaSymbol.Solaris => resourceToolkit.GetResource<Brush>("CorpusGradient"),
                    _ => resourceToolkit.GetResource<Brush>("SentientGradient"),
                }
                : (object)resourceToolkit.GetResource<Brush>("SentientGradient");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
