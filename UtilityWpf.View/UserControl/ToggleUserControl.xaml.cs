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

namespace UtilityWpf.View
{
    /// <summary>
    /// Interaction logic for ToggleUserControl.xaml
    /// </summary>
    public partial class ToggleUserControl : UserControl
    {



        public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(ToggleUserControl));

        public static DependencyProperty UnCheckedProperty = DependencyProperty.Register("UnChecked", typeof(string), typeof(ToggleUserControl));

        public static DependencyProperty CheckedProperty = DependencyProperty.Register("Checked", typeof(string), typeof(ToggleUserControl));

        public static DependencyProperty InputProperty = DependencyProperty.Register("Input", typeof(bool), typeof(ToggleUserControl));



 

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string UnChecked
        {
            get { return (string)GetValue(UnCheckedProperty); }
            set { SetValue(UnCheckedProperty, value); }
        }

        public string Checked
        {
            get { return (string)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        public bool Input
        {
            get { return (bool)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }




        public ToggleUserControl()
        {
            InitializeComponent();
            usercontrol.DataContext = this;

        }
    }
}
