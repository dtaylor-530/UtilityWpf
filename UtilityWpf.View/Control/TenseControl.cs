using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UtilityWpf.View
{
    public class TenseControl : Control
    {
        WPFToggleSwitch.ToggleSwitch ToggleSwitch;
        static TenseControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TenseControl), new FrameworkPropertyMetadata(typeof(TenseControl)));
        }


        public override void OnApplyTemplate()
        {
            ToggleSwitch = this.GetTemplateChild("ToggleSwitch") as WPFToggleSwitch.ToggleSwitch;
            ToggleSwitch.Checked += ToggleSwitch_Checked;
            ToggleSwitch.Unchecked += ToggleSwitch_Unchecked;
        }

        private void ToggleSwitch_Checked(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new EnumEventArgs(SelectedEvent, true));
        }

        private void ToggleSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new EnumEventArgs(SelectedEvent, false));
        }


        public TenseControl()
        {
            //Uri resourceLocater = new Uri("/UtilityWpf.View;component/Themes/TenseControl.xaml", System.UriKind.Relative);
            //ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            //Style = resourceDictionary["HeaderBodyControlStyle"] as Style;
        }


        public static readonly RoutedEvent SelectedEvent = EventManager.RegisterRoutedEvent("Selected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TenseControl));

        // .NET wrapper
        public event RoutedEventHandler Selected
        {
            add { AddHandler(SelectedEvent, value); }
            remove { RemoveHandler(SelectedEvent, value); }
        }

        // Raise the routed event "selected"
        public class EnumEventArgs : RoutedEventArgs
        {
            public bool IsChecked;
            public EnumEventArgs(RoutedEvent routedEvent, bool isChecked) : base(routedEvent)
            {
                IsChecked = isChecked;
            }
        }
    }
}
