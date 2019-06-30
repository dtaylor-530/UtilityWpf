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
using UtilityInterface;
using UtilityInterface.Generic;

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

        private static IList<TreeItemModel> BuildTreeModel()
        {
            return new []
            {
                new TreeItemModel("", 1),
                         new TreeItemModel("",2)
            };
            //    {
            //        //IsExpanded = true,
            //        Children = new List<TreeItemViewModel>
            //        {
            //            new TreeItemViewModel("Node 1.1"),
            //            new TreeItemViewModel("Node 1.2")
            //            {
            //                Children = new List<TreeItemViewModel>
            //                {
            //                    new TreeItemViewModel("Node 1.2.1"),
            //                    new TreeItemViewModel("Node 1.2.2"),
            //                    new TreeItemViewModel("Node 1.2.3"),
            //                    new TreeItemViewModel("Node 1.2.4"),
            //                    new TreeItemViewModel("Node 1.2.5"),
            //                    new TreeItemViewModel("Node 1.2.6")
            //                }
            //            }
            //        }
            //    },
            //    new TreeItemViewModel("Node 2")
            //    {
            //        Children = new List<TreeItemViewModel>
            //        {
            //            new TreeItemViewModel("Node 2.2.1"),
            //            new TreeItemViewModel("Node 2.2.2"),
            //            new TreeItemViewModel("Node 2.2.3"),
            //            new TreeItemViewModel("Node 2.2.4")
            //        }
            //    }
            //};
        }



        public IList<TreeItemModel> RootNodes { get;  }
    }


    public class TreeItemModel:IParent<TreeItemModel>, UtilityWpf.IDelayedConstructor
    {
        private string _number;

        public TreeItemModel(string parent, int number)
        {
            _number= parent + number;
            Name = "Node " + _number;
        }

 

        //public bool IsExpanded {
        //    get;
        //    set; }


        //public bool IsSelected
        //{
        //    get;
        //    set;
        //}

        public string Name { get;  }

        public IEnumerable<TreeItemModel> Children { get;  set; }

        public Task<bool> Init(object o)=>
            Task.Run(() =>
            {
                Children = new[]
                 {
                new TreeItemModel(_number, 1),
                         new TreeItemModel(_number,2)
            }; return true;
            });

        
    }
}
