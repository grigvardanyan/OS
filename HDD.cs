using System;
using System.IO;
using System.Threading;

namespace Hardware
{
    public delegate void WriteEndHandler();

    public delegate void ReadEndHandler();

    //HDD termination
    class HDD
    {
        public static readonly int THWB = 15;//thread write bytes

        public static readonly int THRB = 15;//thraed read bytes

        public static event WriteEndHandler WriteHandler = null;//= WHand;

        public static event ReadEndHandler ReadHandler = null;// = RHand;

        public static bool isNullWriteHandler() {

            if(WriteHandler == null)
                return true;
            
            return false;
        }

        public static bool isNullReadHandler()
        {

            if (ReadHandler == null)
                return true;

            return false;
        }

        public static void ThreadCallWrite(Object threadContext)
        {
            ToWrite threadValue = (ToWrite)threadContext;

            for (int i = 0; i < threadValue.length; i++)
                lock (fs)
                {
                    fs.Seek( threadValue.start + i, SeekOrigin.Begin);///////////nayell OFFSET urish  arjeqneri depqum
                    fs.WriteByte(threadValue.data[i]);                    
                };

            Console.WriteLine("write thread run");

            lock (fs)
            {
                cCTEndWTs++;

                if (cCTEndWTs == countWrite)
                {
                    cCTEndWTs = 0;
                    fs.Close();
                    if (WriteHandler != null)
                    {
                        WriteHandler();
                        WriteHandler = null;
                    }
                }
            }

        }

        public static void Write(byte[] data, int start, int length = -1)
        {

            while (fs != null)
            {
                if (fs.CanWrite == true || fs.CanRead == true)
                    Console.WriteLine("Write wait");
                else break;
            }

            WriteHandler = WHand;//NAYEL

            fs = File.Open(HDD.path, FileMode.Open, FileAccess.Write);

            if (length == -1)
                length = data.Length;

            int currentPosition = 0;

            if (length < THWB)
                countWrite = 1;
            else countWrite = length / THWB+1;


            while (length > 0)
            {
                Thread newThraed = new Thread(HDD.ThreadCallWrite);

              

                byte[] dataThread = new byte[length < THWB ? length : THWB];


                if (length <= THWB)
                {

                    for (int i = 0; i < length; i++)
                    {
                        dataThread[i] = data[i + currentPosition];
                    }

                    newThraed.Start(new ToWrite(dataThread, (currentPosition + start), length - 1));//NAYEL

                    currentPosition += length;

                    length -= THWB;

                    break;
                }

                for (int i = 0; i < THWB; i++)
                {
                    dataThread[i] = data[i + currentPosition];
                }

                newThraed.Start(new ToWrite(dataThread, currentPosition + start, THWB));

                currentPosition += THWB;

                length -= THWB;
            }

        }


        public static void ThreadCallRead( Object threadContext)
        {
            ToRead threadValue = (ToRead)threadContext;

            lock (fs)
            {
                fs.Seek( threadValue.offset , SeekOrigin.Begin);
                
                fs.Read(threadValue.data,threadValue.index, threadValue.count);
             };

            Console.WriteLine("raed thread run");

            lock (fs)
            {
                cCTEndRTs++;

                if (cCTEndRTs == countRead)
                {
                    cCTEndRTs = 0;
                    fs.Close();
                    if (ReadHandler != null)
                    {
                        ReadHandler();
                        ReadHandler = null;
                    }
                }
            }

        }

        public static void Read(ref byte[] data, int offset, int count = -1)
        {
            if (count == -1) count = data.Length;

            while (fs != null)
            {
                if (fs.CanWrite == true || fs.CanRead == true)
                    Console.WriteLine("Read wait");
                else break;
            }

            ReadHandler = RHand;//NAYEL

            fs = File.Open(path, FileMode.Open, FileAccess.Read);

            int currentPosition = 0;

            countRead = count < THRB ? 1 : count / THRB+1;

            Console.WriteLine("hesasdfhasjk   "+countRead);
            while (count > 0)
            {
                Thread newThraed = new Thread(HDD.ThreadCallRead);

                if (count <= THRB)
                {
                    newThraed.Start((Object)new ToRead(ref data, (currentPosition + offset), count - 1, currentPosition));

                    currentPosition += count;

                    count -= THWB;

                    break;
                }

                newThraed.Start((Object)new ToRead(ref data, currentPosition + offset, THRB, currentPosition));

                currentPosition += THRB;

                count -= THRB;
            }

        }





        //file must be creat
        public static readonly string path = Directory.GetCurrentDirectory() + "/HDD.txt";//Linux path file //on windows +"\\HDD.txt"

        private static int countWrite;//counts thread            'countWrite = length/THWB'

        private static int countRead; //counts thread            'countRead = length/TRWB'

        private static int cCTEndWTs = 0;//current count to end write threads 

        private static int cCTEndRTs = 0;//current count to end read threads 

        private static FileStream fs; //= File.Open (HDD.path, FileMode.Open,FileAccess.ReadWrite);

        private static readonly int OFFSET = 0; //offset of data   //same for read and write

        private static void RHand()
        {
            Console.WriteLine("End of read");
        }

        private static void WHand()
        {
            Console.WriteLine("End of write");
        }

        private class ToWrite
        {
            public ToWrite(byte[] data, int start, int length)
            {
                this.data = data;
                this.length = length;
                this.start = start;
            }
            public int start;
            public byte[] data;
            public int length;
        }

        private class ToRead
        {
            public ToRead(ref byte[] data, int offset, int count, int index)
            {
                this.index = index;
                this.offset = offset;
                this.count = count;
                this.data = data;
              
            }
            public byte[] data;
            public int index;
            public int offset;
            public int count;
        }



    }

    ///////////////// TEST ////////////////////
    class MainClass
    {
        public static void Main(string[] args)
        {
            int intValue = 82;
            byte[] intValueOnByteArray = BitConverter.GetBytes(intValue);

           // HDD.Write(intValueOnByteArray, 18);

            intValue = 19;
            //intValueOnByteArray = BitConverter.GetBytes(intValue);
            HDD.Write(intValueOnByteArray, 24);


            byte[] h = new byte[40];

            byte[] stringOnBytes;

            string s = "hes1a ";

            for (int i = 0; i < s.Length; i++)
            {
                stringOnBytes = BitConverter.GetBytes(s[i]);
                HDD.Write(stringOnBytes, i);
            }

            HDD.Read(ref h, 0,40);
           
            //for (int i = 0; i < 100000000; i++) ;

            while (!HDD.isNullReadHandler()) ;
            for (int i = 0; i < h.Length; i++)
            {
                Console.Write(i); Console.WriteLine( "  "+h[i]);
            }

            
        }
    }
}
