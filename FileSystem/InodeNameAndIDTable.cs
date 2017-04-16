/////////////////-------------------------------add


using Hardware;

partial class FileSystem
{
    class InodeNameAndIDTable
    {
        private static string[] nameInode = new string[SuperBlock.MaxInodesCount];
        public static int GetInodeID(string name)
        {
            for (int i = 0; i < nameInode.Length; i++)
            {
                byte[] buffer = new byte[SuperBlock.InodeNameLength];
                HardDisk.Read(ref buffer, SuperBlock.InodeNameAndIDTableStart + i * SuperBlock.InodeNameLength);

                nameInode[i] = System.Text.Encoding.ASCII.GetString(buffer);

                if (name == nameInode[i]) return i;
            }
            return -1; // or return 0;
        }

        public static void SetInodeID(string name, int ID)
        {
            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(name);
            HardDisk.Write(buffer, SuperBlock.InodeNameAndIDTableStart + ID * SuperBlock.InodeNameLength);

            nameInode[ID] = name;
        }

    }
}
