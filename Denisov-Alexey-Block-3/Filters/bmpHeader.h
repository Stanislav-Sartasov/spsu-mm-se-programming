#include <stdio.h>

#ifndef BMP_HEADER
#define BMP_HEADER

typedef unsigned __int16 WORD;
typedef unsigned char BYTE;
#define HEADER_SIZE 54

struct bmp
{
	int biWidth;
	int biHeight;
	WORD biBitCount;
};
typedef struct bmp bmpHeader;

bmpHeader bmpLoad(FILE* file);

#endif // BMP_HEADER
