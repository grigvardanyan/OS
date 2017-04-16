/////////////////-------------------------------add



partial class FileSystem
{
    class Translate
    {
        //ToXID

        public static int ToBlockID(int referencBlock)
        {
            return (referencBlock - SuperBlock.BlockStart) / SuperBlock.BlockSize;
        }

        public static int ToInodeID(int referencInode)
        {
            return (referencInode - SuperBlock.InodeStart) / SuperBlock.InodeSize;
        }

        public static int ToInodeDirID(int referencInodeDir)
        {
            return (referencInodeDir - SuperBlock.InodeDirStart) / SuperBlock.InodeDirSize;
        }

        // ToXRef

        public static int ToBlockRef(int blockID)
        {
            return blockID * SuperBlock.BlockSize + SuperBlock.BlockStart;
        }

        public static int ToInodeRef(int inodeID)
        {
            return inodeID * SuperBlock.InodeSize + SuperBlock.InodeStart;
        }

        public static int ToInodeDirRef(int inodeDirID)
        {
            return inodeDirID * SuperBlock.InodeDirSize + SuperBlock.InodeDirStart;
        }

    }
}

