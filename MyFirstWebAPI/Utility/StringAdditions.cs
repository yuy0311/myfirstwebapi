using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstWebAPI.Utility
{
    public class StringAdditions
    {
        public static Object ConvertToBoolean(string value)
        {
            Nullable<Boolean> b = null;
            try
            {
                b =  Convert.ToBoolean(value);
                return b;
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to convert '{0}' to a Boolean.", value);
            }
            return b;
        }
    }
}