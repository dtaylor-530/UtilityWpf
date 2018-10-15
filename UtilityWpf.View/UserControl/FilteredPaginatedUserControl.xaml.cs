
using System;
using System.Collections.Generic;
using System.Linq;
using DynamicData;
using System.Reactive.Subjects;
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
using UtilityWpf.ViewModel;
using System.Reactive.Linq;
using System.Collections;
using System.Reactive.Concurrency;

namespace UtilityWpf.View
{
    /// <summary>
    /// Interaction logic for FilteredPaginatedUserControl.xaml
    /// </summary>
    public partial class FilteredPaginatedUserControl : UserControl
    {

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(IEnumerable), typeof(FilteredPaginatedUserControl),new PropertyMetadata (null,ItemsChange));
        private DispatcherScheduler ds;


        public IEnumerable Items
        {
            get { return (IEnumerable)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }




        private static void ItemsChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FilteredPaginatedUserControl;
            foreach(var x in (IEnumerable)e.NewValue)
            control.ItemsSubject.OnNext(x);
 

        }

        ISubject<object> ItemsSubject = new Subject<object>();

        static FilteredPaginatedUserControl()
        {

        }

        public FilteredPaginatedUserControl()
        {
             ds = new System.Reactive.Concurrency.DispatcherScheduler(Application.Current.Dispatcher );
            InitializeComponent();

            usercontrol .DataContext = new FilteredPaginatedViewModel<dynamic>(ItemsSubject.Distinct(), Observable.Repeat<Func<dynamic, bool>>(a => true, 1), ds, 25);

        }

    }
}
