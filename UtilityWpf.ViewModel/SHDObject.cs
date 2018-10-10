
using DynamicData.Binding;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UtilityWpf.ViewModel
{


    public interface IContainer<T>
    {
        T Object { get; }
    }


    // Selectable / Hideable / Disableable
    public class SHDObject<T> : UtilityWpf.ViewModel.IContainer<T>
    {
        public T Object { get; set; }

        public ReactiveProperty<bool> IsSelected { get; } = new ReactiveProperty<bool>(false);

        public ReactiveProperty<bool> IsExpanded { get; } = new ReactiveProperty<bool>(false);

        public ReactiveProperty<bool> IsVisible { get; }

        public ReactiveProperty<bool> IsEnabled { get; }

        public ReactiveProperty<bool> IsDoubleClicked { get; }

        public ReactiveCommand DoubleClickCommand { get; } = new ReactiveCommand();

        public ReactiveCommand DeleteCommand { get; } = new ReactiveCommand();

        public SHDObject(T @object, IObservable<Func<T, bool>> visible = null, IObservable<Func<T, bool>> enable = null)
        {
            Object = @object;

            if (visible != null)
                IsVisible = visible.Select(_ => _(Object)).StartWith(true).ToReactiveProperty();
            else
                IsVisible = new ReactiveProperty<bool>(true);

            if (enable != null)
                IsEnabled = enable.Select(_ => _(Object)).ToReactiveProperty();
            else
                IsEnabled = new ReactiveProperty<bool>(true);

            IsDoubleClicked = DoubleClickCommand.CombineLatest(IsSelected, (a, b) => (b)).ToReactiveProperty();

            IsSelected.Subscribe(_ =>
            {

            });

            DeleteCommand.Subscribe(_ =>
            {

            });
            //IsExpanded.Subscribe(_ =>
            //{ });

            //IsSelected.Subscribe(_ =>
            //{ });
            //IsDoubleClicked.Subscribe(_ =>
            //    {


            //    });
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








}




