using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.IO;
//using System.Runtime.CompilerServices;
using System.Threading;

namespace Hardware
{
	class HDD{
		
		public static readonly int THWB = 1;//thread write bytes
		public static readonly string path = Directory.GetCurrentDirectory()+"/HDD.txt";
		private static FileStream fs;//= File.Open (HDD.path, FileMode.Open,FileAccess.ReadWrite);
		public static int count = 0;

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
					fs.Seek (0, SeekOrigin.End);
					fs.WriteByte (threadValue.data [i]);
					count--;
				};
			
			Console.WriteLine ("thread");
				
			if (count == 0) {

				Console.WriteLine ("the end write");

				fs.Close ();
				fs = null;
			}

		}

		public static void Write(byte[] data,int length =-1){
			while(fs!=null)
				Console.WriteLine ("nULLL");

			fs = File.Open (HDD.path, FileMode.Open, FileAccess.Write);

			if (length == -1)
				length = data.Length;

			int currentPosition = 0;

			while (length > 0) {
				Thread newThraed = new Thread (HDD.ThreadCallWrite);

				count++;

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

			Console.WriteLine ("End of Write");	
		}


	}


	class MainClass
	{
		public static void Main (string[] args)
		{
			//int intValue = 13;
			//byte [] intValueOnByteArray =  BitConverter.GetBytes(intValue);
	
			string s = "hesa ";
			for (int i = 0; i < s.Length; i++) {
				byte[] stringOnBytes = BitConverter.GetBytes (s [i]);
				HDD.Write (stringOnBytes);
			}
		}
	}
}
