using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;



namespace UtilityWpf.ViewModel
{
    //public class ToggleViewModel : OutputService<bool> /*where T : struct, IConvertible*/
    //{
       

    //    public string Title { get; }
    //    public ToggleViewModel(string title)
    //    {
    //        Title = title;
    //        Output = new ReactiveProperty<bool>();

    //    }


    //}




    //public class ToggleViewModel<T> : OutputService<T> //, IConvertible*/
    //{

    //    public string Checked { get; }
    //    public string UnChecked { get; }
    //    public string Title { get; }
    //    public ReactiveProperty<bool> Input { get; set; }

    //    public ToggleViewModel(string title="")
    //    {
    //        Title = title==""? typeof(T).Name:title;
    //        Input = new ReactiveProperty<bool>();
    //        Output = Input.Select(_ =>
    //        EnumHelper.ToEnum<T>(Convert.ToInt32(_))).ToReactiveProperty();
    //        Checked = EnumHelper.ToEnum<T>(0).ToString();
    //        UnChecked = EnumHelper.ToEnum<T>(1).ToString();

    //    }

    //}



    //public class ToggleViewModel2<T,R> : OutputViewModel<Func<T,R>> /*where T : struct, IConvertible*/
    //{

    //    public string Checked { get; }
    //    public string UnChecked { get; }
    //    public ReactiveProperty<bool> Input { get; }

    //    public ToggleViewModel2(Func<bool,Func<T,R>> f, string chcked,string unchcked)
    //    {
    //        Input = new ReactiveProperty<bool>();
    //        Output = Input.Select(x => f(x)).ToReactiveProperty();
    //        Checked = chcked;
    //        UnChecked = unchcked;

    //    }


    //}


    //public class TimeSeriesToggleViewModel : ToggleViewModel2<INoisyKernel, CollectionViewModel<KeyValuePair<DateTime, double>>> /*where T : struct, IConvertible*/
    //{

    //    public string Checked { get; }
    //    public string UnChecked { get; }
    //    public ReactiveProperty<bool> Input { get; }

    //    public ToggleViewModel2(Func<bool, Func<T, R>> f, string chcked, string unchcked)
    //    {
    //        Input = new ReactiveProperty<bool>();
    //        Output = Input.Select(x => f(x)).ToReactiveProperty();
    //        Checked = chcked;
    //        UnChecked = unchcked;

    //    }


    //}


    static class EnumHelper
    {
        public static T ToEnum<T>(int i)
        {

            return (T)Enum.ToObject(typeof(T), i);

        }

    }
}


