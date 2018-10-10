using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace UtilityWpf
{
    public class DispatcherService: IDispatcherService
    {

        public IScheduler UI { get;  }
        public IScheduler Background => TaskPoolScheduler.Default;
        public IScheduler New => NewThreadScheduler.Default;
        public Dispatcher Dispatcher { get; }
        public DispatcherService(Dispatcher dispatcher)
        {

            UI = new DispatcherScheduler(dispatcher);
            Dispatcher = dispatcher;
        }

    }

    public interface IDispatcherService
    {

        IScheduler UI { get; }
        IScheduler Background { get; }
        IScheduler New { get; }
        Dispatcher Dispatcher { get; }

    }
}
