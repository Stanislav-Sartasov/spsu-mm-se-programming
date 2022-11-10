namespace Task1
{
    public class Pixel
    {
        public byte[] ArrRGB = new byte[3];

        public Pixel(byte[] data)
        {
            ArrRGB[0] = data[0];
            ArrRGB[1] = data[1];
            ArrRGB[2] = data[2];
        }
    }
}
