using DynamicData;
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
using System.Windows.Input;
using System.Windows.Markup;

namespace UtilityWpf.View
{
   
    public class ListBoxEx : ListBox 
    {

        public new static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(ListBoxEx), new PropertyMetadata(null, SelectedItemChanged));

        public static readonly DependencyProperty DoubleClickedItemProperty = DependencyProperty.Register("DoubleClickedItem", typeof(object), typeof(ListBoxEx), new PropertyMetadata(null, DoubleClickedItemChanged));

        public static readonly DependencyProperty KeyProperty = DependencyProperty.Register("Key", typeof(string), typeof(ListBoxEx), new PropertyMetadata("Key", KeyChanged));

        public static readonly DependencyProperty DeletedProperty = DependencyProperty.Register("Deleted", typeof(object), typeof(ListBoxEx), new PropertyMetadata(null, DeletedChanged));

        //public static readonly DependencyProperty Items2Property = DependencyProperty.Register("Items2", typeof(IEnumerable), typeof(ListBoxEx), new PropertyMetadata(null, Items2Changed));


        public string Key
        {
            get { return (string)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        //public IEnumerable Items2
        //{
        //    get { return (IEnumerable)GetValue(Items2Property); }
        //    set { SetValue(Items2Property, value); }
        //}



        public new object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }

        }

        public object Deleted
        {
            get { return (object)GetValue(DeletedProperty); }
            set { SetValue(DeletedProperty, value); }

        }

        public object DoubleClickedItem
        {
            get { return (object)GetValue(DoubleClickedItemProperty); }
            set { SetValue(DoubleClickedItemProperty, value); }

        }


        private static void DoubleClickedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private static void DeletedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ListBoxEx).DeletedSubject.OnNext((object)e.NewValue);
        }

        private static void KeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ListBoxEx).KeySubject.OnNext((string)e.NewValue);
        }

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            var b = ((IEnumerable)e.NewValue).FirstOrDefault2() is ViewModel.SHDObject<object>;
            if (!b)
                (d as ListBoxEx).ItemsSourceSubject.OnNext((IEnumerable)e.NewValue);
        }

        private static void Items2Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
             (d as ListBoxEx).ItemsSourceSubject.OnNext((IEnumerable)e.NewValue);
        }



        private static void SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ListBoxEx).SelectedItemSubject.OnNext(((object)e.NewValue));
        }

        protected ISubject<object> SelectedItemSubject = new Subject<object>();
        ISubject<IEnumerable> ItemsSourceSubject = new Subject<IEnumerable>();
        ISubject<string> KeySubject = new Subject<string>();
        protected ISubject<object> DeletedSubject = new Subject<object>();


        static ListBoxEx()
        {
            ListBox.ItemsSourceProperty.OverrideMetadata(typeof(ListBoxEx), new FrameworkPropertyMetadata(null, ItemsSourceChanged));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBoxEx), new FrameworkPropertyMetadata(typeof(ListBoxEx)));
        }

        //private static object CoeceSelectedItem(DependencyObject d, object baseValue)
        //{
        //    if (baseValue == null)
        //        return null;
        //    return (baseValue.GetType().GetInterfaces().Contains(typeof(ViewModel.IContainer<object>))) ? (baseValue as ViewModel.SHDObject<object>).Object : baseValue;
        //}

        public ListBoxEx()
        {

            Uri resourceLocater = new Uri("/UtilityWpf.View;component/Themes/ListBoxEx.xaml", System.UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            Style = resourceDictionary["ListBoxExStyle"] as Style;

            var UI = new System.Reactive.Concurrency.DispatcherScheduler(Application.Current.Dispatcher);
            //var obs = Observable.FromEventPattern<RoutedEventHandler, EventArgs>(_ => this.Loaded += _, _ => this.Loaded -= _);
            //var obs2= Observable.FromEventPattern<EventHandler, EventArgs>(_ => this.Initialized += _, _ => this.Initialized -= _);

            //obs.Subscribe(_ => 
            //{ });
            //obs2.Subscribe(_ =>
            //{ });

            var sets = Observable.FromEventPattern<RoutedEventHandler, EventArgs>(_ => this.Loaded += _, _ => this.Loaded -= _)
                //.Take(1)
                .CombineLatest(KeySubject.StartWith("Key").DistinctUntilChanged (), (a, b) => b)
                .CombineLatest(ItemsSourceSubject/*.Where(_ => _.Count() > 0)*/, (key, items) => new { key, items })
                .Select(init => React2(init.key, init.items, UI))
                .Subscribe(_ =>
               {
                   this.Dispatcher.InvokeAsync(() => { ItemsSource = _.Items; }, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));
               });


        }


        public virtual ViewModel.InteractiveCollectionViewModel<object, IConvertible> React2(string a, IEnumerable b, System.Reactive.Concurrency.DispatcherScheduler UI)
        {

            if (a == null)
                throw new Exception("Key Property is null");

            var sx = ObservableChangeSet.Create<object, IConvertible>(cache =>
            {
                var dels = DeletedSubject.Subscribe(_ =>
                cache.Remove<object, IConvertible>(_));
                try
                {
                    foreach (var g in b)
                        cache.AddOrUpdate(g);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return new System.Reactive.Disposables.CompositeDisposable(dels);

            }, GetKey);

            var kx = new ViewModel.InteractiveCollectionViewModel<object, IConvertible>(sx, UI);

            kx.DoubleClicked.Subscribe(_ =>
            {
                this.Dispatcher.InvokeAsync(() => DoubleClickedItem = _, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));
            });

            kx.Selected.Subscribe(_ =>
            {
                this.Dispatcher.InvokeAsync(() => SelectedItem = _, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));
            });

            kx.Deleted.Subscribe(_ =>
            {
                this.Dispatcher.InvokeAsync(() => Deleted = _, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));
            });
            return kx;
        }


        public virtual IConvertible GetKey(object trade)
        {

            return (IConvertible)ReflectionHelper.GetPropValue<IConvertible>(trade, Key);

        }


    }


    

}