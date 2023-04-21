#include <stdio.h>
#include "bmpHeader.h"

bmpHeader bmpLoad(FILE* file)
{
	bmpHeader header;

	fseek(file, 18, SEEK_SET);
	fread(&(header.biWidth), sizeof(int), 1, file);

	fseek(file, 22, SEEK_SET);
	fread(&(header.biHeight), sizeof(int), 1, file);

	fseek(file, 28, SEEK_SET);
	fread(&(header.biBitCount), sizeof(WORD), 1, file);

	return header;
}