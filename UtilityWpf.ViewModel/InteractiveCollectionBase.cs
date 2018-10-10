
using DynamicData;
using DynamicData.Binding;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityWpf.ViewModel
{

    public abstract class InteractiveCollectionBase<T>:NPC
    {


        public IObservable<T> Selected { get; } = new System.Reactive.Subjects.Subject<T>();

        public IObservable<T> DoubleClicked { get; } = new System.Reactive.Subjects.Subject<T>();

        public IObservable<T> Deleted { get; } = new System.Reactive.Subjects.Subject<T>();

        public IObservable<T> Expanded { get; } = new System.Reactive.Subjects.Subject<T>();

        public string Title { get; protected set; }

        protected ReadOnlyObservableCollection<IContainer<T>> _items;

        public ICollection<IContainer<T>> Items => _items;
    }


    //public abstract class InteractiveCollectionBase2<T> : INPCBase
    //{

    //    public IObservable<T> Selected{ get; } = new System.Reactive.Subjects.Subject<T>();

    //    //public IObservable<T> Expanded { get; } = new System.Reactive.Subjects.Subject<T>();

    //    public string Title { get; protected set; }

    //    protected ReadOnlyObservableCollection<SHDObject<T>> _items;

    //    public ICollection<SHDObject<T>> Items => _items;
    //}



    public static class BaseHelper
    {

        public static void ReactToChanges<T>(this InteractiveCollectionBase<T> col, SHDObject<T> so)
        { 
            so.IsSelected
                  .Throttle(TimeSpan.FromMilliseconds(250))
                     .Subscribe(b =>
                     {
                         if (col.Items?.FirstOrDefault(sof =>((SHDObject<T>) sof).IsSelected.Value == true) != (null))
                             ((System.Reactive.Subjects.ISubject<T>)col.Selected).OnNext(col.Items.FirstOrDefault(sof => ((SHDObject<T>)sof).IsSelected.Value == true).Object);
                     });

            //so.IsExpanded.Subscribe(_ =>
            // {
            //     ((System.Reactive.Subjects.ISubject<T>)col.IsExpanded).OnNext(so == default(SHDObject<T>) ? default(T) : so.Object);
            // });


            so.DoubleClickCommand.Subscribe(_ =>
            {
                ((System.Reactive.Subjects.ISubject<T>)col.DoubleClicked).OnNext(so == default(SHDObject<T>) ? default(T) : so.Object);
            });

            so.DeleteCommand.Subscribe(_ =>
            {
                ((System.Reactive.Subjects.ISubject<T>)col.Deleted).OnNext(so == default(SHDObject<T>) ? default(T) : so.Object);
            });
        }

        public static void ReactToChanges<T>(this InteractiveCollectionBase<T> col, SEObject<T> so)
        {
            so.WhenPropertyChanged(_=>_.IsSelected)
                  .Throttle(TimeSpan.FromMilliseconds(250))
                     .Subscribe(b =>
                     {
                         if (col.Items?.FirstOrDefault(sof => ((SEObject<T>)sof).IsSelected == true) != (null))
                             ((System.Reactive.Subjects.ISubject<T>)col.Selected).OnNext(col.Items.FirstOrDefault(sof => ((SEObject<T>)sof).IsSelected== true).Object);
                     });

            
            //so.IsExpanded.Subscribe(_ =>
            // {
            //     ((System.Reactive.Subjects.ISubject<T>)col.IsExpanded).OnNext(so == default(SHDObject<T>) ? default(T) : so.Object);
            // });

        
        }
    }


    public interface ICollectionViewModel<T>
    {

        ICollection<T> Items { get; }
    }

}


