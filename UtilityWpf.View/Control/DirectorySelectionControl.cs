using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UtilityWpf.View
{
    public class DirectorySelectionControl : Control
    {
        public static readonly DependencyProperty DirectoryProperty = DependencyProperty.Register("Directory", typeof(string), typeof(DirectorySelectionControl));

        //public static readonly DependencyProperty OutputProperty = DependencyProperty.Register("Output", typeof(object), typeof(DirectorySelectionControl));

        //public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(Type), typeof(SelectionUserControl));


        public string Directory
        {
            get { return (string)GetValue(DirectoryProperty); }
            set { SetValue(DirectoryProperty, value); }
        }



        //public object Output
        //{
        //    get { return GetValue(OutputProperty); }
        //    //set { SetValue(OutputProperty, value); }
        //}


        static DirectorySelectionControl()
        {

            DefaultStyleKeyProperty.OverrideMetadata(typeof(DirectorySelectionControl), new FrameworkPropertyMetadata(typeof(DirectorySelectionControl)));

        }



        public DirectorySelectionControl()
        {

            Uri resourceLocater = new Uri("/UtilityWpf.View;component/Themes/DirectorySelection.xaml", System.UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            Style = resourceDictionary["DirectorySelectionStyle"] as Style;



        }
    }
}