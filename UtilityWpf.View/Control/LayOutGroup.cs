using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UtilityWpf.View
{
    //https://www.markwithall.com/programming/2014/05/02/labelled-textbox-in-wpf.html
    public class LayoutGroup : StackPanel
    {
        public LayoutGroup()
        {
            Grid.SetIsSharedSizeScope(this, true);
        }
    }

}
