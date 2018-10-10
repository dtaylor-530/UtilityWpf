using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityWpf.ViewModel
{
    public class ButtonDefinition
    {
        public string Content { get; set; }
        public RelayCommand Command { get; set; }
    }

}
