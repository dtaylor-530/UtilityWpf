using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityHelper;
using UtilityInterface;
using UtilityInterface.NonGeneric;

namespace UtilityWpf
{
    public class ContainsFilter : IFilter
    {
        string _a = null;
        string _property = null;

        public ContainsFilter(string a)
        {
            _a = a;
        }

        public ContainsFilter(string a, string property=null)
        {
            _property = property;
            _a = a;
        }

        public bool Filter(object o) =>            _property == null ? ((string)o).Contains(_a) : ((o.GetPropertyValue<string>(_property))).Contains(_a);
    }

  



}
