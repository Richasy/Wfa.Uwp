// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;
using Wfa.Provider.Interfaces;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.Interfaces;
using Wfa.ViewModel.Items;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 入侵页面视图模型.
    /// </summary>
    public sealed partial class InvasionPageViewModel : ViewModelBase, IPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvasionPageViewModel"/> class.
        /// </summary>
        public InvasionPageViewModel(IStateProvider stateProvider)
        {
            _stateProvider = stateProvider;
            ActiveCommand = ReactiveCommand.Create(Active);
            DeactiveCommand = ReactiveCommand.Create(Deactive);
            Invasions = new ObservableCollection<InvasionItemViewModel>();
        }

        private void Active()
        {
            _stateProvider.StateChanged += OnStateChanged;
            InitializeData();
        }

        private void Deactive()
            => _stateProvider.StateChanged -= OnStateChanged;

        private void InitializeData()
        {
            var invasions = _stateProvider.GetInvasions();

            IsLoading = invasions == null;
            if (!(invasions?.Any() ?? false))
            {
                IsLoading = invasions == null;
                IsEmpty = invasions != null && invasions.Count() == 0;
                return;
            }

            var newsCount = invasions.Count(p => !Invasions.Any(j => j.Id == p.Id));
            if (newsCount > 0)
            {
                TryClear(Invasions);
                invasions.ToList().ForEach(p => Invasions.Add(new InvasionItemViewModel(p)));
            }
            else
            {
                foreach (var item in invasions)
                {
                    var source = Invasions.FirstOrDefault(p => p.Id == item.Id);
                    source?.UpdateDataCommand?.Execute(item).Subscribe();
                }
            }
        }

        private void OnStateChanged(object sender, EventArgs e)
            => InitializeData();
    }
}
