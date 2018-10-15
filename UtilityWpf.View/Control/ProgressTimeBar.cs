﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using UtilityWpf;

namespace UtilityWpf.View
{

    public class ProgressTimeBar : ProgressBar
    {


        public TimeSpan MTime
        {
            get { return (TimeSpan)GetValue(MTimeProperty); }
            set { SetValue(MTimeProperty, value); }
        }


        public static readonly DependencyProperty MTimeProperty =
            DependencyProperty.Register("MTime", typeof(TimeSpan), typeof(ProgressTimeBar), new PropertyMetadata(default(TimeSpan)));



        static ProgressTimeBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressTimeBar), new FrameworkPropertyMetadata(typeof(ProgressTimeBar)));
        }

        public ProgressTimeBar() : base()
        {
            Uri resourceLocater = new Uri("/UtilityWpf.View;component/Themes/ProgressTimeBar.xaml", System.UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            Style = resourceDictionary["ProgressTimeBarStyle"] as Style;

            TimeFunction();
        }



        protected virtual void TimeFunction()
        {

            var obs = Observable
      .FromEventPattern<RoutedPropertyChangedEventHandler<double>, RoutedPropertyChangedEventArgs<double>>(h => this.ValueChanged += h, h => this.ValueChanged -= h);

            var x = obs
            .Select(_ => new { _.EventArgs, interval = Observable.Interval(TimeSpan.FromMilliseconds(10)) })
            .Where(_ => /*_.EventArgs.OldValue == 0 ||*/ _.EventArgs.OldValue >= _.EventArgs.NewValue)
            .Select(aa => aa.interval)
            .Switch()
            .WithLatestFrom(obs, (a, b) => new { a, b })
            .TakeWhile(t => t.b.EventArgs.NewValue < 100)
            .Select(t => t.a)
            .Subscribe(_ =>
            {
                this.Dispatcher.InvokeAsync(() => MTime = TimeSpan.FromMilliseconds((long)((double)_) * 10), System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));
            });
        }



    }
}
