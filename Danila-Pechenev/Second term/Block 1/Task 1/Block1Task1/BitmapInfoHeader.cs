namespace Block1Task1
{
    /// <summary>
    /// Stores information about the image.
    /// </summary>
    public struct BitmapInfoHeader
    {
        /// <summary>
        /// Specifies the width of the bitmap, in pixels.
        /// </summary>
        public int BiWidth;

        /// <summary>
        /// Specifies the height of the bitmap, in pixels.
        /// </summary>
        public int BiHeight;

        /// <summary>
        /// Specifies the number of bits per pixel (bpp).
        /// </summary>
        public short BiBitCount;

        /// <summary>
        /// Specifies the size, in bytes, of the image.
        /// </summary>
        public int BiSizeImage;

        /// <summary>
        /// Specifies the number of color indices in the color table that are actually used by the bitmap.
        /// </summary>
        public int BiClrUsed;

        /// <summary>
        /// Specifies the number of color indices that are considered important for displaying the bitmap.
        /// </summary>
        public int BiClrImportant;

        private int biSize;

        private short biPlanes;

        private int biCompression;

        private int biXPelsPerMeter;

        private int biYPelsPerMeter;

        /// <summary>
        /// Reads BitmapInfoHeader from the file stream.
        /// </summary>
        /// <param name="binaryReader">Used to read data from the file stream.</param>
        public void ReadBitmapInfoHeader(BinaryReader binaryReader)
        {
            this.biSize = binaryReader.ReadInt32();
            this.BiWidth = binaryReader.ReadInt32();
            this.BiHeight = binaryReader.ReadInt32();
            this.biPlanes = binaryReader.ReadInt16();
            this.BiBitCount = binaryReader.ReadInt16();
            this.biCompression = binaryReader.ReadInt32();
            this.BiSizeImage = binaryReader.ReadInt32();
            this.biXPelsPerMeter = binaryReader.ReadInt32();
            this.biYPelsPerMeter = binaryReader.ReadInt32();
            this.BiClrUsed = binaryReader.ReadInt32();
            this.BiClrImportant = binaryReader.ReadInt32();
        }

        /// <summary>
        /// Writes BitmapInfoHeader to the file stream.
        /// </summary>
        /// <param name="binaryWriter">Used to write data to the file stream.</param>
        public void WriteBitmapInfoHeader(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(this.biSize);
            binaryWriter.Write(this.BiWidth);
            binaryWriter.Write(this.BiHeight);
            binaryWriter.Write(this.biPlanes);
            binaryWriter.Write(this.BiBitCount);
            binaryWriter.Write(this.biCompression);
            binaryWriter.Write(this.BiSizeImage);
            binaryWriter.Write(this.biXPelsPerMeter);
            binaryWriter.Write(this.biYPelsPerMeter);
            binaryWriter.Write(this.BiClrUsed);
            binaryWriter.Write(this.BiClrImportant);
        }
    }
}