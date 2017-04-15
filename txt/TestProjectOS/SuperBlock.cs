using Hardware;
using System;

partial class FileSystem
{
    class SuperBlock
    {
        public static void init() {
          

        }
        private static int blockSize = -1;  //1024
        public static int BlockSize
        {
            get
            {
                if (blockSize == -1)
                {
                    byte[] buffer = new byte[4];

                    HardDisk.Read(ref buffer, 0);
                    blockSize = BitConverter.ToInt32(buffer, 0);
                }
                return blockSize;
            }
        }

        private static int maxBlocks = -1; //32
        public static int MaxBlocks
        {
            get
            {
                if (maxBlocks == -1)
                {
                    byte[] buffer = new byte[4];

                    HardDisk.Read(ref buffer, 4);
                    maxBlocks = BitConverter.ToInt32(buffer, 0);
                }
                return maxBlocks;
            }
        }

        private static int blockStart = -1;//0
        public static int BlockStart
        {
            get
            {
                if (blockStart == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 8);
                    blockStart = BitConverter.ToInt32(buffer, 0);
                }
                return blockStart;
            }
        }

        private static int inodeStart = -1;// 32*1024+0
        public static int InodeStart
        {
            get
            {
                if (inodeStart == -1)
                {
                    byte[] buffer = new byte[4];

                    HardDisk.Read(ref buffer, 12);
                    inodeStart = BitConverter.ToInt32(buffer, 0);
                }

                return inodeStart;
            }
        }

        private static int maxInodesCount = -1;//16
        public static int MaxInodesCount
        {
            get
            {
                if (maxInodesCount == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 16);

                    maxInodesCount = BitConverter.ToInt32(buffer, 0);
                }
                return maxInodesCount;
            }
        }

        private static int inodeSize = -1;/// //////////////////////////////////////////////////
        public static int InodeSize
        {
            get
            {
                if (inodeSize == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 20);
                    inodeSize = BitConverter.ToInt32(buffer, 0);
                }
                return inodeSize;
            }
        }

        private static int inodeDirStart = -1;// 
        public static int InodeDirStart
        {
            get
            {
                if (inodeDirStart == -1)
                {
                    byte[] buffer = new byte[4];

                    HardDisk.Read(ref buffer, 24);
                    inodeDirStart = BitConverter.ToInt32(buffer, 0);
                }

                return inodeDirStart;
            }
        }

        private static int maxInodesDirCount = -1;//128
        public static int MaxInodesDirCount
        {
            get
            {
                if (maxInodesDirCount == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 28);

                    maxInodesDirCount = BitConverter.ToInt32(buffer, 0);
                }
                return maxInodesDirCount;
            }
        }

        private static int inodeDirSize = -1;//
        public static int InodeDirSize
        {
            get
            {
                if (inodeDirSize == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 32);
                    inodeDirSize = BitConverter.ToInt32(buffer, 0);
                }
                return inodeDirSize;
            }
        }

        private static int FSMstart = -1;
        public static int FSMStart
        {
            get
            {
                if (FSMstart == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 36);
                    FSMstart = BitConverter.ToInt32(buffer, 0);
                }
                return FSMstart;
            }
        }

        private static int FSMsize = -1;
        public static int FSMSize
        {
            get
            {
                if (FSMsize == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 40);
                    FSMsize = BitConverter.ToInt32(buffer, 0);
                }
                return FSMsize;
            }
        }

        private static int root = -1;
        public static int Root
        {
            get
            {
                if (root == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 44);
                    
                    root = BitConverter.ToInt32(buffer, 0);
                }
                return root;
            }
        }

        private static int inodeTableStart = -1;
        public static int InodeTableStart
        {
            get
            {
                if (inodeTableStart == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 48);
                
                    inodeTableStart = BitConverter.ToInt32(buffer, 0);
                }
                return inodeTableStart;
            }
        }

        private static int inodeTableSize = -1;
        public static int InodeTableSize
        {
            get
            {
                if (inodeTableSize == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 52);
                   
                    inodeTableSize = BitConverter.ToInt32(buffer, 0);
                }
                return inodeTableSize;
            }
        }

        private static int inodeDirTableStart = -1;
        public static int InodeDirTableStart
        {
            get
            {
                if (inodeDirTableStart == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 56);
                    
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
                    HardDisk.Read(ref buffer, 60);
                  
                    inodeDirTableSize = BitConverter.ToInt32(buffer, 0);
                }
                return inodeDirTableSize;
            }
        }

    }



}
