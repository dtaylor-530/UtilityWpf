using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityWpf.ViewModel
{
    public class DataRangeViewModel
    {

        public ReactiveProperty<DateTime> From { get; set; } = new ReactiveProperty<DateTime>(new DateTime(2018, 6, 14));

        public ReactiveProperty<DateTime> To { get; set; } = new ReactiveProperty<DateTime>(new DateTime(2018, 7, 15));

        public ReactiveProperty<UtilityModel.Range<DateTime>> Output { get; }


        public DataRangeViewModel()
        {
            Output = From.CombineLatest(To, (a, b) =>new { a, b })
                .Where(g=>g.a<g.b)
                .Select(_=> new UtilityModel.Range<DateTime> { Minimum = _.a, Maximum = _.b })
                .Throttle(TimeSpan.FromSeconds(1))
                .ToReactiveProperty();


        }

    }
}
