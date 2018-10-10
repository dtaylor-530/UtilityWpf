using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace UtilityWpf.ViewModel
{


    public class TimerViewModel<T>
    {

        private TimerViewModel()
        {
            Time = TimerSingleton.Instance.Time.ToReadOnlyReactiveProperty();
        }

        public ReadOnlyReactiveProperty<DateTime> Time { get; }


        public T Object { get; set; }
    }


    public class TimerSingleton
    {


        public IObservable<DateTime> Time { get; private set; }

        private static TimerSingleton instance;


        private TimerSingleton()
        {
            DateTime d = DateTime.Now;

            Time = Observable.Interval(TimeSpan.FromSeconds(5))
                .Select(_=>d+ TimeSpan.FromTicks(_))
                .Publish();

        }

        
        public static TimerSingleton Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new TimerSingleton();
                    }
                    return instance;
                }
            }

    }
    
}
