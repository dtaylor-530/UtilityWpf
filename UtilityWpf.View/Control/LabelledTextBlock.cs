using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

//https://www.markwithall.com/programming/2014/05/02/labelled-textbox-in-wpf.html
namespace UtilityWpf.View
{
    public partial class LabelledTextBox : Control
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(LabelledTextBox), new FrameworkPropertyMetadata("Unnamed Label"));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(LabelledTextBox), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }

        }



        static LabelledTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LabelledTextBox), new FrameworkPropertyMetadata(typeof(LabelledTextBox)));
        }



        public LabelledTextBox()
        {

            Uri resourceLocater = new Uri("/UtilityWpf.View;component/Themes/LabelledTextBox.xaml", System.UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            Style = resourceDictionary["LabelledTextBoxStyle"] as Style;


        }


    }

}
