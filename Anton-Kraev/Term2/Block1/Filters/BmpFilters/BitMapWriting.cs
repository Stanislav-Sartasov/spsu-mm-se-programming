namespace BmpFilters
{
    public class BitMapWriting
    {
        public BitMapWriting(string fileName, byte[] header, byte[][] image)
        {
            using (var fstream = new BinaryWriter(File.Open(fileName, FileMode.OpenOrCreate)))
            {
                fstream.Write(header);
                for (int i = 0; i < image.Length; i++)
                    fstream.Write(image[i]);
            }
        }
    }
}
