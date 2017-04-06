using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hardware
{
    namespace IO
    {
        class Monitor
        {
            public static void Output(object obj)
            {
                Console.WriteLine(obj);
            }
        }

        class Keyboard
        {
            public static String Input()
            {
                return Console.ReadLine();
            }
        }
    }
}
