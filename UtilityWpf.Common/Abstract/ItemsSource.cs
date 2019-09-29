using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityWpf.Abstract
{
    public interface IItemsSource
    {
         IEnumerable ItemsSource { get; set; }
    }
}
