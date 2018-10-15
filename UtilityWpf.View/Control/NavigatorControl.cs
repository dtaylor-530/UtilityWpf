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
using System.Windows.Input;
using UtilityHelper;

namespace UtilityWpf.View
{
    public class NavigatorControl : Control
    {

        public static readonly DependencyProperty PreviousCommandProperty = DependencyProperty.Register("PreviousCommand", typeof(ICommand), typeof(NavigatorControl));

        public static readonly DependencyProperty NextCommandProperty = DependencyProperty.Register("NextCommand", typeof(ICommand), typeof(NavigatorControl));

        public static readonly DependencyProperty PageSizeProperty = DependencyProperty.Register("PageSize", typeof(int), typeof(NavigatorControl), new PropertyMetadata(2, PageSizeChange));

        public static readonly DependencyProperty CurrentPageProperty = DependencyProperty.Register("CurrentPage", typeof(int), typeof(NavigatorControl), new PropertyMetadata(1, CurrentPageChange));

        public static readonly DependencyProperty PageRequestProperty = DependencyProperty.Register("PageRequest", typeof(DynamicData.PageRequest), typeof(NavigatorControl));


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

        //public ReactiveCommand NextCommand { get; }

        //public ReactiveCommand PreviousCommand { get; }

        ISubject<int> CurrentPageSubject = new Subject<int>();
        ISubject<int> PageSizeSubject = new Subject<int>();




        private static void CurrentPageChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NavigatorControl control = d as NavigatorControl;
            control.CurrentPageSubject.OnNext((int)e.NewValue);
        }
        private static void PageSizeChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NavigatorControl control = d as NavigatorControl;
            control.PageSizeSubject.OnNext((int)e.NewValue);
        }







        static NavigatorControl()
        {

            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigatorControl), new FrameworkPropertyMetadata(typeof(NavigatorControl)));

        }



        public NavigatorControl()
        {

            Uri resourceLocater = new Uri("/UtilityWpf.View;component/Themes/NavigatorStyle.xaml", System.UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            Style = resourceDictionary["NavigatorStyle"] as Style;

            NextCommand = new ReactiveCommand();
            PreviousCommand = new ReactiveCommand();

            (NextCommand as ReactiveCommand).Subscribe(_ =>
            {

            });
            //Size = new ReactiveProperty<int>(pageSize);

            var obs = (CurrentPageSubject).DistinctUntilChanged().CombineLatest(PageSizeSubject, (a, b) =>
            new { page = a, size = b });

            var Output = (NextCommand as ReactiveCommand)
                .WithLatestFrom(obs, (a, b) => new PageRequest(b.page + 1, b.size))
                .Merge((PreviousCommand as ReactiveCommand)
                .WithLatestFrom(obs, (a, b) => new PageRequest(b.page - 1, b.size)))
             /*   .StartWith(new PageRequest(1, 25))*/.ToReactiveProperty();

            Output.Subscribe(_ =>
            {
               this.Dispatcher.InvokeAsync(() => PageRequest = _, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));
                //PageRequest = _;
            });
        }


    }

}




