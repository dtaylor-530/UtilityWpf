﻿using DynamicData;
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
    public class NavigatorControl : Control
    {
        private SkipControl SkipControl;

        //public static readonly DependencyProperty OutputProperty = DependencyProperty.Register("Output", typeof(object), typeof(NavigatorControl), new PropertyMetadata(null, OutputChanged));

        //private static void OutputChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    (d as NavigatorControl).OutputChanges.OnNext(e.NewValue);
        //}

        public int Current
        {
            get { return (int)GetValue(CurrentProperty); }
            set { SetValue(CurrentProperty, value); }
        }


        public static readonly DependencyProperty CurrentProperty = DependencyProperty.Register("Current", typeof(int), typeof(NavigatorControl), new PropertyMetadata(1, CurrentChanged, CurrentCoerce));

        private static object CurrentCoerce(DependencyObject d, object baseValue)
        {
            if ((int)baseValue <= 0)
                return 1;
            else if ((int)baseValue > (d as NavigatorControl).Size)
                return (d as NavigatorControl).Size;
            else
                return (int)baseValue;
        }

        private static void CurrentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as NavigatorControl).CurrentChanges.OnNext((int)e.NewValue);
        }
        ISubject<int> CurrentChanges = new Subject<int>();
        ISubject<object> OutputChanges = new Subject<object>();

        public int Size
        {
            get { return (int)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }


        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register("Size", typeof(int), typeof(NavigatorControl), new PropertyMetadata(1, _SizeChanged/*, SizeCoerce*/));

        //private static object SizeCoerce(DependencyObject d, object baseValue)
        //{
        //    if ((int)baseValue <= 0)
        //        return 1;
        //    else if ((int)baseValue < (d as NavigatorControl).Current)
        //        return (d as NavigatorControl).Current;
        //    else
        //        return (int)baseValue;
        //}

        private static void _SizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as NavigatorControl).SizeChanges.OnNext((int)e.NewValue);
        }
        ISubject<int> SizeChanges = new Subject<int>();



        bool canMoveBack = true;
        bool canMoveForward = true;
        public override void OnApplyTemplate()
        {
            SkipControl = this.GetTemplateChild("SkipControl") as SkipControl;

            SkipControl.CanMoveToNext = canMoveForward;
            SkipControl.CanMoveToPrevious = canMoveBack;


            Observable.FromEventPattern<RoutedEventHandler, EventArgs>(ev => SkipControl.Skip += ev, ev => SkipControl.Skip -= ev)
        //.WithLatestFrom(SizeChanges, (a, b) => new { a, b })
        .Subscribe(_ =>
                {
                    this.Dispatcher.InvokeAsync(() =>
                    {
                        Current += ((SkipControl.SkipRoutedEventArgs)_.EventArgs).Direction == UtilityEnum.Direction.Forward ? 1 : -1;
                        RaiseSelectedIndexEvent(Current);
                        SkipControl.CanMoveToNext = Current < Size;
                        SkipControl.CanMoveToPrevious = Current > 1;
                    }, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));

                });



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

            // .Scan(1, (a, b) => a + (int)b)

            SizeChanges.Subscribe(_ =>
            {
                if (Current > _)
                { 
                    Current = _;
                    Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        Current = Current;
                    }, System.Windows.Threading.DispatcherPriority.Background);
                    }
                    canMoveForward = Current < _;
                canMoveBack = Current > 1;

                if (SkipControl != null)
                {
                    SkipControl.CanMoveToNext = canMoveForward;
                    SkipControl.CanMoveToPrevious = canMoveBack;

                }
              


            });

        }


        public static readonly RoutedEvent SelectedIndexEvent = EventManager.RegisterRoutedEvent("SelectedIndex", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NavigatorControl));


        public event RoutedEventHandler SelectedIndex
        {
            add { AddHandler(SelectedIndexEvent, value); }
            remove { RemoveHandler(SelectedIndexEvent, value); }
        }

        void RaiseSelectedIndexEvent(int index)
        {
            SelectedIndexRoutedEventArgs newEventArgs = new SelectedIndexRoutedEventArgs(NavigatorControl.SelectedIndexEvent) { Index = index };
            RaiseEvent(newEventArgs);
        }


        public class SelectedIndexRoutedEventArgs : RoutedEventArgs
        {
            public int Index { get; set; }

            public SelectedIndexRoutedEventArgs(RoutedEvent @event) : base(@event)
            {

            }

        }

    }
}



