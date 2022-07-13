// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Wfa.Models.Community;
using Wfa.Models.Data.Context;
using Wfa.Models.Enums;
using Wfa.Provider.Interfaces;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.Interfaces;
using Wfa.ViewModel.LibraryItems;
using Windows.UI.Core;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 资料库详情页面视图模型.
    /// </summary>
    public sealed partial class LibraryDetailPageViewModel : ViewModelBase, IPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryDetailPageViewModel"/> class.
        /// </summary>
        public LibraryDetailPageViewModel(
            IResourceToolkit resourceToolkit,
            ICommunityProvider communityProvider,
            LibraryDbContext dbContext,
            CoreDispatcher dispatcher)
        {
            _resourceToolkit = resourceToolkit;
            _communityProvider = communityProvider;
            _dbContext = dbContext;
            _dispatcher = dispatcher;

            Items = new ObservableCollection<LibraryItemViewModel>();

            ActiveCommand = ReactiveCommand.CreateFromTask(ActiveAsync);
            DeactiveCommand = ReactiveCommand.Create(Deactive);

            _isLoading = ActiveCommand.IsExecuting.ToProperty(this, x => x.IsLoading);
            ActiveCommand.ThrownExceptions.Subscribe(LogException);
        }

        /// <summary>
        /// 设置数据.
        /// </summary>
        /// <param name="data">数据.</param>
        public void SetDataType(LibrarySectionViewModel data)
        {
            _type = data.Type;
            Title = data.Title;
        }

        private async Task ActiveAsync()
        {
            TryClear(Items);
            var task = Task.Run(async () =>
            {
                await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    switch (_type)
                    {
                        case CommunityDataType.Warframe:
                            var warframes = await _communityProvider.GetDataListAsync<Warframe>(_type);
                            warframes.OrderBy(p => p.Name).ToList().ForEach(p => Items.Add(new LibraryItemViewModel(p)));
                            break;
                        case CommunityDataType.ArchGun:
                            var archguns = await _communityProvider.GetDataListAsync<ArchGun>(_type);
                            archguns.OrderBy(p => p.Name).ToList().ForEach(p => Items.Add(new LibraryItemViewModel(p)));
                            break;
                        case CommunityDataType.ArchMelee:
                            var archMelees = await _communityProvider.GetDataListAsync<ArchMelee>(_type);
                            archMelees.OrderBy(p => p.Name).ToList().ForEach(p => Items.Add(new LibraryItemViewModel(p)));
                            break;
                        case CommunityDataType.Archwing:
                            var archwings = await _communityProvider.GetDataListAsync<Archwing>(_type);
                            archwings.OrderBy(p => p.Name).ToList().ForEach(p => Items.Add(new LibraryItemViewModel(p)));
                            break;
                        case CommunityDataType.Melee:
                            var melees = await _communityProvider.GetDataListAsync<Melee>(_type);
                            melees.OrderBy(p => p.Name).ToList().ForEach(p => Items.Add(new LibraryItemViewModel(p)));
                            break;
                        case CommunityDataType.Primary:
                            var primaries = await _communityProvider.GetDataListAsync<Primary>(_type);
                            primaries.OrderBy(p => p.Name).ToList().ForEach(p => Items.Add(new LibraryItemViewModel(p)));
                            break;
                        case CommunityDataType.Secondary:
                            var secondaries = await _communityProvider.GetDataListAsync<Secondary>(_type);
                            secondaries.OrderBy(p => p.Name).ToList().ForEach(p => Items.Add(new LibraryItemViewModel(p)));
                            break;
                        case CommunityDataType.Mod:
                            var mods = await _communityProvider.GetDataListAsync<Mod>(_type);
                            mods.OrderBy(p => p.Name).Distinct().ToList().ForEach(p => Items.Add(new LibraryItemViewModel(p)));
                            break;
                        default:
                            break;
                    }
                }).AsTask();
            });

            await RunDelayTask(task);
            IsEmpty = Items.Count == 0;
        }

        private void Deactive() => TryClear(Items);
    }
}
