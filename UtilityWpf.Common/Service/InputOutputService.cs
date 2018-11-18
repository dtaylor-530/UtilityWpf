using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using UtilityInterface;

namespace UtilityWpf
{
    public abstract class InputOutputService<T,R>: IOutputService<R>
    {
        public InputOutputService(IService<T> input, IDelayedConstructorService<R> f)
        {
            Output = Observable.Create<IObservable<R>>(observer =>
            {
                return f.Init(input.Resource)
                    .ToObservable().Subscribe(_ =>
                    {
                        observer.OnNext(f.Resource);
                    });
            }).Switch();
        }

        public IObservable<R> Output { get; set; }

    }
}
