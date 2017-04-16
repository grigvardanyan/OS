using Hardware;
using System;

partial class FileSystem
{
    public class SuperBlock
    {
        public static void Init()
        {
			//for test
            blockSize = 100;
            maxBlocks = 32;
            blockStart = 0;
            inodeStart = 100 * 32;
            maxInodesCount = 23;
            inodeSize = 45;
            inodeDirStart = maxInodesCount * inodeSize + inodeStart;
            maxInodesDirCount = 12;
            inodeDirSize = 32;
            FSMstart = maxInodesCount * inodeDirSize + inodeDirStart;
            FSMsize = 256;
            inodeTableStart = FSMsize + FSMstart;
            inodeTableSize = 16;
            blockMaxCountInInode = 12;
            inodeMaxCountInDir = 2;
            dirMaxCountInDir = 1;

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

        private static int blockMaxCountInInode = -1; //SuperBlock.MaxInodesCount / SuperBlock.MaxBlocks
        public static int BlockMaxCountInInode
        {
            get
            {
                if (blockMaxCountInInode == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 64);

                    blockMaxCountInInode = BitConverter.ToInt32(buffer, 0);
                }
                return blockMaxCountInInode;
            }
        }

        private static int inodeMaxCountInDir = -1;//SuperBlock.MaxInodesCount / SuperBlock.MaxInodesDirCount
        public static int InodeMaxCountInDir
        {
            get
            {
                if (inodeMaxCountInDir == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 68);

                    inodeMaxCountInDir = BitConverter.ToInt32(buffer, 0);
                }
                return inodeMaxCountInDir;
            }
        }

        private static int dirMaxCountInDir = -1;
        public static int DirMaxCountInDir
        {
            get
            {
                if (dirMaxCountInDir == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 72);

                    dirMaxCountInDir = BitConverter.ToInt32(buffer, 0);
                }
                return dirMaxCountInDir;
            }
        }


        //--------------------------------------------------------add
        private static int inodeNameAndIDTableStart = -1;
        public static int InodeNameAndIDTableStart
        {
            get
            {
                if (inodeNameAndIDTableStart == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 76);

                    inodeNameAndIDTableStart = BitConverter.ToInt32(buffer, 0);
                }
                return inodeNameAndIDTableStart;
            }
        }

        private static int inodeDirNameAndIDTableStart = -1;
        public static int InodeDirNameAndIDTableStart
        {
            get
            {
                if (inodeDirNameAndIDTableStart == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 80);

                    inodeDirNameAndIDTableStart = BitConverter.ToInt32(buffer, 0);
                }
                return inodeDirNameAndIDTableStart;
            }
        }

        private static int inodeNameLength = -1;
        public static int InodeNameLength
        {
            get
            {
                if (inodeNameLength == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 84);

                    inodeNameLength = BitConverter.ToInt32(buffer, 0);
                }
                return inodeNameLength;
            }
        }

        private static int inodeDirNameLength = -1;
        public static int InodeDirNameLength
        {
            get
            {
                if (inodeDirNameLength == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 88);

                    inodeDirNameLength = BitConverter.ToInt32(buffer, 0);
                }
                return inodeDirNameLength;
            }
        }


        private static int inodeNameAndIDTableSize = -1;
        public static int InodeNameAndIDTableSize
        {
            get
            {
                if (inodeNameAndIDTableSize == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 92);

                    inodeNameAndIDTableSize = BitConverter.ToInt32(buffer, 0);
                }
                return inodeNameAndIDTableSize;
            }
        }

        private static int inodeDirNameAndIDTableSize = -1;
        public static int InodeDirNameAndIDTableSize
        {
            get
            {
                if (inodeDirNameAndIDTableSize == -1)
                {
                    byte[] buffer = new byte[4];
                    HardDisk.Read(ref buffer, 96);

                    inodeDirNameAndIDTableSize = BitConverter.ToInt32(buffer, 0);
                }
                return inodeDirNameAndIDTableSize;
            }
        }


    }



}
