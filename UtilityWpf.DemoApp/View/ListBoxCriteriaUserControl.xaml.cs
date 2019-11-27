using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace UtilityWpf.DemoApp
{
    /// <summary>
    /// Interaction logic for ListBoxCriteria.xaml
    /// </summary>
    public partial class ListBoxCriteriaUserControl : UserControl
    {
        public ListBoxCriteriaUserControl()
        {
            Random random = new Random();
            InitializeComponent();
            passFail.ItemsSource = Enumerable.Range(0, 20).Select(_ =>
        new PassFail
        {
            Key = Faker.Name.FirstName(),
            Expired = System.Convert.ToBoolean(random.Next(0, 2))
        });
        }
    }

    internal class PassFail
    {
        public string Key { get; set; }

        public bool Expired { get; set; }
    }

    //class EventArgsConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return (value as View.ListBoxCriteria.CriteriaMetEventArgs).Indices;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    internal class ListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var x = (value) as IEnumerable;
            if (x == null)
                return DependencyProperty.UnsetValue;
            else
                return x;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}