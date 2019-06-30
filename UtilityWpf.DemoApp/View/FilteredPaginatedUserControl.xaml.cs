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

namespace UtilityWpf.DemoApp
{
    /// <summary>
    /// Interaction logic for FilteredPaginatedUserControl.xaml
    /// </summary>
    public partial class FilteredPaginatedUserControl : UserControl
    {
        public FilteredPaginatedUserControl()
        {
            InitializeComponent();
            this.DataContext = Faker.EnumerableExtensions.Times(100, i => new Data
            {
                A = Faker.Date.Birthday(i % 18, i % 365).ToString(),
                B = Faker.Company.CatchPhrase(),
                C = Faker.Education.Major(),
                D = Faker.Name.Ethnicity()
            });
        }

        class Data
        {
            public string A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D { get; set; }
        }

    }

   
}
