﻿using DynamicData;
using DynamicData.Operators;
using Reactive.Bindings;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace UtilityWpf.ViewModel
{
    public class FilteredPaginatedViewModel<T> //: AbstractNotifyPropertyChanged
    {
        private ReadOnlyObservableCollection<T> pitems;

        public ReadOnlyObservableCollection<T> Items
        {
            get { return pitems; }
            set { pitems = value; }
        }

        public ReactiveProperty<IPageResponse> PageResponse { get; } = new ReactiveProperty<IPageResponse>();

        private object lck = new object();

        public FilteredPaginatedViewModel(IObservable<IChangeSet<T>> obs, IObservable<PageRequest> request, IObservable<Func<T, bool>> filter, IScheduler s)
        {
            obs.Synchronize(lck)
                  .Filter(filter)
                  .Page(request)
                           .Do(_ =>
                           {
                               lock (lck)
                               {
                                   PageResponse.Value = ((IPageChangeSet<T>)_).Response;
                               }
                           })
                           .Bind(out pitems)
                           .DisposeMany()
                           .Subscribe();
        }
    }
}