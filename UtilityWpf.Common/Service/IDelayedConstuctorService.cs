using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityInterface;
using UtilityInterface.Generic;

namespace UtilityWpf
{
    public interface IDelayedConstructorService<R>:IDelayedConstructor,IService<R>
    {
        //R Method(T input);
    }
} 
