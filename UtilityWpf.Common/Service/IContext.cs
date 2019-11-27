using System;

namespace UtilityWpf
{
    public interface IContext
    {
        //bool IsSynchronized { get; }
        void Invoke(Action action);

        void BeginInvoke(Action action);
    }
}