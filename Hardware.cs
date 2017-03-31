using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;


/*
    please QLND-ing 
*/


///////////////////////////////////  TEST ///////////////////////////////////////
namespace Hardware
{
    class HDD {

        static private int  MAX_LENGTH_BYTES = 10;

        //   your path can modify
        public static string path =
            @"c:\Users\Home\documents\visual studio 2015\Projects\OS\Hardware\HDD.txt";

        // kaskatelia esi uxaki threadi hamarem senc grel te che amen mi methodi 
        // mejel karam open cloaase anem  
        static private  FileStream fs = File.Open(path, FileMode.Open, FileAccess.ReadWrite);

       private  class Write {
            Write(){}

            public Write(byte[] data, long offset, int length)
            {
                this.data = data;
                this.offset = offset;
                this.length = length;
            }

            public byte[] data;
            public long offset;
            public int length;
        }

        public static void ThreadPoolCallbackWrite(Object threadContext)
        {
            Write threads = (Write)threadContext;
            write(threads.data,threads.offset,threads.length);
        }

        /*
        MAX_OFFSET = log2(HDD.SIZE)
        */
        //[MethodImpl(MethodImplOptions.Synchronized)]
        public static void write(byte[] data, long offset, int length = -1) {

            if (length == -1) length = data.Length;

            int count = 0;
            while(length / MAX_LENGTH_BYTES > 1) {
                byte[] dataX = new byte[MAX_LENGTH_BYTES];

                for (int i = 0; i < MAX_LENGTH_BYTES; i++)
                 dataX[i] = data[count + i];

                count += MAX_LENGTH_BYTES;

                Write state = new Write(dataX, MAX_LENGTH_BYTES, MAX_LENGTH_BYTES);
                ThreadPool.QueueUserWorkItem(HDD.ThreadPoolCallbackWrite, (Object)state);
                length -= MAX_LENGTH_BYTES;
            }

            // Thread newThread = new Thread(raed);
            // newThread.Start(data,offset,length);
            //var fs = File.Open(path,FileMode.Open,FileAccess.Write);

            fs.Seek(offset,SeekOrigin.Begin);

            fs.Write(data, 0, length);

            Console.WriteLine("write "+path);
           // fs.Close();

        }

        private class Read
        {
            Read() { }

            public Read(byte[] data, long offset, int count)
            {
                this.data = data;
                this.offset = offset;
                this.count = count;
            }

            public byte[] data;
            public long offset;
            public int count;
        }

        public static void ThreadPoolCallbackRead(Object threadContext)
        {
            Read threads = (Read)threadContext;
            read(ref threads.data, threads.offset, threads.count);
        }

        /*
            MAX_OFFSET = log2(HDD.SIZE)
       */
        // count <= raed.Length
        //[MethodImpl(MethodImplOptions.Synchronized)]
        public static void read(ref byte[] data, long offset, int count) {

            //var fs = File.Open(path,FileMode.Open,FileAccess.Read);

            /*
                Hl@ chem grel axper asim esqan@ qcem  mi hat qlnges eli )
            */

            fs.Seek(offset, SeekOrigin.Begin);

            fs.Read(data, 0, count);

            //for test :)
            for(int i = 0;i<count; i++) Console.WriteLine(data[i]);

            Console.WriteLine("read "+ path);
            //fs.Close();
        }
    }




    class Program
    {
        static void Main(string[] args)
        {

            //int intValue = 13;
            //byte [] intValueOnByteArray =  BitConverter.GetBytes(intValue);

            byte[] intBytes = new byte[31];
            for (byte i = 0; i <31; i++) intBytes[i] = i;

            long writeOffset = 0;
            long readOffset = 0;

            int readCount = 2;
            intBytes[1] = 115;
            HDD.write(intBytes, writeOffset);
            HDD.read(ref intBytes, readOffset, readCount);



        }
    }


}


/*
31.03.2017
if you have a question write  kirakosyan0001@gmail.com
*/
/*
please help us
*/
