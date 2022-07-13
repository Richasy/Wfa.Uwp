// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Wfa.Models.Community;
using Wfa.Models.Data.Context;
using Wfa.Models.Data.Local;
using Wfa.Models.Enums;
using Wfa.Toolkit.Interfaces;

namespace Wfa.ViewModel.Base
{
    /// <summary>
    /// 枪械视图模型基类.
    /// </summary>
    /// <typeparam name="T">枪械数据.</typeparam>
    public class GunViewModelBase<T> : EntryViewModelBase<T>
        where T : GunBase
    {
        private readonly IResourceToolkit _resourceToolkit;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArchwingItemViewModel"/> class.
        /// </summary>
        public GunViewModelBase(
            LibraryDbContext dbContext,
            NavigationViewModel navigationViewModel,
            IResourceToolkit resourceToolkit)
            : base(dbContext, navigationViewModel)
        {
            _resourceToolkit = resourceToolkit;
            Properties = new ObservableCollection<KeyValue>();
        }

        /// <summary>
        /// 数值集合.
        /// </summary>
        public ObservableCollection<KeyValue> Properties { get; }

        /// <inheritdoc/>
        protected override async Task InitializeAsync(T data)
        {
            AddProeprty(LanguageNames.Accuracy, data.Accuracy);
            AddProeprty(LanguageNames.Ammo, data.Ammo);
            AddProeprty(LanguageNames.CriticalChance, $"{Math.Round(data.CriticalChance * 100)}%");
            AddProeprty(LanguageNames.StatusChance, $"{Math.Round(data.ProcChance * 100)}%");
            AddProeprty(LanguageNames.CriticalMultiplier, $"{Math.Round(data.CriticalMultiplier, 1)}x");
            AddProeprty(LanguageNames.FireRate, Math.Round(data.FireRate));
            AddProeprty(LanguageNames.MagazineSize, data.MagazineSize);
            await base.InitializeAsync(data);
        }

        private void AddProeprty(LanguageNames key, object value)
        {
            if (value == null)
            {
                return;
            }

            var keyStr = _resourceToolkit.GetLocaleString(key);
            Properties.Add(new KeyValue(keyStr, value.ToString()));
        }
    }
}
