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
    /// Interaction logic for FileFolderUserControl.xaml
    /// </summary>
    public partial class FileUserControl : UserControl
    {
        public static DependencyProperty FileProperty = DependencyProperty.Register("File", typeof(string), typeof(FileUserControl));


        public string File
        {
            get { return (string)GetValue(FileProperty); }
            set { SetValue(FileProperty, value); }
        }


        public FileUserControl()
        {
            InitializeComponent();
            usercontrol.DataContext = this;
        }
    }
}
