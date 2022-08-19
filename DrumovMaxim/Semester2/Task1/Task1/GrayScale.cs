namespace Task1
{
    public class GrayScale : IFilter
    {
        public void PixelConvolution(ref Image bmpImage)
        {
            int avg;
            
            for (int i = 0; i < bmpImage.BiWidth; i++)
            {
                for (int j = 0; j < bmpImage.BiHeight; j++)
                {
                    byte[] data = new byte[3];

                    Pixel pixel = bmpImage.GetPixel(i, j);
                    avg = (pixel.ArrRGB[0] + pixel.ArrRGB[1] + pixel.ArrRGB[2]) / 3;
                   
                    for (int k = 0; k < 3; k++)
                    {
                        data[k] = (byte)avg;
                    }

                    bmpImage.SetPixel(new Pixel(data), i, j);
                }
            }
        }
    }
}
