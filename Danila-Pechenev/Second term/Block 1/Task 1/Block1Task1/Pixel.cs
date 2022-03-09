namespace Block1Task1
{
    /// <summary>
    /// Stores the values of each pixel channel (RGB).
    /// </summary>
    public struct Pixel
    {
        /// <summary>
        /// Blue-channel of the pixel.
        /// </summary>
        public byte Blue;

        /// <summary>
        /// Green-channel of the pixel.
        /// </summary>
        public byte Green;

        /// <summary>
        /// Red-channel of the pixel.
        /// </summary>
        public byte Red;

        /// <summary>
        /// Reads pixel from the file stream.
        /// </summary>
        /// <param name="binaryReader">Used to read data from the file stream.</param>
        public void ReadPixel(BinaryReader binaryReader)
        {
            this.Blue = binaryReader.ReadByte();
            this.Green = binaryReader.ReadByte();
            this.Red = binaryReader.ReadByte();
        }

        /// <summary>
        /// Writes pixel to the file stream.
        /// </summary>
        /// <param name="binaryWriter">Used to write data to the file stream.</param>
        public void WritePixel(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(this.Blue);
            binaryWriter.Write(this.Green);
            binaryWriter.Write(this.Red);
        }
    }
}