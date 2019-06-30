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


namespace UtilityWpf.View
{
    public class SkipControl : Control
    {

        public static readonly DependencyProperty PreviousCommandProperty = DependencyProperty.Register("PreviousCommand", typeof(ICommand), typeof(SkipControl));

        public static readonly DependencyProperty NextCommandProperty = DependencyProperty.Register("NextCommand", typeof(ICommand), typeof(SkipControl));

        public static readonly DependencyProperty CanMoveToNextProperty = DependencyProperty.Register("CanMoveToNext", typeof(bool), typeof(SkipControl), new PropertyMetadata(true, CanMoveToNextChanged));

        public static readonly DependencyProperty CanMoveToPreviousProperty = DependencyProperty.Register("CanMoveToPrevious", typeof(bool), typeof(SkipControl), new PropertyMetadata(true, CanMoveToPreviousChanged));

        //public static readonly DependencyProperty OutputProperty = DependencyProperty.Register("Output", typeof(object), typeof(SkipControl), new PropertyMetadata( 0 ));


        ISubject<int> SizeChanges = new Subject<int>();


        private static void CanMoveToNextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as SkipControl).CanMoveToNextChanges.OnNext((bool)e.NewValue);
        }



        private static void CanMoveToPreviousChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as SkipControl).CanMoveToPreviousChanges.OnNext((bool)e.NewValue);
        }

        public bool CanMoveToNext
        {
            get { return (bool)GetValue(CanMoveToNextProperty); }
            set { SetValue(CanMoveToNextProperty, value); }
        }


        public bool CanMoveToPrevious
        {
            get { return (bool)GetValue(CanMoveToPreviousProperty); }
            set { SetValue(CanMoveToPreviousProperty, value); }
        }

        //public object Output
        //{
        //    get { return (object)GetValue(OutputProperty); }
        //    set { SetValue(OutputProperty, value); }
        //}

        //static SkipControl()
        //{
        //    DefaultStyleKeyProperty.OverrideMetadata(typeof(SkipControl), new FrameworkPropertyMetadata(typeof(SkipControl)));
        //}

        ISubject<bool> CanMoveToNextChanges = new Subject<bool>();
        ISubject<bool> CanMoveToPreviousChanges = new Subject<bool>();


        public SkipControl()
        {

            Uri resourceLocater = new Uri("/UtilityWpf.View;component/Themes/SkipStyle.xaml", System.UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            Style = resourceDictionary["SkipStyle"] as Style;

            var nextCommand = new ReactiveCommand(CanMoveToNextChanges);
            var previousCommand = new ReactiveCommand(CanMoveToPreviousChanges);
            this.SetValue(NextCommandProperty, nextCommand);
            this.SetValue(PreviousCommandProperty, previousCommand);

            (nextCommand as ReactiveCommand).Select(_ => UtilityEnum.Direction.Forward).Merge(previousCommand.Select(_ => UtilityEnum.Direction.Backward)).Subscribe(direction =>
            {
                this.Dispatcher.InvokeAsync(() => RaiseSkipEvent(direction), System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));
            });


        }


        public static readonly RoutedEvent SkipEvent = EventManager.RegisterRoutedEvent("Skip", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SkipControl));


        public event RoutedEventHandler Skip
        {
            add { AddHandler(SkipEvent, value); }
            remove { RemoveHandler(SkipEvent, value); }
        }

        void RaiseSkipEvent(UtilityEnum.Direction direction)
        {
            SkipRoutedEventArgs newEventArgs = new SkipRoutedEventArgs(SkipControl.SkipEvent) { Direction =direction };
            RaiseEvent(newEventArgs);
        }


        public class SkipRoutedEventArgs : RoutedEventArgs
        {
            public UtilityEnum.Direction Direction { get; set; }

            public SkipRoutedEventArgs(RoutedEvent @event) : base(@event)
            {

            }

        }


    }

}




