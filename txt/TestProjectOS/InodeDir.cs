using Hardware;
using System;

partial class FileSystem
{
    class InodeDir
    {
        private byte dirID;

        public InodeDir(byte ID = 255)
        {
            if (ID == 255)
            {
                dirID = (byte)InodeDirTable.GetInodeDir();
            }
            else dirID = ID;
        }

        public int[] arrayFile = new int[SuperBlock.MaxInodesCount / SuperBlock.MaxInodesDirCount];


        public int GetArrayFile(int index)
        {

            byte[] buffer = new byte[4];
            HardDisk.Read(ref buffer, SuperBlock.InodeDirStart + dirID * SuperBlock.InodeDirSize + 4 * index);

            arrayFile[index] = BitConverter.ToInt32(buffer, 0);

            return arrayFile[index];
        }


        public void SetArrayFile(int index, byte number)
        {
            byte[] buffer = new byte[4];
            buffer = BitConverter.GetBytes( SuperBlock.InodeStart + SuperBlock.InodeSize * number);
            HardDisk.Write(buffer, SuperBlock.InodeDirStart + dirID * SuperBlock.InodeDirSize + 4 * index);

            arrayFile[index] = SuperBlock.InodeStart + SuperBlock.InodeSize * number;
        }
    }
}
