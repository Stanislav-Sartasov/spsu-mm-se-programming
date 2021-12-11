#define _CRT_NO_SECURE_WARNINGS

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

struct bmpHeader
{
	char identifier[2]; // идентификатор 
	unsigned int size; // размер bmp файла
	int garbage; // не нужно
	unsigned int imageOffset; // смещение изображения
	// (начальный адрес байта, в котором изображение)
};

struct bmpInfoHeader
{
	unsigned int headerSize; // размер этого заголовка
	// с информацией
	int width; // ширина изображения
	int height; // высота изображения
	unsigned short int numberOfColorPlanes;
	unsigned short int numberBitsPerPixel; // количеств бит/пикс
	unsigned int compressionMethod; // метод сжатия
	unsigned int imageSize; // размер изображения
	int horizontalResolution; // пикселей на горизонтальный метр
	int verticalResolution; // пикселей на вертикальный метр
	unsigned int numberColors; // количество цветов в палитре
	unsigned int numberImportantColors; // количество важных цветов
};

struct argb
{
	unsigned char blue;
	unsigned char green;
	unsigned char red;
	unsigned char alpha;
};

struct img
{
	int height;
	int width;
	struct argb** argb;
};

int cmp(unsigned char* x, unsigned char* y);

FILE* fileOpen(const char* fileName, const char* mode);

void fileWrite(const void* buf, size_t elemSize, size_t elemCount, FILE* stream);

void fileRead(void* buf, size_t elemSize, size_t elemCount, FILE* stream);

void applyMedianFilter(struct img picture, struct argb** cur, int i, int j);

void applySobelXFilter(struct img picture, struct argb** cur, int i, int j);

void applySobelYFilter(struct img picture, struct argb** cur, int i, int j);

void applyGaussFilter(struct img picture, struct argb** cur, int i, int j);

void applyGrayFilter(struct img* picture);

void applyFilter(struct img* picture, char* filterName);

struct img readImage(FILE* fp, int height, int width, unsigned short int numberBitsPerPixel);

void writeImage(struct bmpHeader header, struct bmpInfoHeader headerInfo, struct img picture, char** argv, unsigned short int numberBitsPerPixel);

void readHeaders(FILE* fp, struct bmpHeader* header, struct bmpInfoHeader* headerInfo);