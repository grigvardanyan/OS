using Hardware;

namespace FileSystem
{
    //Table ID free or not
    class InodeTable
    {
        private static byte[] arrayID = new byte[128];
        public static byte GetArrayID(int index)
        {
            byte[] buffer = new byte[1];
            HDD.Read(ref buffer, SuperBlock.InodeTableStart + index);
            while (!HDD.isNullReadHandler()) ;
            
            arrayID[index] = buffer[0];
            return arrayID[index];
        }
        public static void SetArrayID(int index, byte value)
        {
            arrayID[index] = value;
            HDD.Write(new byte[1] { value }, SuperBlock.InodeTableStart + index);
            while (!HDD.isNullWriteHandler()) ;
        }
        public static byte GetID()
        {

            for (byte i = 0; i < arrayID.Length; i++)
                if (GetArrayID(i) == 0)
                {
                    SetArrayID(i, 1);
                    SuperBlock.CurrentInodesCount = SuperBlock.CurrentInodesCount + 1;
                    return i;
                }
            return 0;
        }
    }
}
