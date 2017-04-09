using System;
using Hardware;

namespace FileSystem
{
	class InodeDir
	{
		public InodeDir(byte ID = 255) {
			if (ID == 255)
			{
				dirID = InodeDirTable.GetDirID();
				//Console.WriteLine(dirID);
			}
			else dirID = ID;

		}

		private byte[] arrayRef = new byte[64];//0 - 51 inodeID  52- 63 inodeDirID
		public byte dirID;

		public byte GetArrayRef(int index)
		{

			byte[] buffer = new byte[1];
			HDD.Read(ref buffer, SuperBlock.InodeDirStart + dirID * SuperBlock.InodeDirSize + index);
			while (!HDD.isNullReadHandler()) ;
			arrayRef[index] = buffer[0];

			return arrayRef[index];
		}

		// index <= BlockCount
		public void SetArrayRef(int index, byte number) // index  if index > 52 numeber is dir else inode, number dir or inode
		{
			byte[] buffer = new byte[1];
			buffer[0] = number;
			HDD.Write(buffer, SuperBlock.InodeDirStart + dirID * SuperBlock.InodeDirSize + index);
			while (!HDD.isNullWriteHandler()) ;
			arrayRef[index] = number;
		}


	}


}

