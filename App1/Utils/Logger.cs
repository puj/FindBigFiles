using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
namespace FindBigFiles.Utils
{
    class Logger
    {
        private static void l(String s)
        {
            Debug.WriteLine(s);
        }
        public static void i(String s)
        {
            Debug.WriteLine("I: " + s);
        }
        public static void w(String s)
        {
            Debug.WriteLine("W: " + s);
        }
        public static void e(String s)
        {
            Debug.WriteLine("E: " + s);
        }
    }
}
