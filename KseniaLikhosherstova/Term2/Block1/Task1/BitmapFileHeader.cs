namespace Task1
{
    public class BitmapFileHeader
    {
        public byte TypeFile = 0;
        public byte TypeSize = 0;
        public uint SizeFile = 0;
        public ushort ReservedOne = 1;
        public ushort ReservedTwo = 1;
        public uint OffsetBits = 0;
    }
}
