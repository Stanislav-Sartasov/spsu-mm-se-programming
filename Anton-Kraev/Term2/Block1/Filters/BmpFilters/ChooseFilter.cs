using BmpFilters.Filters;

namespace BmpFilters
{
    public class ChooseFilter
    {
        public static bool ApplyFilter(BitMapReading input, string filterName, string output)
        {
            uint width = input.Width;
            uint height = input.Height;
            int channels = input.BitsPerPixel / 8;
            byte[][] image = input.Image;
            byte[][] filteredImage;

            switch (filterName)
            {
                case "GRAY":
                    filteredImage = GrayFilter.ApplyFilter(width, height, channels, image);
                    break;
                case "MEDIAN":
                    filteredImage = MedianFilter.ApplyFilter(width, height, channels, image);
                    break;
                case "GAUSS":
                    filteredImage = ConvolutionFilters.GaussFilter(width, height, channels, image);
                    break;
                case "SOBELX":
                    filteredImage = ConvolutionFilters.SobelXFilter(width, height, channels, image);
                    break;
                case "SOBELY":
                    filteredImage = ConvolutionFilters.SobelYFilter(width, height, channels, image);
                    break;
                case "SOBEL":
                case "SOBELXY":
                    filteredImage = ConvolutionFilters.SobelFilter(width, height, channels, image);
                    break;
                default:
                    return false;
            }

            try
            {
                new BitMapWriting(output, input.Header, filteredImage);
            }
            catch (Exception)
            {
                Console.WriteLine("Could not open\\create output file");
                Environment.Exit(3);
            }

            Console.WriteLine($"{filterName} filter was applied successfully");
            return true;
        }
    }
}