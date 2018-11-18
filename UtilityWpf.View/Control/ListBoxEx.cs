using DynamicData;
using Reactive.Bindings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using UtilityHelper.NonGeneric;
using UtilityInterface;
using UtilityWpf.ViewModel;

namespace UtilityWpf.View
{

    public class ListBoxEx : ListBox
    {

        public new static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(ListBoxEx), new PropertyMetadata(null, SelectedItemChanged));

        public static readonly DependencyProperty DoubleClickedItemProperty = DependencyProperty.Register("DoubleClickedItem", typeof(object), typeof(ListBoxEx), new PropertyMetadata(null, DoubleClickedItemChanged));

        public static readonly DependencyProperty KeyProperty = DependencyProperty.Register("Key", typeof(string), typeof(ListBoxEx), new PropertyMetadata("Key", KeyChanged));

        public static readonly DependencyProperty DeletedProperty = DependencyProperty.Register("Deleted", typeof(object), typeof(ListBoxEx), new PropertyMetadata(null, DeletedChanged));

        public static readonly DependencyProperty FilterProperty = DependencyProperty.Register("Filter", typeof(IFilter), typeof(ListBoxEx), new PropertyMetadata(null, FilteredChanged));

        public static readonly DependencyProperty RemoveProperty = DependencyProperty.Register("Remove", typeof(bool), typeof(ListBoxEx), new PropertyMetadata(true, RemoveChanged));

        //public static readonly DependencyProperty FilterOnProperty = DependencyProperty.Register("FilterOn", typeof(string), typeof(ListBoxEx), new PropertyMetadata(null, FilterOnChanged));
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ListBoxEx), new PropertyMetadata(Orientation.Horizontal));

        public static readonly DependencyProperty AllChangesProperty = DependencyProperty.Register("AllChanges", typeof(IEnumerable), typeof(ListBoxEx), new PropertyMetadata(null));

        public static readonly DependencyProperty CheckedProperty = DependencyProperty.Register("Checked", typeof(IEnumerable), typeof(ListBoxEx), new PropertyMetadata(null));




        public IEnumerable AllChanges
        {
            get { return (IEnumerable)GetValue(AllChangesProperty); }
            set { SetValue(AllChangesProperty, value); }
        }


        //public IEnumerable ItemsSource
        //{
        //    get { return (IEnumerable)GetValue(ItemsSourceProperty); }
        //    set { SetValue(ItemsSourceProperty, value); }
        //}

        //public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ListBoxEx), new PropertyMetadata(null, ItemsSourceChanged));



        public IEnumerable Checked
        {
            get { return (IEnumerable)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }



        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }



        public bool Remove
        {
            get { return (bool)GetValue(RemoveProperty); }
            set { SetValue(RemoveProperty, value); }
        }


        public IFilter Filter
        {
            get { return (IFilter)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        //public string FilterOn
        //{
        //    get { return (string)GetValue(FilterOnProperty); }
        //    set { SetValue(FilterOnProperty, value); }
        //}

        // Using a DependencyProperty as the backing store for FilterProperty.  This enables animation, styling, binding, etc...

        public string Key
        {
            get { return (string)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }


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
        }

        private static object ItemsSourceCoerce(DependencyObject d, object baseValue)
        {
            if (((IEnumerable)baseValue)?.Count() > 0)
            {
                if ((((IEnumerable)baseValue).OfType<ViewModel.SHDObject<object>>().Count() > 0))
                    return baseValue;
                else
                {
                    (d as ListBoxEx).ItemsSourceSubject.OnNext((IEnumerable)baseValue);
                    return null;
                }
            }
            return null;
        }

        private static void FilteredChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ListBoxEx).FilterSubject.OnNext((IFilter)e.NewValue);
        }

        private static void RemoveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ListBoxEx).RemoveSubject.OnNext((bool)e.NewValue);
        }
        //private static void FilterOnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    (d as ListBoxEx).FilterOnSubject.OnNext((string)e.NewValue);
        //}

        private static void SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ListBoxEx).SelectedItemSubject.OnNext(((object)e.NewValue));
        }

        protected ISubject<object> SelectedItemSubject = new Subject<object>();
        protected ISubject<IEnumerable> ItemsSourceSubject = new Subject<IEnumerable>();
        protected ISubject<string> KeySubject = new Subject<string>();
        protected ISubject<object> DeletedSubject = new Subject<object>();
        protected ISubject<object> ClearedSubject = new Subject<object>();
        protected ISubject<IFilter> FilterSubject = new Subject<IFilter>();
        protected ISubject<bool> RemoveSubject = new Subject<bool>();
        //protected ISubject<string> FilterOnSubject = new Subject<string>();

        ViewModel.InteractiveCollectionViewModel<object, object> interactivecollection; /*{ get;  set; }*/

        public IObservable<KeyValuePair<IContainer<Object>, ChangeReason>> Changes { get; private set; } = new Subject<KeyValuePair<IContainer<Object>, ChangeReason>>();
        DispatcherScheduler UI;

        static ListBoxEx()
        {

            ListBoxEx.ItemsSourceProperty.OverrideMetadata(typeof(ListBoxEx), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, ItemsSourceChanged, ItemsSourceCoerce));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBoxEx), new FrameworkPropertyMetadata(typeof(ListBoxEx)));
        }


        public ListBoxEx(Func<object, object> _keyfunc = null)
        {
            Uri resourceLocater = new Uri("/UtilityWpf.View;component/Themes/ListBoxEx.xaml", System.UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            Style = resourceDictionary["ListBoxExStyle"] as Style;


            UI = new System.Reactive.Concurrency.DispatcherScheduler(Application.Current.Dispatcher);
            if (_keyfunc != null)
            {
                //_.GetType().GetProperty("Object").GetValue(_)
                interactivecollection = ViewModel.InteractiveCollectionFactory.Build(
                   _=>_keyfunc(_),
                   ItemsSourceSubject.Select(v => v.Cast<object>()),
                   FilterSubject,
                   DeletedSubject.WithLatestFrom(RemoveSubject.StartWith(Remove).DistinctUntilChanged(), (d, r) => r ? d : null).Where(v => v != null),
                   ClearedSubject,
                   UI
                );
                CollectiionChanged();
            }

            Init();

        }

        public ListBoxEx(string _key = null)
        {

            Uri resourceLocater = new Uri("/UtilityWpf.View;component/Themes/ListBoxEx.xaml", System.UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            Style = resourceDictionary["ListBoxExStyle"] as Style;

            UI = new System.Reactive.Concurrency.DispatcherScheduler(Application.Current.Dispatcher);
            if (_key != null)
            {
                Key = _key;
                interactivecollection = ViewModel.InteractiveCollectionFactory.Build(
                   GetKeyFunc(_key),
                   ItemsSourceSubject.Select(v => v.Cast<object>()),
                   FilterSubject,
                   DeletedSubject.WithLatestFrom(RemoveSubject.StartWith(Remove).DistinctUntilChanged(), (d, r) => r ? d : null).Where(v => v != null),
                   ClearedSubject,
                   UI
                );
                CollectiionChanged();
            }

            Init();
        }


        public ListBoxEx()
        {

            Uri resourceLocater = new Uri("/UtilityWpf.View;component/Themes/ListBoxEx.xaml", System.UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            Style = resourceDictionary["ListBoxExStyle"] as Style;

            UI = new System.Reactive.Concurrency.DispatcherScheduler(Application.Current.Dispatcher);

            Init();
        }


        private void Init()
        {

            var key = KeySubject.DistinctUntilChanged().Merge(ItemsSourceSubject.Take(1)
    .Select(_ => _GetKey(_))
    .Where(_ => _ != null)).DistinctUntilChanged();

            Build(ItemsSourceSubject.Select(v => v.Cast<object>()), key).Subscribe(a_ =>
            {
                this.Dispatcher.InvokeAsync(() =>
                {
                    interactivecollection = a_;
                    CollectiionChanged();
                },
                System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));
            });
        }

        ObservableCollection<object> changeCollection = new ObservableCollection<object>();


        private IObservable<InteractiveCollectionViewModel<object, object>> Build(IObservable<IEnumerable<object>> observable, IObservable<string> key)
        {
            var UI = new System.Reactive.Concurrency.DispatcherScheduler(Application.Current.Dispatcher);

            return ViewModel.InteractiveCollectionFactory.Build(
                       key.Select(_ => GetKeyFunc(_)),
       observable,
       FilterSubject,
       DeletedSubject.WithLatestFrom(RemoveSubject.StartWith(Remove).DistinctUntilChanged(), (d, r) => r ? d : null).Where(v => v != null),
       ClearedSubject,
       UI
    );


        }

        private void CollectiionChanged()
        {
            this.Dispatcher.InvokeAsync(() =>
            {
                ItemsSource = interactivecollection.Items;
            }, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));

            interactivecollection.GetDoubleClicked().Subscribe(_ =>
               this.Dispatcher.InvokeAsync(() => DoubleClickedItem = _, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken)));

            interactivecollection.GetSelected().Subscribe(_ =>
               this.Dispatcher.InvokeAsync(() => SelectedItem = _, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken)));

            interactivecollection.GetRemoved().Subscribe(_ =>
               this.Dispatcher.InvokeAsync(() => Deleted = _, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken)));

            interactivecollection.Changes.Subscribe(_ => { (Changes as ISubject<KeyValuePair<IContainer<Object>, ChangeReason>>).OnNext(_); changeCollection.Add(_); ItemsSource = interactivecollection.Items; });

            this.Dispatcher.InvokeAsync(() => AllChanges = changeCollection, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));

            this.Dispatcher.InvokeAsync(() => Checked = interactivecollection.@checked, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));
        }

        public virtual object GetKey(object trade)
        {
            //var type = trade.GetType().GetProperty(Key);
            //var interfaces =type .GetInterfaces();
            //if (!type.IsAssignableFrom(typeof(IConvertible)) && !interfaces.Select(_=>_.Name).Any(_=>_.StartsWith("IEquatable")))
            //    throw new Exception("Key of type "+ type.Name+ " does not inherit " + nameof(IConvertible) + " or "+ "IEquatable");
            //else
            return null;

        }

        public virtual string _GetKey(IEnumerable _)
        {
            var type = _.First().GetType();
            var xx = UtilityHelper.IdHelper.GetIdProperty(type);
            if (xx != null)
            {
                var sxx = UtilityHelper.IdHelper.CheckIdProperty(xx, type);
                if (sxx)
                    return xx;
                else
                    return null;
            }
            return null;
        }


        private Func<object, object> GetKeyFunc(string key)
        {
            Func<object, object> f = o =>
            UtilityHelper.PropertyHelper.GetPropValue<object>(o, key);
            return f;
        }
    }




}