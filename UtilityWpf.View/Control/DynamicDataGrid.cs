using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UtilityWpf.Common;
using System.Reflection;
using UtilityHelper;
using CustomHelper;

namespace UtilityWpf.View
{

    // A datagrid which acepts a dictionary as its itemssource by converting keyvaluepair to custom dynamic class
    public class DynamicDataGrid : DataGrid
    {

        
        public static readonly DependencyProperty KeyProperty =            DependencyProperty.Register("Key", typeof(string), typeof(DynamicDataGrid), new PropertyMetadata("Key", KeyChange));

        //public static readonly DependencyProperty ItemsSinkProperty =    DependencyProperty.Register("ItemsSink", typeof(IEnumerable), typeof(DynamicDataGrid), new PropertyMetadata(null));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(DynamicDataGrid), new PropertyMetadata("Value", ValueChange));

        //public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(IEnumerable), typeof(DynamicDataGrid), new PropertyMetadata(null, DataChanged));

        public string Key
        {
            get { return (string)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }


        //public IEnumerable Data
        //{
        //    get { return (IEnumerable)GetValue(DataProperty); }
        //    set { SetValue(DataProperty, value); }
        //}

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(((IEnumerable)e.NewValue).FirstOrDefault2() is Dynamic))
                (d as DynamicDataGrid).ItemsSourceSubject.OnNext((IEnumerable)e.NewValue);
        }



        private static void ValueChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DynamicDataGrid).ValueChangeSubject.OnNext((string)e.NewValue);
        }

        private static void KeyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DynamicDataGrid).KeyChangeSubject.OnNext((string)e.NewValue);
        }


        //private static void DataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    (d as DynamicDataGrid).DataChangeSubject.OnNext((IEnumerable)e.NewValue);
        //}


        ISubject<IEnumerable> ItemsSourceSubject = new Subject<IEnumerable>();
        ISubject<string> KeyChangeSubject = new Subject<string>();
        ISubject<string> ValueChangeSubject = new Subject<string>();



        static DynamicDataGrid()
        {
             ItemsSourceProperty.OverrideMetadata(typeof(DynamicDataGrid), new FrameworkPropertyMetadata(null, ItemsSourceChanged));
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(DynamicDataGrid), new FrameworkPropertyMetadata(typeof(DynamicDataGrid)));
        }

        public DynamicDataGrid()
        {

            //Uri resourceLocater = new Uri("/UtilityWpf.View;component/Themes/DynamicDataGrid.xaml", System.UriKind.Relative);
            //ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            //Style = resourceDictionary["DynamicDataGridStyle"] as Style;


            var obs = Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(h => this.Loaded += h, h => this.Loaded -= h).Select(_ => 0);
            var obs2 = Observable.When(KeyChangeSubject.And(ValueChangeSubject).And(ItemsSourceSubject).Then((a, b,c) => 0));

            obs.Amb(obs2)
                .CombineLatest(KeyChangeSubject.StartWith(Key).DistinctUntilChanged(),(a,b)=>b)
                .CombineLatest(ValueChangeSubject.StartWith(Value).DistinctUntilChanged(), (a, b) => b)
                .CombineLatest(ItemsSourceSubject.DistinctUntilChanged(), (a, b) => b)
                .Subscribe(_ =>
                {

                    this.Dispatcher.InvokeAsync(() => this.SetValue(ItemsSourceProperty, DynmamicHelper.OnGetData(_,Key,Value)), System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));
             
                });

        }




    }

 

}

