namespace BMPFileFilter
{
    public class Rgb
    {
        private int Length;

        public byte Blue;
        public byte Green;
        public byte Red;
        public byte Reserved;

        public Rgb(byte[] list)
        {
            Length = list.Length;
            Blue = list[0];
            Green = list[1];
            Red = list[2];
            if (Length == 4)
                Reserved = list[3];
        }

        public byte[] GetListBytes()
        {
            if (Length == 3)
            {
                byte[] res_3 = { Blue, Green, Red };
                return res_3;
            }
            byte[] res_4 = { Blue, Green, Red, Reserved };
            return res_4;
        }
    }
}
