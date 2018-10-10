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
    /// Interaction logic for MultiSelectTreeView.xaml
    /// </summary>
    public partial class MultiSelectTreeView : UserControl
    {
        public MultiSelectTreeView()
        {
            InitializeComponent();
            this.DataContext = new TreeViewModel();
        }
    }


    public class TreeViewModel
    {

        public TreeViewModel()
        {
            RootNodes = BuildTreeModel();
        }

        private static List<TreeItemViewModel> BuildTreeModel()
        {
            return new List<TreeItemViewModel>
            {
                new TreeItemViewModel("Node 1")
                {
                    //IsExpanded = true,
                    Children = new List<TreeItemViewModel>
                    {
                        new TreeItemViewModel("Node 1.1"),
                        new TreeItemViewModel("Node 1.2")
                        {
                            Children = new List<TreeItemViewModel>
                            {
                                new TreeItemViewModel("Node 1.2.1"),
                                new TreeItemViewModel("Node 1.2.2"),
                                new TreeItemViewModel("Node 1.2.3"),
                                new TreeItemViewModel("Node 1.2.4"),
                                new TreeItemViewModel("Node 1.2.5"),
                                new TreeItemViewModel("Node 1.2.6")
                            }
                        }
                    }
                },
                new TreeItemViewModel("Node 2")
                {
                    Children = new List<TreeItemViewModel>
                    {
                        new TreeItemViewModel("Node 2.2.1"),
                        new TreeItemViewModel("Node 2.2.2"),
                        new TreeItemViewModel("Node 2.2.3"),
                        new TreeItemViewModel("Node 2.2.4")
                    }
                }
            };
        }



        public List<TreeItemViewModel> RootNodes { get; set; }
    }


    public class TreeItemViewModel
    {
        public TreeItemViewModel(string displayName)
        {
            Name = displayName;
        }

        public bool IsExpanded {
            get;
            set; }


        public bool IsSelected
        {
            get;
            set;
        }

        public string Name { get;  }

        public List<TreeItemViewModel> Children { get; set; }

   
    }
}
