using Reactive.Bindings;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace UtilityWpf.DemoAnimation
{
    public class BeatViewModel
    {
        public ReactiveProperty<double> Rate { get; } = new ReactiveProperty<double>(1d);

        public ReactiveProperty<long> Beat { get; }

        public BeatViewModel()
        {
            Beat = Rate.Where(_ => _ > 0).Select(_ => Observable.Interval(TimeSpan.FromSeconds(1d / _))).Switch().ToReactiveProperty();
        }
    }
}