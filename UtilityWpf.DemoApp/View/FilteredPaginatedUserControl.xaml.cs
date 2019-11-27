using System.Windows.Controls;

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

        private class Data
        {
            public string A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D { get; set; }
        }
    }
}