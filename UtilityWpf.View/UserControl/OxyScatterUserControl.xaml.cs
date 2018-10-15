using System;
using System.Collections;
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
    /// Interaction logic for OxyScatterUserControl.xaml
    /// </summary>
    public partial class OxyScatterUserControl : UserControl
    {

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(IEnumerable), typeof(OxyScatterUserControl));


        public IEnumerable Items
        {
            get { return (IEnumerable)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }


        public OxyScatterUserControl()
        {
            InitializeComponent();
            usercontrol.DataContext = this;
        }
    }
}
