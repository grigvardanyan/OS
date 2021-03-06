﻿using System;

partial class FileSystem
{
    //Read = binary(01) , Write = binary(10) , ReadWrite = binary(11) , Craet = binary(100)
    [Flags]
    public enum FileAccess { Read = 1, Write = 2, ReadWrite = Read|Write };
    
    public class FileDescriptor
    {
        public FileDescriptor(int fileID,FileAccess fileAccess) {
            this.fileID = fileID;
            this.fileAccess = fileAccess;

            countDescriptors++;                                                 //add
            tableDescriptors[countDescriptors] = fileID;                        //add
        }
        public FileAccess fileAccess;
        public int fileID;
    }

    public static int countDescriptors = -1;

    public static int[] tableDescriptors ;
}
