//HardDisk without threads
//this  code for testing 
//in HDD.cs we must be do Join Threads
//for that this code

using System.IO;

namespace Hardware
{
    class HardDisk
    {
        private static FileStream fs = File.Open(Directory.GetCurrentDirectory() + "/hdd.txt", FileMode.Open, FileAccess.ReadWrite);
        public static void Write(byte[] data, int offset, int size = -1)
        {
            if (size == -1) size = data.Length;
            for (int i = 0; i < size; i++)
                lock (fs)
                {
                    fs.Seek(i + offset, SeekOrigin.Begin);
                    fs.WriteByte(data[i]);
                };

        }

        public static void Read(ref byte[] data, int offset, int size = -1)
        {
            if (size == -1) size = data.Length;
            lock (fs)
            {
                fs.Seek(offset, SeekOrigin.Begin);
                fs.Read(data, 0, size);
            };
        }

        public static void Close()
        {
            fs.Close();
        }

    }
}
