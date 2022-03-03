namespace BMPFileFilter
{
    public class Rgb
    {
        private int length;

        public byte blue;
        public byte green;
        public byte red;
        public byte reserved;

        public Rgb(byte[] list)
        {
            length = list.Length;
            blue = list[0];
            green = list[1];
            red = list[2];
            if (length == 4)
                reserved = list[3];
        }

        public byte[] GetListBytes()
        {
            if (length == 3)
            {
                byte[] res_3 = { blue, green, red };
                return res_3;
            }
            byte[] res_4 = { blue, green, red, reserved };
            return res_4;
        }
    }
}
