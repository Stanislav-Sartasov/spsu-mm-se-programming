namespace BMPFileFilter
{
    public class Rgb
    {
        private int length;

        public byte Blue;
        public byte Green;
        public byte Red;
        public byte Reserved;

        public Rgb(byte[] list)
        {
            length = list.Length;
            Blue = list[0];
            Green = list[1];
            Red = list[2];
            if (length == 4)
                Reserved = list[3];
        }

        public byte[] GetListBytes()
        {
            if (length == 3)
            {
                byte[] result = { Blue, Green, Red };
                return result;
            }
            byte[] newResult = { Blue, Green, Red, Reserved };
            return newResult;
        }
    }
}
