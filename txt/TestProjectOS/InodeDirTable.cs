using Hardware;

partial class FileSystem
{
    class InodeDirTable
    {
        private static byte[] arrayInodeDirTable = new byte[SuperBlock.InodeDirTableStart];
        public static void SetArrayInodeDirTable(int index, byte value)
        {
            arrayInodeDirTable[index] = value;
            HardDisk.Write(new byte[1] { value }, SuperBlock.InodeDirTableStart + index);
        }
        public static byte GetArrayInodeDirTable(int index)
        {
            byte[] buffer = new byte[1];
            HardDisk.Read(ref buffer, SuperBlock.InodeDirTableStart + index);
            arrayInodeDirTable[index] = buffer[0];
            return arrayInodeDirTable[index];
        }

        public static int GetInodeDir()
        {

            for (int i = 0; i < arrayInodeDirTable.Length; i++)
                if (GetArrayInodeDirTable(i) == 0)
                {
                    SetArrayInodeDirTable(i, 1);
                    return i;
                }
            return 0;
        }
    }
}
