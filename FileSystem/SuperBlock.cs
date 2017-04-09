using System;
using Hardware;
namespace FileSystem
{
    //starting on 0 position
    class SuperBlock
    {
        //blockSize
        private static int blockSize = -1;//4096
        public static int BlockSize
        {
            get
            {
                if (blockSize == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 0);
                    while (!HDD.isNullReadHandler()) ;

                    blockSize = BitConverter.ToInt32(buffer, 0);
                }
                return blockSize;
            }
        }

        //blockCountInInodes
        private static int blockCountInInodes = -1;//1024
        public static int BlockCountInInodes
        {
            get
            {
                if (blockCountInInodes == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 4);
                    while (!HDD.isNullReadHandler()) ;

                    blockCountInInodes = BitConverter.ToInt32(buffer, 0);
                }
                return blockCountInInodes;
            }
        }

        //maxBlocks
        private static int maxBlocks = -1;//1024*128
        public static int MaxBlocks
        {
            get
            {
                if (maxBlocks == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 8);
                    while (!HDD.isNullReadHandler()) ;

                    maxBlocks = BitConverter.ToInt32(buffer, 0);
                }
                return maxBlocks;
            }
        }

        //blockStart
        private static int blockStart = -1;//0
        public static int BlockStart
        {
            get
            {
                if (blockStart == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 12);
                    while (!HDD.isNullReadHandler()) ;

                    blockStart = BitConverter.ToInt32(buffer, 0);
                }
                return blockStart;
            }
        }

        //freeBlocksCount
        private static int freeBlocksCount = -1;//1024*128-1
        private static bool boolFreeBlockCount = false;
        public static int FreeBlocksCount
        {
            get
            {
                if (!boolFreeBlockCount)
                {

                    boolFreeBlockCount = true;
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 16);
                    while (!HDD.isNullReadHandler()) ;

                    freeBlocksCount = BitConverter.ToInt32(buffer, 0);
                }
                return freeBlocksCount;
            }

            set
            {
                boolFreeBlockCount = false;
                freeBlocksCount = value;
                byte[] buffer = new byte[4];
                buffer = BitConverter.GetBytes(freeBlocksCount);
                HDD.Write(buffer, 16);
                while (!HDD.isNullWriteHandler()) ;

            }
        }

        //inodeStart
        private static int inodeStart = -1;//4096*128*1024 
        public static int InodeStart
        {
            get
            {
                if (inodeStart == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 20);
                    while (!HDD.isNullReadHandler()) ;

                    inodeStart = BitConverter.ToInt32(buffer, 0);
                }
                return inodeStart;
            }
        }

        //maxInodesCount
        private static int maxInodesCount = -1;//128
        public static int MaxInodesCount
        {
            get
            {
                if (maxInodesCount == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 24);
                    while (!HDD.isNullReadHandler()) ;

                    maxInodesCount = BitConverter.ToInt32(buffer, 0);
                }
                return maxInodesCount;
            }
        }

        //currentInodesCount
        private static int currentInodesCount = -1;
        private static bool boolCurrentInodesCount = false;
        public static int CurrentInodesCount
        {
            get
            {
                if (!boolCurrentInodesCount)
                {
                    boolCurrentInodesCount = true;
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 28);
                    while (!HDD.isNullReadHandler()) ;

                    currentInodesCount = BitConverter.ToInt32(buffer, 0);
                }
                return currentInodesCount;
            }
            set
            {
                boolCurrentInodesCount = false;
                byte[] buffer = new byte[4];
                buffer = BitConverter.GetBytes(value);
                HDD.Write(buffer, 28);
                while (!HDD.isNullReadHandler()) ;
            }
        }

        //inodeSize
        private static int inodeSize = -1; //4102
        public static int InodeSize
        {
            get
            {
                if (inodeSize == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 32);
                    while (!HDD.isNullReadHandler()) ;

                    inodeSize = BitConverter.ToInt32(buffer, 0);
                }
                return inodeSize;
            }
        }

        //freeSpaceMgmtStart
        private static int freeSpaceMgmtStart = -1;//4102*128 + (4096*1024*128 )
        public static int FreeSpaceMgmtStart
        {
            get
            {
                if (freeSpaceMgmtStart == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 36);
                    while (!HDD.isNullReadHandler()) ;

                    freeSpaceMgmtStart = BitConverter.ToInt32(buffer, 0);
                }
                return freeSpaceMgmtStart;
            }
        }

        //freeSpaceMgmtSize
        private static int freeSpaceMgmtSize = -1;//1024*128
        public static int FreeSpaceMgmtSize
        {
            get
            {
                if (freeSpaceMgmtSize == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 40);
                    while (!HDD.isNullReadHandler()) ;

                    freeSpaceMgmtSize = BitConverter.ToInt32(buffer, 0);
                }
                return freeSpaceMgmtSize;
            }
        }

        //root
        private static int root = -1; // 1024*128  +(4102*128 + (4096*1024*128 ))
        public static int Root
        {
            get
            {
                if (root == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 44);
                    while (!HDD.isNullReadHandler()) ;

                    root = BitConverter.ToInt32(buffer, 0);
                }
                return root;
            }
        }

        private static int inodeTableStart = -1;// 4 + 1024*128  +(4102*128 + (4096*1024*128 )) 
        public static int InodeTableStart
        {
            get
            {
                if (inodeTableStart == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 48);
                    while (!HDD.isNullReadHandler()) ;

                    inodeTableStart = BitConverter.ToInt32(buffer, 0);
                }
                return inodeTableStart;
            }
        }


        private static int inodeTableSize = -1;//128 
        public static int InodeTableSize
        {
            get
            {
                if (inodeTableSize == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 52);
                    while (!HDD.isNullReadHandler()) ;

                    inodeTableSize = BitConverter.ToInt32(buffer, 0);
                }
                return inodeTableSize;
            }
        }

        private static int inodeDirStart = -1;// 128 + 4 + 1024*128  +(4102*128 + (4096*1024*128 ))
        public static int InodeDirStart
        {
            get
            {
                if (inodeDirStart == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 56);
                    while (!HDD.isNullReadHandler()) ;

                    inodeDirStart = BitConverter.ToInt32(buffer, 0);
                }
                return inodeDirStart;
            }
           
        }

        private static int inodeDirSize = -1;// 32*80 
        public static int InodeDirSize
        {
            get
            {
                if (inodeDirSize == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 60);
                    while (!HDD.isNullReadHandler()) ;

                    inodeDirSize = BitConverter.ToInt32(buffer, 0);
                }
                return inodeDirSize;
            }
        }

        private static int inodeDirTableStart = -1;// 32*80 + 128 + 4 + 1024*128  +(4102*128 + (4096*1024*128 ))
        public static int InodeDirTableStart
        {
            get
            {
                if (inodeDirTableStart == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 64);
                    while (!HDD.isNullReadHandler()) ;

                    inodeDirTableStart = BitConverter.ToInt32(buffer, 0);
                }
                return inodeDirTableStart;
            }

        }

        private static int inodeDirTableSize = -1;//32
        public static int InodeDirTableSize
        {
            get
            {
                if (inodeDirTableSize == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 68);
                    while (!HDD.isNullReadHandler()) ;

                    inodeDirTableSize = BitConverter.ToInt32(buffer, 0);
                }
                return inodeDirTableSize;
            }
        }

    }


}
