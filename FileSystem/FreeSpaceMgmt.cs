using Hardware;

namespace FileSystem
{
    class FreeSpaceMgmt
    {
        
        //arrayBlock
        private static byte[] arrayBlock = new byte[SuperBlock.MaxBlocks];//0 if free else 1 
        public static void SetArrayBlock(int index, byte value)
        {
            arrayBlock[index] = value;
            HDD.Write(new byte[1] { value }, SuperBlock.FreeSpaceMgmtStart + index);
            while (!HDD.isNullWriteHandler()) ;
        }

        public static byte GetArrayBlock(int index)
        {
            byte[] b = new byte[1];
            HDD.Read(ref b, SuperBlock.FreeSpaceMgmtStart + index);
            arrayBlock[index] = b[0];
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
					SuperBlock.FreeBlocksCount = SuperBlock.FreeBlocksCount - 1;
                    return i;
                }
            }

            //Get SuperBlock
            return 0;
        }

    }
}
