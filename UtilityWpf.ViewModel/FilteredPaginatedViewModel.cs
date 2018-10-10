
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using System.Reactive.Subjects;
using DynamicData.Binding;



namespace UtilityWpf.ViewModel
{
    public class FilteredPaginatedViewModel<T> : AbstractNotifyPropertyChanged
    {

        private ReadOnlyObservableCollection<T> pitems;
        private ReadOnlyObservableCollection<T> allitems;
        private ReadOnlyObservableCollection<T> fitems;

        public ReadOnlyObservableCollection<T> PaginatedItems
        {
            get { return pitems; }
            set { pitems = value; }
        }

        public ReadOnlyObservableCollection<T> AllItems
        {
            get { return allitems; }
            set { allitems = value; }
        }

        private IObservable<IChangeSet<T>> items;

        public ReadOnlyObservableCollection<T> FilteredItems
        {
            get => fitems;
            set => SetAndRaise(ref fitems, value);
        }

        public PageNavigatorViewModel PageNavigatorVM { get; }
        public ReactiveProperty<int> CurrentPage { get; } = new ReactiveProperty<int>(1);
        public ReactiveProperty<int> PageSize { get; }

        object lck = new object();



        public FilteredPaginatedViewModel(IObservable<T> obs, IObservable<Func<T, bool>> filter, IScheduler s, int initialsize = 25)
        {

            var x = obs.ToObservableChangeSet();

            PageSize = new ReactiveProperty<int>(initialsize);
            PageNavigatorVM = new PageNavigatorViewModel(CurrentPage, PageSize);

            IObservable<PageRequest> request = PageNavigatorVM.Output
                 .DistinctUntilChanged()
                    .ObserveOn(s);


            items = x.Synchronize(lck).DisposeMany()
                  .ObserveOn(s);


            items.Filter(filter)
                .Bind(out fitems)
                .DisposeMany()
                .Subscribe();

            items.Filter(filter).Page(request)
                           .Do(_ =>
                           {
                               lock (lck)
                               {
                                   CurrentPage.Value = ((IPageChangeSet<T>)_).Response.TotalSize;
                                   PageSize.Value = ((IPageChangeSet<T>)_).Response.PageSize;
                               }
                           })

                           .Bind(out pitems)
         .DisposeMany()
              .Subscribe();



            x.Synchronize(lck).DisposeMany()
                    .ObserveOn(s)
                                 //.Page(request)
                                 .Bind(out allitems)
                    .Subscribe();

        }

    }




}