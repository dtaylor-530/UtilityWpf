
using DynamicData.Binding;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using UtilityInterface;

namespace UtilityWpf.ViewModel
{


    //public interface IContainer<T>
    //{
    //    T Object { get; }
    //}

    public class SHDOObject : SHDObject<object>
    {
        public SHDOObject() : base(null)
        {


        }

        public SHDOObject(object o) : base(o)
        {


        }
    }

    // Selectable / Hideable / Disableable
    public class SHDObject<T> : NPC, IContainer<T>
    {
        public T Object { get; set; }

        //public ReactiveProperty<bool> IsSelected { get; } = new ReactiveProperty<bool>(false);

        //public ReactiveProperty<bool> IsChecked { get; } = new ReactiveProperty<bool>(false);

        //public ReactiveProperty<bool> IsExpanded { get; } = new ReactiveProperty<bool>(false);

        //public ReactiveProperty<bool> IsDeSelected { get; } = new ReactiveProperty<bool>(false);

        public ReactiveProperty<bool> IsVisible { get; }

        public ReactiveProperty<bool> IsEnabled { get; }

        public ReactiveProperty<bool> IsDoubleClicked { get; }

        //public ReactiveCommand DoubleClickCommand { get; } = new ReactiveCommand();

        //Decim
        public ISubject<object> Deletions { get; } = new Subject<object>();

        public RelayCommand DeleteCommand { get; }


        //public ReactiveCommand ReAddCommand { get; } = new ReactiveCommand();

        public SHDObject(T @object, IObservable<Predicate<T>> visible = null, IObservable<Predicate<T>> enable = null)
        {
            Object = @object;

            if (visible != null)
                IsVisible = visible.Select(_ => _(Object)).StartWith(true).ToReactiveProperty();
            else
                IsVisible = new ReactiveProperty<bool>(true);

            if (enable != null)
                IsEnabled = enable.StartWith(_ => true).Select(_ => _(Object)).ToReactiveProperty();
            else
                IsEnabled = new ReactiveProperty<bool>(true);

            DeleteCommand = new RelayCommand(() =>
            {
                Deletions.OnNext(null);
            });
        }
        public SHDObject()
        {

        }
        //IsDoubleClicked = DoubleClickCommand.CombineLatest(this.WhenPropertyChanged(_=>_. IsSelected).Select(__=>__.Value), (a, b) => (b)).ToReactiveProperty();


        //IsDeSelected = DeleteCommand.Select(_ => true).Merge(ReAddCommand.Select(_=>false)).ToReactiveProperty();

        //DeleteCommand.Subscribe(_ =>
        //{

        //});

        //ReAddCommand.Subscribe(_ =>
        //{

        //});
        //IsExpanded.Subscribe(_ =>
        //{ });

        //IsSelected.Subscribe(_ =>
        //{ });
        //IsDoubleClicked.Subscribe(_ =>
        //    {


        //    });


        private bool? _isExpanded;
        private bool? _isSelected;
        private bool? _isChecked;


        // properties don't work as reactive properties

        public bool? IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                //if (value != _isExpanded)
                //{
                _isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
                //}
            }
        }


        public virtual bool? IsSelected
        {
            get { return _isSelected; }
            set
            {
                //if (value != _isSelected)
                //{
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
                //}
            }
        }

        public virtual bool? IsChecked
        {
            get { return _isChecked; }
            set
            {
                //if (value != _isSelected)
                //{
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
                //}
            }
        }
    }


}















//public class SelectableObject<T> : INotifyPropertyChanged, ISelectable//where T : struct, IConvertible,ISelectable
//{
//    public T Object { get; set; }

//    //public string Name { get { return Enum.ToString(); } }

//    public SelectableObject(T obj)
//    {

//        Object = obj;

//    }


//    private bool isSelected;


//    public bool IsSelected
//    {
//        get { return isSelected; }
//        set
//        {
//            isSelected = value;
//            NotifyChanged(nameof(IsSelected));
//        }
//    }

//    public override string ToString()
//    {
//        return Object.ToString();
//    }

//    #region INotifyPropertyChanged Implementation
//    /// <summary>
//    /// Occurs when any properties are changed on this object.
//    /// </summary>
//    public event PropertyChangedEventHandler PropertyChanged;


//    /// <summary>
//    /// A helper method that raises the PropertyChanged event for a property.
//    /// </summary>
//    /// <param name="propertyNames">The names of the properties that changed.</param>
//    public virtual void NotifyChanged(params string[] propertyNames)
//    {
//        foreach (string name in propertyNames)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
//        }
//    }


//    protected void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string caller = "")
//    {

//        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));

//    }
//    #endregion
//}



//public static class SelectableObjectHelper
//{
//    public static void Subscribe<T>(SelectableObject<T> s, Action a)
//    {
//        s.WhenValueChanged(t => t.IsSelected)
//                        .Throttle(TimeSpan.FromMilliseconds(250))
//                       .Subscribe(b => a());

//    }
//}













