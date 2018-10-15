using DynamicData;
using Reactive.Bindings;
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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UtilityWpf.View
{
    /// <summary>
    /// Interaction logic for NavigatorUserControl.xaml
    /// </summary>
    public partial class NavigatorUserControl : UserControl
    {

        public static readonly DependencyProperty PreviousCommandProperty = DependencyProperty.Register("PreviousCommand", typeof(ICommand), typeof(NavigatorUserControl));

        public static readonly DependencyProperty NextCommandProperty = DependencyProperty.Register("NextCommand", typeof(ICommand), typeof(NavigatorUserControl));

        public static readonly DependencyProperty PageSizeProperty = DependencyProperty.Register("PageSize", typeof(int), typeof(NavigatorUserControl), new PropertyMetadata(25, PageSizeChange));

        public static readonly DependencyProperty CurrentPageProperty = DependencyProperty.Register("CurrentPage", typeof(int), typeof(NavigatorUserControl), new PropertyMetadata(1, CurrentPageChange));

        public static readonly DependencyProperty PageRequestProperty = DependencyProperty.Register("PageRequest", typeof(DynamicData.PageRequest), typeof(NavigatorUserControl));




        public ICommand PreviousCommand
        {
            get { return (ICommand)GetValue(PreviousCommandProperty); }
            set { SetValue(PreviousCommandProperty, value); }
        }

        public ICommand NextCommand
        {
            get { return (ICommand)GetValue(NextCommandProperty); }
            set { SetValue(NextCommandProperty, value); }
        }

        public DynamicData.PageRequest PageRequest
        {
            get { return (DynamicData.PageRequest)GetValue(PageRequestProperty); }
            set { SetValue(PageRequestProperty, value); }
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




        private static void CurrentPageChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NavigatorUserControl control = d as NavigatorUserControl;
            control.CurrentPageSubject.OnNext((int)e.NewValue);
        }
        private static void PageSizeChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NavigatorUserControl control = d as NavigatorUserControl;
            control.PageSizeSubject.OnNext((int)e.NewValue);
        }




        public NavigatorUserControl()
        {
            InitializeComponent();
            usercontrol.DataContext = this;
            NextCommand = new ReactiveCommand();
            PreviousCommand = new ReactiveCommand();

            (NextCommand as ReactiveCommand).Subscribe(_ => 
            {

            });
            //Size = new ReactiveProperty<int>(pageSize);

            var obs = (CurrentPageSubject.StartWith(1)).DistinctUntilChanged().CombineLatest(PageSizeSubject.StartWith(25), (a, b) =>
            new { page = a, size = b });

            var Output = (NextCommand as ReactiveCommand)
                .WithLatestFrom(obs, (a, b) => new PageRequest(b.page + 1, b.size))
                .Merge((PreviousCommand as ReactiveCommand)
                .WithLatestFrom(obs, (a, b) => new PageRequest(b.page - 1, b.size)))
                .StartWith(new PageRequest(1, 25)).ToReactiveProperty();

            Output.Subscribe(_ =>
            {
                this.Dispatcher.InvokeAsync(() => PageRequest = _, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));

            });

        }



        // public class PageNavigatorViewModel : NavigatorVM, IOutputViewModel<DynamicData.PageRequest>
        // {
        //     public IObservable<DynamicData.PageRequest> Output { get; set; }

        //     public PageNavigatorViewModel(IObservable<int> currentPage, IObservable<int> pageSize)
        //     {
        //         Size = new ReactiveProperty<int>(pageSize);

        //         var obs = currentPage.DistinctUntilChanged().CombineLatest(Size, (a, b) =>
        //         new { page = a, size = b });

        //         Output = NextCommand.WithLatestFrom(obs, (a, b) => new PageRequest(b.page + 1, b.size)).Merge
        //(PreviousCommand.WithLatestFrom(obs, (a, b) =>
        //new PageRequest(b.page - 1, b.size))).StartWith(new PageRequest(1, 25)).ToReactiveProperty();


        //     }

        // }







    }
}