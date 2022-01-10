#define _CRT_SECURE_NO_WARNINGS

#include <stdint.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <string.h>
#include <stdbool.h>
#include <conio.h>
#include <math.h>

typedef uint16_t WORD;
typedef uint32_t DWORD;
typedef uint32_t LONG;

#pragma pack(1)

struct infoHeaderOfFile
{
	WORD bfType;
	DWORD bfSize;
	WORD bfReserved1;
	WORD bfReserved2;
	DWORD bfOffBits;

	DWORD biSize;
	LONG biWidth;
	LONG biHeight;
	WORD biPlanes;
	WORD biBitCount;
	DWORD biCompression;
	DWORD biSizeImage;
	LONG biXPelsPerMeter;
	LONG biYPelsPerMeter;
	DWORD biClrUsed;
	DWORD biClrImportant;
};
struct RGBTRIPLE
{
	unsigned char b;
	unsigned char g;
	unsigned char r;
};
#pragma pop()

bool readArgument(int);
int fileValidation(struct infoHeaderOfFile* inf, FILE* importFile);
struct RGBTRIPLE** readArray(int* padding, struct infoHeaderOfFile infMAP, FILE* importFile);
void filterBlackandWhite(struct infoHeaderOfFile bh, struct RGBTRIPLE** rgbArr, FILE* fileExport);
void applyMatrixToPixel(struct infoHeaderOfFile headerFile, struct RGBTRIPLE** rgbArr, FILE* fileExport, double* matrix, bool sob);
void filterGauss(struct infoHeaderOfFile headerFile, struct pixel_info** rgbArr, FILE* fileExport);
void filterSobelXY(struct infoHeaderOfFile headerFile, struct pixel_info** rgbArr, FILE* fileExport, int axis);
void filterMedian(struct infoHeaderOfFile headerFile, struct pixel_info** rgbArr, FILE* fileExport);