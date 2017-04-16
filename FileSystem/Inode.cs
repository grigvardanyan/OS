using Hardware;
using System;

partial class FileSystem
{
    public class Inode
    {
        private int[] arrayBlocks = new int[SuperBlock.BlockMaxCountInInode];

        private byte fileID;

        public Inode(byte ID = 255)
        {
            if (ID == 255)
            {
                fileID = (byte)InodeTable.GetInode();
          
            }
            else fileID = ID;
        }

        public int GetArrayBlock(int index)
        {

            byte[] buffer = new byte[4];
            HardDisk.Read(ref buffer, SuperBlock.InodeStart + fileID * SuperBlock.InodeSize + 4 * index);
            arrayBlocks[index] = BitConverter.ToInt32(buffer, 0);

            return arrayBlocks[index];
        }

       
        public void SetArrayBlock(int index, int number) 
        {
            byte[] buffer = new byte[4];
            buffer = BitConverter.GetBytes(number*SuperBlock.BlockSize+SuperBlock.BlockStart);

            HardDisk.Write(buffer, SuperBlock.InodeStart + fileID * SuperBlock.InodeSize + 4 * index);

            arrayBlocks[index] = number * SuperBlock.BlockSize + SuperBlock.BlockStart;
        }
    }
}
