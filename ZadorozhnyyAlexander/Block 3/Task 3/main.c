#include "structs.h"
#include "filters.h"
#include "image_functions.h"

int main(int argc, char* argv[])
{
	printf("This programm modificate your bmp image_file with 6 availible filters:\n"
		"1 -> SobelX\n2 -> SobelY\n3 -> SobelBoth\n4 -> Middle\n5 -> Gauss3x3\n6 -> Grey\n");

	if (argc != 4)
	{
		printf("Wrong count of input parameters. You must enter only 4 parameters, but you enter: %d \n", argc);
		return 0;
	}

	if (!((strcmp(argv[2], "SobelX") == 0) || (strcmp(argv[2], "SobelY") == 0) || (strcmp(argv[2], "SobelBoth") == 0) ||
		(strcmp(argv[2], "Middle") == 0) || (strcmp(argv[2], "Gauss3x3") == 0) || (strcmp(argv[2], "Grey") == 0)))
	{
		printf("\nYou chose non-exist filter.\n");
		return 0;
	}

	bitmap_file header;
	FILE* file = fopen(argv[1], "rb");
	
	if (file == NULL)
	{
		printf("Invalid file path specified");
		return -1;
	}

	if (!(fread(&header, sizeof(char), sizeof(bitmap_file), file)))
	{
		printf("The file cannot be read or the file is corrupted");
		return -2;
	}

	if (header.name[0] != 'B' || header.name[1] != 'M' || !(header.bitsperpixel == 24 || header.bitsperpixel == 32))
	{
		printf("Invalid file format. Perhaps the bmp file does not have 24 or 32 bytes.");
		return -3;
	}

	if (header.compression != 0) 
		return printf("Error! Your Bmp file is compessed.\n");
	
	image image_file = read_image(file, header.height, header.width, header.bitsperpixel);
	if (strcmp(argv[2], "SobelX") == 0)
		image_file = sobel_filter(image_file, -1);
	else if (strcmp(argv[2], "SobelY") == 0)
		image_file = sobel_filter(image_file, 1);
	else if (strcmp(argv[2], "SobelBoth") == 0)
		image_file = sobel_filter(image_file, 0);
	else if (strcmp(argv[2], "Middle") == 0)
		middle_filter(image_file);
	else if (strcmp(argv[2], "Gauss3x3") == 0)
		gausses_filter_3x3(image_file);
	else
		grey_filter(image_file);
	if (save_bmp_file(header, image_file, argv[3]))
		printf("Your image_file was successfully filtered!");
	free_image(image_file);
	fclose(file);
}