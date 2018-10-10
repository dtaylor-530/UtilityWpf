﻿using DynamicData.Binding;
using Reactive.Bindings;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;

namespace UtilityWpf.ViewModel
{


    public class OutputViewModel<T> : IOutputViewModel<T>
    {

        public virtual IObservable<T> Output { get; protected set; }

    }


    public interface IOutputViewModel<T> //: INotifyPropertyChanged
    {
        IObservable<T> Output { get;  }

    }




    //public interface IOutputViewModel2<T> : INotifyPropertyChanged
    //{
    //    T Output { get; set; }

    //}


    //public class OutputViewModel2<T> :AbstractNotifyPropertyChanged, IOutputViewModel2<T>
    //{

    //    private T _output;



    //    public T Output
    //    {
    //        get => _output;
    //        set => SetAndRaise(ref _output, value);
    //    }
    //}





    //public static class OutputViewModelHelper2
    //{
    //    // saves having to add reference to reactive propety for dependent projects
    //    public static System.IObservable<T> OutputAsObservable<T>(this IOutputViewModel2<T> fd)
    //    {

    //        return fd.WhenValueChanged(_ => _.Output).Where(_ => _ != null);
    //    }
    //}
}
