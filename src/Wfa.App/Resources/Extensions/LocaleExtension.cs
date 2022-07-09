// Copyright (c) Richasy. All rights reserved.

using Splat;
using Wfa.Models.Enums;
using Wfa.Toolkit.Interfaces;
using Windows.UI.Xaml.Markup;

namespace Wfa.App.Resources.Extensions
{
    /// <summary>
    /// Localized text extension.
    /// </summary>
    [MarkupExtensionReturnType(ReturnType = typeof(string))]
    public sealed class LocaleExtension : MarkupExtension
    {
        /// <summary>
        /// Language name.
        /// </summary>
        public LanguageNames Name { get; set; }

        /// <inheritdoc/>
        protected override object ProvideValue()
        {
            return Locator.Current.GetService<IResourceToolkit>()
                                          .GetLocaleString(Name);
        }
    }
}
