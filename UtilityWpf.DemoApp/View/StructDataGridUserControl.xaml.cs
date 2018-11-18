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

namespace UtilityWpf.DemoApp
{
    /// <summary>
    /// Interaction logic for StructDataGridUserControl.xaml
    /// </summary>
    public partial class StructDataGridUserControl : UserControl
    {
        public StructDataGridUserControl()
        {
            InitializeComponent();
            this.DataContext = Enumerable.Range(0, 4).Select(_ =>new Point(_,_*3)).ToList();
        }
    }
}
