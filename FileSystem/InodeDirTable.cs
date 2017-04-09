using System;
using Hardware;

namespace FileSystem
{
	class InodeDirTable
	{
		private static byte[] arrayDirID = new byte[32]; // 32 hat direvtoria
		public static byte GetArrayDirID(int index)
		{
			byte[] buffer = new byte[1];
			HDD.Read(ref buffer,SuperBlock.InodeDirTableStart+index);
			while (!HDD.isNullReadHandler()) ;

			arrayDirID[index] = buffer[0];
			return arrayDirID[index];
		}
		public static void SetArrayDirID(int index, byte value)
		{
			arrayDirID[index] = value;
			HDD.Write(new byte[1] { value }, SuperBlock.InodeDirTableStart+index);
			while (!HDD.isNullWriteHandler()) ;
		}
		public static byte GetDirID()
		{

			for (byte i = 0; i < arrayDirID.Length; i++)
				if (GetArrayDirID(i) == 0)
				{
					SetArrayDirID(i, 1);
					return i;
				}

			return 0;
		}

	}
}

