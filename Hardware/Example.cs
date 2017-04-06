using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

using Hardware;

namespace Example
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Clock.SetTimer();
            int intValue = 82;
            byte[] intValueOnByteArray = BitConverter.GetBytes(intValue);

            HDD.Write(intValueOnByteArray, 25);

            intValue = 19;
            //intValueOnByteArray = BitConverter.GetBytes(intValue);
            //HDD.Write(intValueOnByteArray, 24);


            byte[] h = new byte[40];

            byte[] stringOnBytes;

            string s = "hes1a ";

            for (int i = 0; i < s.Length; i++)
            {
                stringOnBytes = BitConverter.GetBytes(s[i]);
                HDD.Write(stringOnBytes, i);
            }

            HDD.Read(ref h, 0, 40);



            while (!HDD.isNullReadHandler()) ;
            for (int i = 0; i < h.Length; i++)
            {
                Console.Write(i); Console.WriteLine("  " + h[i]);
            }
        }
    }
}
