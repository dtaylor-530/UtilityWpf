using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

    class PassFail
        {
            public string Key { get; set; }

            public bool Expired { get; set; }

        }


    class EventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as View.ListBoxCriteria.CriteriaMetEventArgs).Indices;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    class ListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var x= (value) as IEnumerable;
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
