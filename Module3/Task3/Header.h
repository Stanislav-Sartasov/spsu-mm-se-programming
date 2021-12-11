#pragma once
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
typedef struct infoHeaderOfFile
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
} ;

#pragma pop()
bool readArgument(int);

//work with file
int fileValidation(struct infoHeaderOfFile* BITMAPFILEHEADER, FILE* importFile);
struct RGBTRIPLE** readArray(int* padding, struct infoHeaderOfFile* infMAP, FILE* importFile);
struct RGBTRIPLE** cpyArray(struct RGBTRIPLE** old_arr, struct infoHeaderOfFile* infMap);

//filters
void swap(unsigned char* a, unsigned char* b);
void filterMedian(int width, int height, struct RGBTRIPLE** rgb_arr, struct RGBTRIPLE** new_arr);
void applyMedianToPixels(struct RGBTRIPLE** arr, struct RGBTRIPLE** new_arr, int idx, int idy);

void filterBlackandWhite(int width, int height, struct RGBTRIPLE** rgb_arr, struct RGBTRIPLE** new_arr);

void filterGauss(int width, int height, struct RGBTRIPLE** rgb_arr, struct RGBTRIPLE** new_arr, int size);
void applyGaussToPixels(struct RGBTRIPLE** arr, struct RGBTRIPLE** new_arr, int idx, int idy, int size);

void filterSobelXY(int width, int height, struct RGBTRIPLE** rgb_arr, struct RGBTRIPLE** new_arr, int axis);
void applySobelToPixels(struct RGBTRIPLE** arr, struct RGBTRIPLE** new_arr, int idx, int idy, int axis);