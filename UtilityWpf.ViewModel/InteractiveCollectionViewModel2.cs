
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

namespace UtilityWpf.ViewModel
{

    public class InteractiveCollectionViewModel<T> : InteractiveCollectionBase<T>, ICollectionViewModel<IContainer<T>>
    {


        //private ReadOnlyObservableCollection<SHDObject<T>> _items;


        public InteractiveCollectionViewModel(IObservable<IChangeSet<T>> observable,
IObservable<Func<T, bool>> invisiblefilter,
IObservable<Func<T, bool>> enabledfilter,
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
           ex => Console.WriteLine("Error in generic view model"));

            Title = title;

        }


        //private ObservableCollection<SHDObject<T>> items;

        public InteractiveCollectionViewModel(IObservable<T> observable, IScheduler scheduler,            string title = null)
        {

            //items =(new ObservableCollection<SHDObject<T>>());
            // _items = (new ReadOnlyObservableCollection<SHDObject<T>>(items));
            observable.ToObservableChangeSet().ObserveOn(scheduler)
                .Transform
                (s =>
                {
                    var so = new SHDObject<T>(                       s );
                    this.ReactToChanges(so);
                    return (IContainer<T>)so;
                })
                //.Do(so => )
      
               //.Distinct()
     .Bind(out _items)
         .DisposeMany()
           .Subscribe(
           _ => 
           Console.WriteLine("generic view model changed"),
           ex => Console.WriteLine("Error in generic view model"));

            Title = title;

        }


        public InteractiveCollectionViewModel(IEnumerable<T> enumerable, System.Windows.Threading.Dispatcher dispatcher,    string title = null)
        {

            //_items = new ReadOnlyObservableCollection<SHDObject<T>>(new ObservableCollection<SHDObject<T>>());
            //dispatcher.Invoke(() =>
            //{
                var xx = enumerable.Select
                 (s =>
                 {
                     var so = new SHDObject<T> ( s );
                     return (IContainer<T>)so;
                 });

                foreach (var so in xx)
                    this.ReactToChanges((SHDObject<T>)so);

                _items=new ReadOnlyObservableCollection<IContainer<T>>(new ObservableCollection<IContainer<T>>(xx));
                
                Title = title;

            //});

        }


        public ISubject<T> ChildSubject = new Subject<T>();

        public InteractiveCollectionViewModel(IEnumerable<T> enumerable,string childrenpath, System.Windows.Threading.Dispatcher dispatcher, string title = null)
        {

            //_items = new ReadOnlyObservableCollection<SHDObject<T>>(new ObservableCollection<SHDObject<T>>());
            //dispatcher.Invoke(() =>
            //{
                var xx = enumerable.Select
                 (s =>
                 {
                     var so = new SEObject<T>(s,childrenpath,dispatcher );
                     so.OnPropertyChange(_=>_.ChildSelected).Subscribe(_ => ChildSubject.OnNext(_));
                     return (IContainer<T>)so;
                 });

                foreach (var so in xx)
                    this.ReactToChanges((SEObject < T > )so);

                _items = new ReadOnlyObservableCollection<IContainer<T>>(new ObservableCollection<IContainer<T>>(xx));

                Title = title;

            //});

        }



    }
}


