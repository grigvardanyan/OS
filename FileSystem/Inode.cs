using System;
using Hardware;

namespace FileSystem
{
    
    class Inode
    {
        //ID > 128 error 
        public Inode(byte ID = 255)
        {
            if (ID == 255)
            {
                fileID = InodeTable.GetID();
                 Console.WriteLine(fileID);
            }
            else fileID = ID;
        }

        private int[] arrayBlocks = new int[1024];//block numbers
        public int GetArrayBlock(int index)
        {

            byte[] buffer = new byte[4];
            HDD.Read(ref buffer, SuperBlock.InodeStart + fileID * SuperBlock.InodeSize + 4 * index);
            arrayBlocks[index] = BitConverter.ToInt32(buffer, 0);

            return arrayBlocks[index];
        }

        // index <= BlockCount
        public void SetArrayBlock(int index, int number = -1) // index , number block
        {
            byte[] buffer = new byte[4];
            

			buffer = BitConverter.GetBytes(number);
            HDD.Write(buffer, SuperBlock.InodeStart + fileID * SuperBlock.InodeSize + index);

			if (number == -1) {
				FreeSpaceMgmt.SetArrayBlock (arrayBlocks [index], 0);
				blockCount = blockCount - 1;
				arrayBlocks[index] = 0;

			} else {
				blockCount = blockCount + 1;
				FreeSpaceMgmt.SetArrayBlock (number, 1);//update
				arrayBlocks[index] = number;
			}            
        }

   		private byte fileID;

        private int blockCount;//= -1;
        private bool boolBlockCount = false;
        public int BlockCount
        {
            get
            {
                if (!boolBlockCount)
                {
                    boolBlockCount = true;
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, SuperBlock.InodeStart + fileID * SuperBlock.InodeSize + 4096);
                    while (!HDD.isNullReadHandler()) ;

                    blockCount = BitConverter.ToInt32(buffer, 0);
                }
                return blockCount;
            }
            set
            {
                boolBlockCount = false;
                byte[] buffer = new byte[4];
                buffer = BitConverter.GetBytes(value);
                HDD.Write(buffer, SuperBlock.InodeStart + fileID * SuperBlock.InodeSize + 4096);
                while (!HDD.isNullWriteHandler()) ;

                blockCount = value;
            }
        }

        private int modifyTime;
        private bool boolModifyTime = false;
        public int ModifyTime
        {
            get
            {
                if (!boolModifyTime)
                {
                    boolModifyTime = true;
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, SuperBlock.InodeStart + fileID * SuperBlock.InodeSize + 4096 + 4);
                    while (!HDD.isNullReadHandler()) ;
                    modifyTime = BitConverter.ToInt32(buffer, 0);
                }
                return modifyTime;
            }
            set
            {
                boolModifyTime = false;
                byte[] buffer = new byte[4];
                HDD.Write(buffer, SuperBlock.InodeStart + fileID * SuperBlock.InodeSize + 4096 + 4);
                while (!HDD.isNullWriteHandler()) ;
                modifyTime = BitConverter.ToInt32(buffer, 0);
            }
        }

        private int accessTime;
        private bool boolAccessTime = false;
        public int AccessTime
        {

            get
            {
                if (!boolAccessTime)
                {
                    boolAccessTime = true;
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, SuperBlock.InodeStart + fileID * SuperBlock.InodeSize + 4096 + 4 + 4);
                    while (!HDD.isNullReadHandler()) ;
                    accessTime = BitConverter.ToInt32(buffer, 0);
                }
				return accessTime;
            }
            set
            {
                boolAccessTime = false;
                byte[] buffer = new byte[4];
                HDD.Write(buffer, SuperBlock.InodeStart + fileID * SuperBlock.InodeSize + 4096 + 4 + 4);
                while (!HDD.isNullWriteHandler()) ;
                accessTime = BitConverter.ToInt32(buffer, 0);
            }
        }
    }
}
