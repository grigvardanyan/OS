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
        public static string path = Directory.GetCurrentDirectory()+"\\HDD.txt";

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

        //update
     [MethodImpl(MethodImplOptions.Synchronized)]
        public static void ThreadPoolCallbackWrite(Object threadContext)
        {
            Write threadValue = (Write)threadContext;
            
            fs.Seek(threadValue.offset, SeekOrigin.End);

            //fs.Write(threadValue.data, 0, threadValue.length);
            for (int i = 0; i < threadValue.length; i++) 
                fs.WriteByte(threadValue.data[i]);

            //Console.WriteLine("thread");
        }

        //update
        public static void write(byte[] data, long offset, int length = -1)
        {
            if (length == -1) length = data.Length;
            
            while (length / MAX_LENGTH_BYTES != 0)
            {
                byte[] dataThread = new byte[MAX_LENGTH_BYTES];
                Write state = new Write(data, offset, length);
                ThreadPool.QueueUserWorkItem(HDD.ThreadPoolCallbackWrite, (Object)state);
                
                length -= MAX_LENGTH_BYTES;
            }

            if (length != 0)
            {
                Write state = new Write(data, 0, length);
                ThreadPool.QueueUserWorkItem(HDD.ThreadPoolCallbackWrite, (Object)state);
            }
          
            Console.WriteLine("The End Write");
         
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
please help us
*/
