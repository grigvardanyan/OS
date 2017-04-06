using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Hardware
{
    class Clock
    {
        private static System.Timers.Timer timer;
        private static int count = 0;
        public static void SetTimer()
        {

            timer = new System.Timers.Timer(200);

            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {

            Console.WriteLine("Clock  -Interapt-  count ::  {0}",
            ++count);
        }
    }
}
