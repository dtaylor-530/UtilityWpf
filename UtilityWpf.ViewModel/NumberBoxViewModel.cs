using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityWpf.ViewModel
{
    public class NumberBoxViewModel:OutputService<int>
    {

        public string Title { get; }
        public int Minimum { get; } = 1;
        public int Maximum { get; } = 100;
        public NumberBoxViewModel(string title="", int value=1)
         {
            Output = new ReactiveProperty<int>(value);
            Title = title;

        }
}
}
