#define _CRT_SECURE_NO_WARNINGS

#include "bmpEditor.h"

int main(int argc, char** argv)
{
	struct bmpHeader header;
	struct bmpInfoHeader headerInfo;
	struct img picture;
	FILE* fp;

	printf("This program converts the specified input bmp"
		" file according to the specified filter into the specified output file\n");
	printf("Please enter in the format shown:"
		" <program name> <input file> <filter type> <output file>\n");
	if (argc != 4)
	{
		printf("Error! Not enough arguments for the program\n");
		printf("Re-enter as <program name> <input file> <filter type> <output file>\n");
		exit(-1);
	}

	fp = fileOpen(argv[1], "rb");
	readHeaders(fp, &header, &headerInfo);
	picture = readImage(fp, headerInfo.height, headerInfo.width, headerInfo.numberBitsPerPixel);
	applyFilter(&picture, argv[2]);
	writeImage(header, headerInfo, picture, argv, headerInfo.numberBitsPerPixel);
	printf("Successfully! Filter %s applied to bmp image %s, result saved in %s\n", argv[2], argv[1], argv[3]);
	fclose(fp);
	return 0;
}
