using Hardware;

partial class FileSystem
{
    class FSM
    {
        private static byte[] arrayBlock = new byte[SuperBlock.MaxBlocks];
        public static void SetArrayBlock(int index , byte value) {
            arrayBlock[index] = value;
            HardDisk.Write(new byte[1] { value }, SuperBlock.FSMStart + index);
        }

        public static byte GetArrayBlock(int index) {
            byte[] buffer = new byte[1];
            HardDisk.Read(ref buffer, SuperBlock.FSMStart + index);

            arrayBlock[index] = buffer[0];
            return arrayBlock[index];
        }

        public static int GetBlock()
        {
            //0 block is SuperBlock
            for (int i = 1; i < arrayBlock.Length; i++)
            {
                if (GetArrayBlock(i) == 0)
                {
                    SetArrayBlock(i, 1);
                    return i;
                }
            }
            //Get SuperBlock
            return 0;
        }

    }
}
