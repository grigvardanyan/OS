using Hardware;

partial class FileSystem
{
    class InodeTable
    {
        private static byte[] arrayInodeTable = new byte[SuperBlock.InodeTableStart];
        public static void SetArrayInodeTable(int index, byte value) {
            arrayInodeTable[index] = value;
            HardDisk.Write(new byte[1] { value }, SuperBlock.InodeTableStart + index);
        }
        public static byte GetArrayInodeTable(int index) {
            byte[] buffer = new byte[1];
            HardDisk.Read(ref buffer, SuperBlock.InodeTableStart + index);
            arrayInodeTable[index] = buffer[0];
            return arrayInodeTable[index];
        }

        public static int GetInode()
        {

            for (int i = 0; i<arrayInodeTable.Length; i++)
                if (GetArrayInodeTable(i) == 0)
                {
                    SetArrayInodeTable(i, 1);
                    return i;
                }
            return 0;
        }
    }
}
