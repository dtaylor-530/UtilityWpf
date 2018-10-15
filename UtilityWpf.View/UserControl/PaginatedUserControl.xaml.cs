using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DynamicData;
using System.Reactive.Subjects;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Collections;

namespace UtilityWpf.View
{
    /// <summary>
    /// Interaction logic for PaginatedUserControl.xaml
    /// </summary>
    public partial class PaginatedUserControl : UserControl
    {

        public static readonly DependencyProperty PageSizeProperty = DependencyProperty.Register("PageSize", typeof(int), typeof(PaginatedUserControl), new PropertyMetadata(25, PageSizeChange));

        public static readonly DependencyProperty CurrentPageProperty = DependencyProperty.Register("CurrentPage", typeof(int), typeof(PaginatedUserControl), new PropertyMetadata(1, CurrentPageChange));

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(IEnumerable), typeof(PaginatedUserControl), new PropertyMetadata(null, ItemsChange));

        //public static readonly DependencyProperty PaginatedItemPropertys = DependencyProperty.Register("PaginatedItems", typeof(IEnumerable), typeof(PaginatedUserControl), new PropertyMetadata(null, PaginatedItemsChange));


        public static readonly DependencyProperty PageRequestProperty = DependencyProperty.Register("PageRequest", typeof(object), typeof(PaginatedUserControl), new PropertyMetadata(null, PageRequestChange));

        public static readonly DependencyProperty PaginatedItemsProperty = DependencyProperty.Register("PaginatedItems", typeof(ReadOnlyObservableCollection<object>), typeof(PaginatedUserControl), new PropertyMetadata(null, ItemsChange));

        //public PageRequest PageRequest {

        //    set
        //    {
        //        var v = value;
        //    }
        //}


        public IEnumerable Items
        {
            get { return (IEnumerable)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }



        public int CurrentPage
        {
            get { return (int)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }
        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }


        ISubject<int> CurrentPageSubject = new Subject<int>();
        ISubject<int> PageSizeSubject = new Subject<int>();
        ISubject<IEnumerable> ItemsSubject = new Subject<IEnumerable>();



        private static void CurrentPageChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PaginatedUserControl control = d as PaginatedUserControl;
            control.CurrentPageSubject.OnNext((int)e.NewValue);
        }
        private static void PageSizeChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PaginatedUserControl control = d as PaginatedUserControl;
            control.PageSizeSubject.OnNext((int)e.NewValue);
        }


        private static void ItemsChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PaginatedUserControl control = d as PaginatedUserControl;
            //Task.Run(() =>
            //{
                //foreach (var x in)
                    control.ItemsSubject.OnNext((IEnumerable)e.NewValue);
            //});
        }

        private ReadOnlyObservableCollection<object> pitems;
        //private ReadOnlyObservableCollection<T> allitems;
        //private ReadOnlyObservableCollection<T> fitems;

        public ReadOnlyObservableCollection<object> PaginatedItems
        {
            get { return (ReadOnlyObservableCollection<object>)GetValue(PaginatedItemsProperty); }
            set { SetValue(PaginatedItemsProperty, value); }
        }

        //public ReadOnlyObservableCollection<T> AllItems
        //{
        //    get { return allitems; }
        //    set { allitems = value; }
        //}

        //private IObservable<IChangeSet<T>> items;

        //public ReadOnlyObservableCollection<T> FilteredItems
        //{
        //    get => fitems;
        //    set => SetAndRaise(ref fitems, value);
        //}


        public object PageRequest
        {
            get { return (object)GetValue(PageRequestProperty); }
            set { SetValue(PageRequestProperty, value); }
        }



        private static void PageRequestChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PaginatedUserControl).PageRequestSubject.OnNext((PageRequest)e.NewValue);

        }


        private static void PaginatedItemsChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        object lck = new object();

        ISubject<PageRequest> PageRequestSubject = new Subject<PageRequest>();



        public PaginatedUserControl()
        {
            InitializeComponent();
            usercontrol.DataContext = this;
            var items = ItemsSubject.SelectMany(_ => _.Cast<object>()).ToObservableChangeSet();

            items.
          /*Filter(filter).*/Page(PageRequestSubject)
                         .Do(_ =>
                         {
                             lock (lck)
                             {
                                 CurrentPage = ((IPageChangeSet<object>)_).Response.Page;
                                 PageSize =  ((IPageChangeSet<object>)_).Response.PageSize;
                             }
                             
                         })

                         .Bind(
                out pitems)
       .DisposeMany()
            .Subscribe(_ => this.Dispatcher.InvokeAsync(() => PaginatedItems = pitems, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken)));
          



            //x.Synchronize(lck).DisposeMany()
            //        .ObserveOn(s)
            //                     //.Page(request)
            //                     .Bind(out allitems)
            //        .Subscribe();
        }

    }
}
