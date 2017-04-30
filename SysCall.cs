using static Kernel.FileSystem;
using Kernel;


class SysCall
{
    private static readonly int MAX_OPEN_FILES = 20;
    private static FileDescriptor[] table = new FileDescriptor[MAX_OPEN_FILES];
    private static int currentOpenFiles = -1;
    private static string[] GetArrayOfString(string path)
    {
        int countSlash = 0;
        for (int i = 0; i < path.Length; i++) if (path[i] == '\\') countSlash++;
        string[] result = new string[countSlash];

        int count = 0;
        int j = 0;
        char[] buffer;
        for (int i = 1; i < path.Length; i++)
        {
            if (path[i] == '\\')
            {
                buffer = new char[count + 1];
                for (int h = i - count, g = 0; h < i; h++, g++)
                {
                    buffer[g] = path[h];
                }

                result[j] = new string(buffer);
                j++;
                count = 0;
            }
            else
                count++;

        }

        buffer = new char[count + 1];
        for (int h = path.Length - count, g = 0; h < path.Length; h++, g++)
        {
            buffer[g] = path[h];
        }

        result[j] = new string(buffer);

        return result;
    }

    private static int IsFound(string[] stringDir)
    {
        string fileName = stringDir[stringDir.Length - 1];

        InodeDir dir;
        bool flag;

        for (int i = 0; i < stringDir.Length - 2; i++)
        {
            dir = new InodeDir((byte)InodeDirNameAndIDTable.GetInodeDirID(stringDir[i]));
            flag = false;
            for (int j = 0; j < SuperBlock.DirMaxCountInDir; j++)
            {
                if (Translate.ToInodeDirID(dir.GetArrayDir(j)) == InodeDirNameAndIDTable.GetInodeDirID(stringDir[i + 1]))
                {
                    flag = true;
                    break;
                }
            }

            if (!flag) return 0;
        }

        dir = new InodeDir((byte)InodeDirNameAndIDTable.GetInodeDirID(stringDir[stringDir.Length - 1]));

        for (int i = 0; i < SuperBlock.InodeMaxCountInDir; i++)
        {

            if (Translate.ToInodeID(dir.GetArrayFile(i)) == InodeNameAndIDTable.GetInodeID(fileName))
            {
                return 1;
            };
        }

        return -1;
    }

    public static int Open(string path, FileSystem.FileAccess access)
    {
        string[] stringDir = GetArrayOfString(path);
        string fileName = stringDir[stringDir.Length - 1];
        if (IsFound(stringDir) == 0 || IsFound(stringDir) == -1) { /*not found*/ }
        //FileDescriptor fd = new FileDescriptor(InodeNameAndIDTable.GetInodeID(fileName), access);

        //must return currentFileDescriptors
        //and that numberov descriptors add the table currentFileDescriptor And fileId 

        table[++currentOpenFiles] = new FileDescriptor(InodeNameAndIDTable.GetInodeID(fileName), access);
        return currentOpenFiles;
    }

    public static void Creat(string path)
    {
        string[] stringDir = GetArrayOfString(path);
        if (IsFound(stringDir) != -1) { /*not found directory or file already craet*/}

        string fileName = stringDir[stringDir.Length - 1];
        string dirName = stringDir[stringDir.Length - 2];
        Inode newInode = new Inode();
        InodeNameAndIDTable.SetInodeID(fileName, newInode.fileID);
        int dirID = InodeDirNameAndIDTable.GetInodeDirID(dirName);
        InodeDir dir = new InodeDir((byte)dirID);
        //int index = 0;
        //dir.SetArrayFile(index, newInode.fileID);
        //must in deicertory add inode ID 
    }

    public static void Write(int file, byte[] data)
    {
        FileDescriptor f = table[file];
        if (f.fileAccess != FileAccess.Write || f.fileAccess != FileAccess.ReadWrite) { /*not access*/}

        Inode inode = new Inode((byte)f.fileID);
       
        //if (positionBegin == positionEnd) Hardware.HardDisk.Write(data, inode.GetArrayBlock(positionBegin)+f.offset);
        int startBlock = f.offset / SuperBlock.BlockSize;
        int startPosition = f.offset % SuperBlock.BlockSize;
        //write   
        Hardware.HardDisk.Write(data, inode.GetArrayBlock(startBlock)+startPosition,SuperBlock.BlockSize - startPosition);

        int endBlock = (f.offset + data.Length) / SuperBlock.BlockSize;
        int currentPositionInData = SuperBlock.BlockSize - startPosition;
        byte[] dataX = new byte[SuperBlock.BlockSize];
        for (int i = startBlock + 1; i < endBlock; i++)
        {

            for (int j = 0; j < SuperBlock.BlockSize; j++)
            {
                dataX[j] = data[currentPositionInData + j];
            }

            Hardware.HardDisk.Write(dataX, inode.GetArrayBlock(i), SuperBlock.BlockSize);
            currentPositionInData += SuperBlock.BlockSize;
        }
        for (int i = currentPositionInData, j = 0; i < data.Length; i++, j++)
        {
            dataX[j] = data[i];
            if (i == data.Length - 1) Hardware.HardDisk.Write(dataX, inode.GetArrayBlock(endBlock), data.Length - currentPositionInData);
        }

        f.offset += data.Length;
    }

    //read some method only Reading in Blocks
    public static void Read(int file, ref byte[] data) {
        FileDescriptor f = table[file];
        if (f.fileAccess != FileAccess.Read || f.fileAccess != FileAccess.ReadWrite) { /*not access*/}

        Inode inode = new Inode((byte)f.fileID);
        
        //if (positionBegin == positionEnd) Hardware.HardDisk.Read(ref data, inode.GetArrayBlock(positionBegin)+f.offset);
        int startBlock = f.offset / SuperBlock.BlockSize;
        int startPosition = f.offset % SuperBlock.BlockSize;
        //Read   
        Hardware.HardDisk.Read(ref data, inode.GetArrayBlock(startBlock)+startPosition,SuperBlock.BlockSize - startPosition);

        int endBlock = (f.offset + data.Length) / SuperBlock.BlockSize;
        int currentPositionInData = SuperBlock.BlockSize - startPosition;
        byte[] dataX = new byte[SuperBlock.BlockSize];
        for (int i = startBlock + 1; i < endBlock; i++)
        {
            Hardware.HardDisk.Read(ref dataX, inode.GetArrayBlock(i), SuperBlock.BlockSize);
            for (int j = 0; j < SuperBlock.BlockSize; j++)
            {
                data[currentPositionInData + j] = dataX[j];
            }
            currentPositionInData += SuperBlock.BlockSize;
        }
        for (int i = currentPositionInData, j = 0; i < data.Length; i++, j++)
        {
            if (i == data.Length - 1)
            {
                Hardware.HardDisk.Read(ref dataX, inode.GetArrayBlock(endBlock), data.Length - currentPositionInData);
                for (int k = 0; k < data.Length - currentPositionInData; k++) data[currentPositionInData + k] = dataX[k];
            }
        }

        f.offset += data.Length;
    }
}

