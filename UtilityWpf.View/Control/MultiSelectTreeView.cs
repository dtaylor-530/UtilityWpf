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
    //[ContentProperty("Items")]
    public class MultiSelectTreeView : TreeView
    {
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register("SelectedItems", typeof(IEnumerable), typeof(MultiSelectTreeView), new PropertyMetadata(null));

        public static readonly DependencyProperty ChildrenPathProperty = DependencyProperty.Register(nameof(ChildrenPath), typeof(string), typeof(MultiSelectTreeView), new PropertyMetadata("Children",ChildrenPathChange));



        //public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(MultiSelectTreeView), new PropertyMetadata(null, ItemsSourceChanged));

        //public static readonly DependencyProperty ItemsSinkProperty = DependencyProperty.Register("ItemsSink", typeof(IEnumerable), typeof(MultiSelectTreeView));


        public new static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(MultiSelectTreeView));

        public static readonly DependencyProperty KeyProperty = DependencyProperty.Register("Key", typeof(string), typeof(MultiSelectTreeView), new PropertyMetadata("Key", KeyChanged));



        public new object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }

        }

        public string Key
        {
            get { return (string)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }



        //public IEnumerable ItemsSource
        //{
        //    get { return (IEnumerable)GetValue(ItemsSourceProperty); }
        //    set { SetValue(ItemsSourceProperty, value); }
        //}

        //public IEnumerable ItemsSink
        //{
        //    get { return (IEnumerable)GetValue(ItemsSinkProperty); }
        //    set { SetValue(ItemsSinkProperty, value); }
        //}




        public IEnumerable SelectedItems
        {
            get { return (IEnumerable)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        public string ChildrenPath
        {
            get { return (string)this.GetValue(ChildrenPathProperty); }
            set { this.SetValue(ChildrenPathProperty, value); }
        }


        static MultiSelectTreeView()
        {
            TreeView.ItemsSourceProperty.OverrideMetadata(typeof(MultiSelectTreeView), new FrameworkPropertyMetadata(null, ItemsSourceChanged));

            //ListBoxEx.KeyProperty.OverrideMetadata(typeof(MultiSelectTreeView), new PropertyMetadata(null, KeyChanged));
            //ListBoxEx.SelectedItemProperty.OverrideMetadata(typeof(MultiSelectTreeView), new PropertyMetadata(null, SelectedItemChanged));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiSelectTreeView), new FrameworkPropertyMetadata(typeof(MultiSelectTreeView)));
        }

        private static void KeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as MultiSelectTreeView).KeySubject.OnNext((string)e.NewValue);
        }

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var b = ((IEnumerable)e.NewValue).First() is ViewModel.SEObject<object>;
            if (!b)
                (d as MultiSelectTreeView).ItemsSourceSubject.OnNext((IEnumerable)e.NewValue);
        }
        private static void ChildrenPathChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as MultiSelectTreeView).ChildrenPathSubject.OnNext((string)e.NewValue);
        }

        //private static void SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    //ListBoxEx lbx = d as ListBoxEx;
        //    //lbx.SelectedItemSubject.OnNext(((object)e.NewValue));
        //}


        ISubject<IEnumerable> ItemsSourceSubject = new Subject<IEnumerable>();
        ISubject<object> SelectedItemSubject = new Subject<object>();
        ISubject<string> KeySubject = new Subject<string>();
        ISubject<string> ChildrenPathSubject = new Subject<string>();


        public MultiSelectTreeView()
        {

            Uri resourceLocater = new Uri("/UtilityWpf.View;component/Themes/MultiSelectTreeView.xaml", System.UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            Style = resourceDictionary["MultiSelectTreeViewControlStyle"] as Style;


            var dispatcher = Application.Current.Dispatcher;
            var UI = new System.Reactive.Concurrency.DispatcherScheduler(dispatcher);

            var sets = Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(h => this.Loaded += h, h => this.Loaded -= h).Select(_ => 0)
                .Take(1)
                .CombineLatest(ChildrenPathSubject.StartWith("Children").DistinctUntilChanged(), (a, b) =>  b )
                .CombineLatest(KeySubject.StartWith("Key").DistinctUntilChanged(), (cp, key) => new { cp, key })
                .CombineLatest(ItemsSourceSubject.DistinctUntilChanged(), (a, itemsource) => new { a, itemsource })
                .Select(init => React(init.a.key,init.a.cp, init.itemsource, UI, dispatcher))
                .Subscribe(_ =>
                {
                    this.Dispatcher.InvokeAsync(() => { ItemsSource = _.Items; }, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));
                });
        }



        public virtual ViewModel.InteractiveCollectionViewModel<object, IConvertible> React(string key,string childrenpath, IEnumerable enumerable, System.Reactive.Concurrency.DispatcherScheduler UI, System.Windows.Threading.Dispatcher dispatcher)
        {
            var sx = ObservableChangeSet.Create<object, IConvertible>(cache =>
            {

                foreach (var val in enumerable)
                    cache.AddOrUpdate(val);
                return System.Reactive.Disposables.Disposable.Empty;
            }, GetKey);

            var kx = new ViewModel.InteractiveCollectionViewModel<object, IConvertible>(sx, ChildrenPath, UI, dispatcher);

            kx.Selected.Subscribe(_=>
            {
                this.Dispatcher.InvokeAsync(() => SelectedItem = _, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));
                //this.Dispatcher.InvokeAsync(() => SelectedItems = ReflectionHelper.GetPropValue<IEnumerable>(_,childrenpath), System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));

            });


            kx.ChildSubject.Subscribe(_ =>
            {
                this.Dispatcher.InvokeAsync(() => SelectedItem = _, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));
                //this.Dispatcher.InvokeAsync(() => SelectedItems = ReflectionHelper.GetPropValue<IEnumerable>(_, childrenpath), System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));

            });
            //kx.DoubleClicked.Subscribe(_ =>
            //{
            //    this.Dispatcher.InvokeAsync(() => DoubleClickedItem = _, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));
            //});

            //SelectedItemSubject.Subscribe(_ =>


            //kx.Deleted.Subscribe(_ =>
            //{
            //    this.Dispatcher.InvokeAsync(() => Deleted = _, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));
            //});
            return kx;
        }


        public virtual IConvertible GetKey(object trade)
        {
            return UtilityWpf.ReflectionHelper.GetPropValue<IConvertible>(trade, Key);

        }




    }
}
