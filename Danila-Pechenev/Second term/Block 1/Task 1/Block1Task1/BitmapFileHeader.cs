namespace Block1Task1
{
    /// <summary>
    /// Stores information about the file.
    /// </summary>
    public struct BitmapFileHeader
    {
        /// <summary>
        /// The size, in bytes, of the bitmap file.
        /// </summary>
        public int BfSize;

        /// <summary>
        /// The offset, in bytes, from the beginning of the BitmapFileHeader structure to the bitmap bits.
        /// </summary>
        public int BfOffBits;

        private short bfType;

        private short bfReserved1;

        private short bfReserved2;

        /// <summary>
        /// Reads BitmapFileHeader from the file stream.
        /// </summary>
        /// <param name="binaryReader">Used to read data from the file stream.</param>
        public void ReadBitmapFileHeader(BinaryReader binaryReader)
        {
            this.bfType = binaryReader.ReadInt16();
            this.BfSize = binaryReader.ReadInt32();
            this.bfReserved1 = binaryReader.ReadInt16();
            this.bfReserved2 = binaryReader.ReadInt16();
            this.BfOffBits = binaryReader.ReadInt32();
        }

        /// <summary>
        /// Writes BitmapFileHeader to the file stream.
        /// </summary>
        /// <param name="binaryWriter">Used to write data to the file stream.</param>
        public void WriteBitmapFileHeader(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(this.bfType);
            binaryWriter.Write(this.BfSize);
            binaryWriter.Write(this.bfReserved1);
            binaryWriter.Write(this.bfReserved2);
            binaryWriter.Write(this.BfOffBits);
        }
    }
}