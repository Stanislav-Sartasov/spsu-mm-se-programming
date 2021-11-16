#include "structs.h"
#include "filters.h"
#include "image_functions.h"

int main(int argc, char* argv[])
{
	if (argc != 4)
	{
		printf("Wrong count of input parameters.\n");
		return -1;
	}

	printf("This programm modificate your bmp image with 6 availible filters:\n"
	"1 -> SobelX\n2 -> SobelY\n3 -> SobelBoth\n4 -> Middle\n5 -> Gauss 3x3\n6 -> Grey\n");

	if ((strcmp(argv[2], "SobelX") == 0) || (strcmp(argv[2], "SobelY") == 0) || (strcmp(argv[2], "SobelBoth") == 0) ||
		(strcmp(argv[2], "Middle") == 0) || (strcmp(argv[2], "Gauss 3x3") == 0) || (strcmp(argv[2], "Grey") == 0))
	{
		printf("\nYou chose non-exist filter.\n");
		return -2;
	}

	Bitmap_file header;
	FILE* file = fopen(argv[1], "rb");
	
	if (file == NULL)
	{
		printf("Invalid file path specified");
		return -3;
	}

	if (!(fread(&header, sizeof(char), sizeof(Bitmap_file), file)))
	{
		printf("The file cannot be read or the file is corrupted");
		return -4;
	}

	if (header.name[0] != 'B' || header.name[1] != 'M' || !(header.bitsperpixel == 24 || header.bitsperpixel == 32))
	{
		printf("Invalid file format. Perhaps the bmp file does not have 24 or 32 bytes.");
		return -5;
	}

	if (header.compression != 0)
	{
		printf("Error! Your Bmp file is compessed.\n");
		return -6;
	}
	
	Image image = read_image(file, header.height, header.width, header.bitsperpixel);
	if (strcmp(argv[2], "SobelX") == 0)
		image = sobel_filter(image, -1);
	else if (strcmp(argv[2], "SobelY"))
		image = sobel_filter(image, 1);
	else if (strcmp(argv[2], "SobelBoth"))
		image = sobel_filter(image, 0);
	else if (strcmp(argv[2], "Middle"))
		middle_filter(image);
	else if (strcmp(argv[2], "Gauss3x3"))
		gausses_filter_3x3(image);
	else
		grey_filter(image);
	save_bmp_file(header, image, argv[3]);
	free_image(image);
	fclose(file);
}
