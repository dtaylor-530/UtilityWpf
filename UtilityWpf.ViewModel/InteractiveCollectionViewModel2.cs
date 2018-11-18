
using DynamicData;
using DynamicData.Binding;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using UtilityInterface;

namespace UtilityWpf.ViewModel
{

    public class InteractiveCollectionViewModel<T> : InteractiveCollectionBase<T>, ICollectionViewModel<IContainer<T>>
    {

        public InteractiveCollectionViewModel(IObservable<IChangeSet<T>> observable,
IObservable<Predicate<T>> invisiblefilter,
IObservable<Predicate<T>> enabledfilter,
IScheduler scheduler, string title = null)
        {

            observable.ObserveOn(scheduler)
           .Transform(s =>
           {
               var so = new SHDObject<T>(s ,invisiblefilter, enabledfilter);
               this.ReactToChanges(so);
               return (IContainer<T>)so;
           })
             .Bind(out _items)
            
             .DisposeMany()
             .Subscribe(
           _ =>
           Console.WriteLine("generic view model changed"),
                         ex =>
                         {
                             (Errors as ISubject<Exception>).OnNext(ex);
                             Console.WriteLine("Error in generic view model");
                         });

            Title = title;
    
        }

        public InteractiveCollectionViewModel(IObservable<T> observable, IScheduler scheduler,            string title = null)
        {
            observable.ToObservableChangeSet().ObserveOn(scheduler)
                .Transform
                (s =>
                {
                    var so = new SHDObject<T>(                       s );
                    this.ReactToChanges(so);
                    return (IContainer<T>)so;
                })
     .Bind(out _items)
         .DisposeMany()
           .Subscribe(
           _ => 
           Console.WriteLine("generic view model changed"),
                ex =>
                {
                    (Errors as ISubject<Exception>).OnNext(ex);
                    Console.WriteLine("Error in generic view model");
                });

            Title = title;
    

        }


        public InteractiveCollectionViewModel(IEnumerable<T> enumerable, System.Windows.Threading.Dispatcher dispatcher,    string title = null)
        {
            var xx = enumerable.Select
             (s =>
             {
                 var so = new SHDObject<T>(s);
                 return (IContainer<T>)so;
             }).ToList();

                foreach (var so in xx)
                    this.ReactToChanges((SHDObject<T>)so);

                _items=new ReadOnlyObservableCollection<IContainer<T>>(new ObservableCollection<IContainer<T>>(xx));
                
                Title = title;
    
        }



        public InteractiveCollectionViewModel(IEnumerable<T> enumerable,string childrenpath, IObservable<bool> ischecked, IObservable<bool> expand, System.Windows.Threading.Dispatcher dispatcher=null, string title = null)
        {
            var xx = enumerable.Select
             (s =>
             {
                 var so = new SEObject<T>(s, childrenpath,ischecked, expand, dispatcher);

                 return (IContainer<T>)so;
             }).ToList();

            foreach (var so in xx)
            {
                this.ReactToChanges((SEObject<T>)so);
                (so as SEObject<T>).OnPropertyChangeWithSource<SEObject<T>, KeyValuePair<T, InteractionArgs>>(nameof(SEObject<T>.ChildChanged)).Subscribe(_ =>
                {
                    ChildSubject.OnNext(_.Item2);
                });
            }


                _items = new ReadOnlyObservableCollection<IContainer<T>>(new ObservableCollection<IContainer<T>>(xx));

            ischecked.DelaySubscription(TimeSpan.FromSeconds(0.5)).Take(1).Subscribe(_ =>
            {
                foreach (var x in xx)
                    (x as SEObject<T>).IsChecked = _;
            });

            Title = title;
    
        }



    }
}


