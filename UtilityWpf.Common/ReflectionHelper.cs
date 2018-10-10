using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UtilityWpf
{
    public static class ReflectionHelper
    {
        public static T GetPropValue<T>(this Object obj, String name, Type type = null) => GetPropValue<T>(obj, (type ?? obj.GetType()).GetProperty(name));



        public static T GetPropValue<T, R>(R obj, String name) => GetPropValue<T>(obj, typeof(R).GetProperty(name));



        public static T GetPropValue<T>(this Object obj, PropertyInfo info = null)
        {
            if (info == null) return default(T);
            object retval = info.GetValue(obj, null);
            return retval == null ? default(T) : (T)retval;
        }

        public static IEnumerable<T> GetPropValues<T>(this IEnumerable obj, String name, Type type = null)
        {
            var info = (type ?? obj.First().GetType()).GetProperty(name);
            foreach (var x in obj)
                yield return GetPropValue<T>(x, info);
        }


        public static IEnumerable<T> GetPropValues<T>(this IEnumerable<Object> obj, String name, Type type = null)
        {
            var x = (type ?? obj.First().GetType()).GetProperty(name);
            return obj.Select(_ => GetPropValue<T>(_, x));
        }

        public static IEnumerable<T> GetPropValues<T, R>(IEnumerable<R> obj, String name)
        {
            var x = typeof(R).GetProperty(name);
            return obj.Select(_ => GetPropValue<T>(_, x));
        }

        public static IEnumerable<T> GetPropValues<T>(this IEnumerable<Object> obj, PropertyInfo info = null) => obj.Select(_ => GetPropValue<T>(_, info));


        public static IEnumerable<T> GetPropValues<T>(this IEnumerable obj, PropertyInfo info = null)
        {
            foreach (var x in obj)
                yield return GetPropValue<T>(x, info);
        }



    }



 
}
