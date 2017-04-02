using System;
using System.IO;
using System.Threading;

namespace Hardware
{


	public delegate void WriteEndHandler();

	class HDD{
		public static readonly int THWB = 6;											//thread write bytes

		//file must be creat
		public static readonly string path = Directory.GetCurrentDirectory()+"/HDD.txt";//Linux path file //on windows +"\\HDD.txt"

		public static int count; 														//counts thread            'count = length/THWB'

		public static int cCTEndTs = 0;													//current count to end threads 

		private static FileStream fs;													//= File.Open (HDD.path, FileMode.Open,FileAccess.ReadWrite);

		private static readonly int OFFSET = 0;											//offset of data   //same for read and write

		public static event WriteEndHandler WriteHandler = WHand;

		private static void WHand(){
			Console.WriteLine ("End of write");
		}

		private class ToWrite
		{
			public ToWrite(byte[]data,int length){
				this.data = data;
				this.length = length;
			}

			public byte[] data;
			public int length;
		}

		public static void ThreadCallWrite(Object threadContext)
		{
			ToWrite threadValue = (ToWrite)threadContext;

			for (int i = 0; i < threadValue.length; i++)
				lock(fs) {
					fs.Seek (OFFSET, SeekOrigin.End);
					fs.WriteByte (threadValue.data [i]);
				};
			
			Console.WriteLine ("thread run");

			lock (fs) {
				cCTEndTs++;

				if (cCTEndTs == count) {
					cCTEndTs = 0;
					fs.Close ();
					WriteHandler();
				}
			}
		
		}

		public static long Write(byte[] data,int length =-1){
			
			while(fs!=null&&fs.CanWrite==true)
				Console.WriteLine ("Write wait");

			fs = File.Open (HDD.path, FileMode.Open, FileAccess.Write);
			long position =  fs.Length;

			if (length == -1)
				length = data.Length;

			int currentPosition = 0;

			if (length < THWB)
			count = 1;
			else count = length/THWB;


			while (length > 0) {
				Thread newThraed = new Thread (HDD.ThreadCallWrite);

				byte[] dataThread = new byte[length];

				if (length <= THWB) {

					for (int i = 0; i < length; i++) {
						dataThread [i] = data [i + currentPosition];
					}

					currentPosition += length;
					newThraed.Start (new ToWrite (dataThread, length));

					break;
				}

				for (int i = 0; i < THWB; i++) {
					dataThread [i] = data [i + currentPosition];
				}

				newThraed.Start(new ToWrite(dataThread,THWB));

				currentPosition +=THWB;

				length -= THWB;
			}

			return position;
		}


	}


	class MainClass
	{
		public static void Main (string[] args)
		{
			int intValue = 13;
			byte [] intValueOnByteArray =  BitConverter.GetBytes(intValue);
			HDD.Write (intValueOnByteArray);

			intValue = 14;
			intValueOnByteArray =  BitConverter.GetBytes(intValue);
			HDD.Write (intValueOnByteArray);

			byte[] stringOnBytes;
			;

			string s = "hesa ";
			long[] position = new long[s.Length];

			for (int i = 0; i < s.Length; i++) {
				stringOnBytes = BitConverter.GetBytes (s [i]);
				position [i] = HDD.Write (stringOnBytes);
			}

			for(int i = 0;i<position.Length;i++)
				Console.WriteLine ($"position[{i}] = {position[i]}");
			
		}
	}
}
