using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityWpf;

namespace CustomHelper
{
    public static class DynmamicHelper
    {


        public static List<Dynamic> OnGetData(IEnumerable enumerable, string key, string value)
        {

            var keys = ((IEnumerable)enumerable.First()).GetPropValues<object>(key);
            //var values= ((IEnumerable)enumerable.First()).GetPropValues(value);

            try
            {
                foreach (var k in keys)
                    Dynamic.AddProperty((string)k, typeof(string));
            }
            catch
            {

            }
            var Customers = new List<Dynamic>();

            foreach (var en in enumerable)
            {
                var values = ((IEnumerable)en).GetPropValues<object>(value);
                Dynamic customer1 = new Dynamic();// { FirstName = "Julie", LastName = "Smith" };
                foreach (var val in values.Cast<object>().Zip(keys.Cast<object>(), (a, b) => new { a, b }))
                    customer1.SetPropertyValue((string)val.b, (string)val.a);


                Customers.Add(customer1);
            }

            return Customers;
        }
    }
}
