using System;

namespace EasyOS
{
    class Test
    {
        static void Main(string[] args)
        {
            Hardware.Clock.SetTimer();
            Console.ReadLine();
            FileSystem.FileDescriptor g = new FileSystem.FileDescriptor(1,FileSystem.FileAccess.Read);
            Console.WriteLine(g.fileAccess);
        }
    }
}
