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





    public class InteractiveCollectionViewModel<T, R> : InteractiveCollectionBase<T>, ICollectionViewModel<IContainer<T>>
    {
        //private ReadOnlyObservableCollection<SHDObject<T>> _items;

        public InteractiveCollectionViewModel(IObservableCache<T, R> observable,
            IObservable<Func<T, bool>> visiblefilter,
            IObservable<Func<T, bool>> enabledfilter,
            IScheduler scheduler, string title = null)
        {

            //  Output = new ReactiveProperty<T>();

            observable.Connect()
                 .ObserveOn(scheduler)
                .Transform(s =>
                {
                    var so = new SHDObject<T>(s, visiblefilter, enabledfilter);
                    this.ReactToChanges(so);
                    return (IContainer<T>)so;
                })
                  .Bind(out _items)
              .DisposeMany()
                .Subscribe(
                _ => Console.WriteLine("generic view model changed"),
                ex => Console.WriteLine("Error in generic view model"));

            Title = title;

        }


            //private ReadOnlyObservableCollection<SHDObject<T>> _items;

            public InteractiveCollectionViewModel(IObservable<IChangeSet<T, R>> observable, IScheduler scheduler, string title = null)
            {

                observable
                     .ObserveOn(scheduler)
                    .Transform(s =>
                    {
                        var so = new SHDObject<T>(s );
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

        public ISubject<T> ChildSubject = new Subject<T>();

        public InteractiveCollectionViewModel(IObservable<IChangeSet<T, R>> observable, string childrenpath,IScheduler scheduler, System.Windows.Threading.Dispatcher dispatcher,string title = null)
        {

            observable
                 .ObserveOn(scheduler)
                .Transform(s =>
                {
                    var so = new SEObject<T>(s,childrenpath,dispatcher);
                    so.OnPropertyChange(_ => _.ChildSelected).Subscribe(_ => ChildSubject.OnNext(_));
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



        public InteractiveCollectionViewModel(IObservable<IChangeSet<T, R>> observable,
IObservable<Func<T, bool>> invisiblefilter,
IObservable<Func<T, bool>> enabledfilter,
IScheduler scheduler, string title = null)
        {

           // Output = new ReactiveProperty<T>();

            observable.ObserveOn(scheduler)
           .Transform(s =>
           {
               var so = new SHDObject<T>(s,invisiblefilter, enabledfilter) ;
               this.ReactToChanges(so);
               return (IContainer<T>)so;
           })
             .Bind(out _items)
         .DisposeMany()
           .Subscribe(
           _ => Console.WriteLine("generic view model changed"),
           ex => Console.WriteLine("Error in generic view model"));

            Title = title;

        }

    }




    //public static IObservable<IKey> GetOutput(this SelectableCollectionViewModel<IKey, string> Selections)
    //{
    //    return Selections.Output.BufferUntilInactive().Select(so => so.Object);
    //}
    //public static IObservable<T> GetSelectedObjectSteam<T, R>(this SelectableViewModel<T, R> si)
    //{
    //    //si.WhenValueChanged(t => t.SelectedItem).Subscribe(_ =>
    //    //Console.WriteLine());

    //    //si.WhenValueChanged(t => t.SelectedItem)
    //    //    .Throttle(TimeSpan.FromMilliseconds(250))
    //    //    .Where(_ => _ != null)
    //    //     .Select(_ => _.Object).Subscribe(_ =>
    //    //Console.WriteLine());


    //    return si.WhenValueChanged(t => t.Output)
    //        .Throttle(TimeSpan.FromMilliseconds(250))
    //        .Where(_ => _ != null)
    //         .Select(_ => _.Object);



    //}


    //public static SelectableObject<T> AddSelectable<T>(this ISelectableItems<T> si, T s)
    //{
    //    var so = new SelectableObject<T>(s);
    //    so.WhenValueChanged(t => t.IsSelected)
    //        .Throttle(TimeSpan.FromMilliseconds(250))
    //       .Subscribe(b =>
    //       {
    //           si.SelectedItem = si.Items.FirstOrDefault(sof => sof.IsSelected == true);
    //       });
    //    return so;

    //}

  
}




