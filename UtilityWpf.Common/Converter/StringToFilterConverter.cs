using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using UtilityInterface;
using UtilityInterface.NonGeneric;
using UtilityWpf;

namespace UtilityWpf
{


    // Converts enumerable's to a distinct list of given property's (parameter)  value
    [ValueConversion(typeof(string), typeof(IFilter))]
    public class StringToFilterConverter : IValueConverter
    {


        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,  System.Globalization.CultureInfo culture)
        {
            return new ContainsFilter((string)value,(string)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter,  System.Globalization.CultureInfo culture)
        {
            return !(bool)value ? new Object() : null;
        }

        #endregion

    }

}
