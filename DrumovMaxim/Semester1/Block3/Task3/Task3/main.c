#include <stdlib.h>
#include <stdio.h>
#include "filters.h"

int main(int argc, char* argv[])
{
	FILE* fIn, * fOut;
	if (argc != 4)
	{
		printf("Arguments are not enough");
		return 1;
	}

	if (fopen_s(&fIn, argv[1], "rb") || fopen_s(&fOut, argv[3], "wb"))
	{
		printf("Files could not be opened");
		return -1;
	}

	bitMapFileHeader fHead;
	bitMapInfoHeader iHead;
	image img;

	if (readBmp(fIn, &fHead, &iHead, &img))
	{
		printf("Image data could not be read");
		return 1;
	}

	filter(&img, argv[2]);

	if (writeBmp(fOut, fHead, iHead, &img))
	{
		printf("Failed to record image data");
		return 1;
	}

	fclose(fIn);
	fclose(fOut);
	printf("The program has finished with success");
	return 0;
}