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
using System.Collections;

namespace UtilityWpf.View
{
    /// <summary>
    /// Interaction logic for TableUserControl.xaml
    /// </summary>
    public partial class TableUserControl : UserControl
    {

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(IEnumerable), typeof(TableUserControl));


        public IEnumerable Items
        {
            get { return (IEnumerable)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }



        public TableUserControl()
        {
            InitializeComponent();
            usercontrol.DataContext = this;
        }
    }
}
