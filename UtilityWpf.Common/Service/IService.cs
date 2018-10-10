using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityWpf.Common
{ 
    public interface IService<T>
    {
        IObservable<T> Resource { get; }
    }

}
