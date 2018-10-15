using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityWpf
{
    public static class LinqEx
    {

        public static object First(this IEnumerable enumerable)
        {

            IEnumerator enumerator = enumerable.GetEnumerator();
            enumerator.MoveNext();

                return enumerator.Current;

        }
        public static object FirstOrDefault2(this IEnumerable enumerable)
        {

            IEnumerator enumerator = enumerable.GetEnumerator();
            enumerator.MoveNext();
            try
            {
                return enumerator.Current;
            }
            catch (InvalidOperationException e)
            {
                return null;
            }
        }

        public static IEnumerable FilterByIndex(this IEnumerable enumerable, IEnumerable<int> indices)
        {

            IEnumerator enumerator = enumerable.GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext())
            {
                if (indices.Contains(i))
                    yield return enumerator.Current;
                i++;
            }

        }

        public static int Count(this IEnumerable enumerable)
        {

            IEnumerator enumerator = enumerable.GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext())
            {
                i++;
            }

            return i;
        }


    }
}
