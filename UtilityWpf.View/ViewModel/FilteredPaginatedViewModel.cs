
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
using DynamicData.Operators;

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


        object lck = new object();


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