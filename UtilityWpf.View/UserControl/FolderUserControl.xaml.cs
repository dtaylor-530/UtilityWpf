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
    /// Interaction logic for FolderUserControl.xaml
    /// </summary>
    public partial class FolderUserControl : UserControl
    {




        public static DependencyProperty DirectoryProperty = DependencyProperty.Register("Directory", typeof(string), typeof(FolderUserControl));


        public string Directory
        {
            get { return (string)GetValue(DirectoryProperty); }
            set { SetValue(DirectoryProperty, value); }
        }


        public FolderUserControl()
        {
            InitializeComponent();
            usercontrol.DataContext = this;
         
        }
    }
}
