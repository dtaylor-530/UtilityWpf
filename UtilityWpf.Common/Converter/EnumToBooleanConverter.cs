﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using UtilityEnum;

namespace UtilityWpf
{

    [ValueConversion(typeof(UtilityEnum.ProcessState), typeof(bool))]
    public class EnumToBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,            System.Globalization.CultureInfo culture)
        {
           return value == ((System.Collections.IEnumerable)parameter).Cast<object>().ToArray()[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {

            var a=((System.Collections.IEnumerable)parameter).Cast<object>().ToArray();
            return  ((bool)value) ? a[0] :a[1];
        }

        #endregion
    }


}
