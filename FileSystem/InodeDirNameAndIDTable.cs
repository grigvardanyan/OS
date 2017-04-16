/////////////////-------------------------------add



using Hardware;

partial class FileSystem
{
    class InodeDirNameAndIDTable
    {
        private static string[] nameDirInode = new string[SuperBlock.MaxInodesDirCount];
        public static int GetInodeDirID(string name)
        {
            for (int i = 0; i < nameDirInode.Length; i++)
            {
                byte[] buffer = new byte[SuperBlock.InodeDirNameLength];
                HardDisk.Read(ref buffer, SuperBlock.InodeDirNameAndIDTableStart + i * SuperBlock.InodeDirNameLength);

                nameDirInode[i] = System.Text.Encoding.ASCII.GetString(buffer);

                if (name == nameDirInode[i]) return i;
            }
            return -1; // or return 0;
        }

        public static void SetInodeDirID(string name, int ID)
        {
            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(name);
            HardDisk.Write(buffer, SuperBlock.InodeDirNameAndIDTableStart + ID * SuperBlock.InodeDirNameLength);

            nameDirInode[ID] = name;
        }
    }
}
