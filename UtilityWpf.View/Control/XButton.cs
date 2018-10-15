using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UtilityWpf.View
{
    public class XButton : PathButton
    {

        public XButton()
        {
            var myResourceDictionary = new System.Windows.ResourceDictionary();
            myResourceDictionary.Source =
                new Uri("/UtilityWpf.View;component/Themes/Geometry.xaml",
                        UriKind.RelativeOrAbsolute);
            var path = myResourceDictionary["Cross"];
            PathData = (System.Windows.Media.Geometry)path;

            HoverBackground = new System.Windows.Media.SolidColorBrush(Colors.IndianRed);
        }

    }
}
