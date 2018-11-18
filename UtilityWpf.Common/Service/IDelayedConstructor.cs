using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityWpf
{
    public interface IDelayedConstructor
    {
        Task<bool> Init(object o);
    }
}
