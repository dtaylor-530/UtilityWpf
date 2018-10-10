using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityWpf.ViewModel
{
    public class SearchParameters : AbstractNotifyPropertyChanged
    {

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set => SetAndRaise(ref _searchText, value);
        }




    }
}
