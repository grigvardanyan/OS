using System;
using Hardware;
using FileSystem;

namespace Example
{
    class MainClass
    {
        public static void Main(string[] args)
        {

			Console.WriteLine ("LOL");

            InodeDir i = new InodeDir(31);

            Inode g = new Inode(i.GetArrayRef(0));
            
			int ff = g.GetArrayBlock (0);

			byte[] data = new byte["ayoooo oui".Length] ;

			HDD.Read (ref data, ff * SuperBlock.BlockSize + SuperBlock.BlockStart);

			while (!HDD.isNullReadHandler ())
				;
			string s = System.Text.Encoding.ASCII.GetString (data);
			Console.WriteLine (s);

			//HDD.Write (data, ff * SuperBlock.BlockSize + SuperBlock.BlockStart);
			//while (!HDD.isNullWriteHandler ())
				;

        }
    }
}


////-------------------instal
//            byte[] buffer = BitConverter.GetBytes(4096);//blockSize
//            HDD.Write(buffer, 0);
//            while (!HDD.isNullWriteHandler()) ;

//            buffer = BitConverter.GetBytes(1024);//blockCountInInodes
//            HDD.Write(buffer, 4);
//            while (!HDD.isNullWriteHandler()) ;

//            buffer = BitConverter.GetBytes(1024 * 128);//maxBlocks
//            HDD.Write(buffer, 8);
//            while (!HDD.isNullWriteHandler()) ;

//            buffer = BitConverter.GetBytes(0);//blockStart
//            HDD.Write(buffer, 12);
//            while (!HDD.isNullWriteHandler()) ;

//            buffer = BitConverter.GetBytes(1024 * 128 - 1);//freeBlocksCount
//            HDD.Write(buffer, 16);
//            while (!HDD.isNullWriteHandler()) ;

//            buffer = BitConverter.GetBytes(4096 * 128 * 1024);//inodeStart
//            HDD.Write(buffer, 20);
//            while (!HDD.isNullWriteHandler()) ;

//            buffer = BitConverter.GetBytes(128);//maxInodesCount
//            HDD.Write(buffer, 24);
//            while (!HDD.isNullWriteHandler()) ;

//            buffer = BitConverter.GetBytes(0);//currentInodesCount
//            HDD.Write(buffer, 28);
//            while (!HDD.isNullWriteHandler()) ;

//            buffer = BitConverter.GetBytes(4102);//inodeSize
//            HDD.Write(buffer, 32);
//            while (!HDD.isNullWriteHandler()) ;

//            buffer = BitConverter.GetBytes(4102 * 128 + (4096 * 1024 * 128));//freeSpaceMgmtStart
//            HDD.Write(buffer, 36);
//            while (!HDD.isNullWriteHandler()) ;

//            buffer = BitConverter.GetBytes(1024 * 128);//freeSpaceMgmtSize
//            HDD.Write(buffer, 40);
//            while (!HDD.isNullWriteHandler()) ;

//            buffer = BitConverter.GetBytes(1024 * 128 + (4102 * 128 + (4096 * 1024 * 128)));//root
//            HDD.Write(buffer, 44);
//            while (!HDD.isNullWriteHandler()) ;

//            buffer = BitConverter.GetBytes(4 + 1024 * 128 + (4102 * 128 + (4096 * 1024 * 128)));//inodeTableStart
//            HDD.Write(buffer, 48);
//            while (!HDD.isNullWriteHandler()) ;

//            buffer = BitConverter.GetBytes(128 );//inodeTableSize
//            HDD.Write(buffer, 52);
//            while (!HDD.isNullWriteHandler()) ;

//            buffer = BitConverter.GetBytes(128 + 4 + 1024*128  +(4102*128 + (4096*1024*128 )) );//inodeDirStart
//            HDD.Write(buffer, 56);
//            while (!HDD.isNullWriteHandler()) ;

//            buffer = BitConverter.GetBytes(32*80 );//inodeDirSize
//            HDD.Write(buffer, 60);
//            while (!HDD.isNullWriteHandler()) ;

//            buffer = BitConverter.GetBytes( 32*80 + 128 + 4 + 1024*128  +(4102*128 + (4096*1024*128 )));//inodeDirTableStart
//            HDD.Write(buffer, 64);
//            while (!HDD.isNullWriteHandler()) ;

//            buffer = BitConverter.GetBytes(32 );//inodeDirTableSize
//            HDD.Write(buffer, 68);
//            while (!HDD.isNullWriteHandler()) ;


//--------------------test
//Clock.SetTimer();
//int intValue = 82;
//byte[] intValueOnByteArray = BitConverter.GetBytes(intValue);

//HDD.Write(intValueOnByteArray, 25);

//intValue = 19;
////intValueOnByteArray = BitConverter.GetBytes(intValue);
////HDD.Write(intValueOnByteArray, 24);


//byte[] h = new byte[40];

//byte[] stringOnBytes;

//string s = "hes1a ";

//for (int i = 0; i < s.Length; i++)
//{
//    stringOnBytes = BitConverter.GetBytes(s[i]);
//    HDD.Write(stringOnBytes, i);
//}

//HDD.Read(ref h, 0, 40);



//while (!HDD.isNullReadHandler()) ;
//for (int i = 0; i < h.Length; i++)
//{
//    Console.Write(i); Console.WriteLine("  " + h[i]);
//}
