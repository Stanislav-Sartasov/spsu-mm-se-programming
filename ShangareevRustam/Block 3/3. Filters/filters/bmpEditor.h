#define _CRT_NO_SECURE_WARNINGS

#include <stdio.h>

struct rgb
{
	unsigned char blue;
	unsigned char green;
	unsigned char red;
};

struct img
{
	int height;
	int width;
	struct rgb** rgb;
};

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
	unsigned char alpha;
	unsigned char blue;
	unsigned char green;
	unsigned char red;
};

struct img32
{
	int height;
	int width;
	struct argb** argb;
};

int min(int a, int b);

int cmp(int* x, int* y);

unsigned char grayScale(struct rgb rgb);

void medianFilter(struct img* picture);

void sobelXFilter(struct img* picture);

void sobelYFilter(struct img* picture);

void gaussFilter(struct img* picture);

void grayFilter(struct img* picture);

struct img readImage(FILE* fp, int height, int width);

void writeImage(struct bmpHeader header, struct bmpInfoHeader headerInfo, struct img picture, char** argv);

int cmp(int* x, int* y);

unsigned char grayScale32(struct argb argb);

void medianFilter32(struct img32* picture);

void sobelXFilter32(struct img32* picture);

void sobelYFilter32(struct img32* picture);

void gaussFilter32(struct img32* picture);

void grayFilter32(struct img32* picture);

struct img32 readImage32(FILE* fp, int height, int width);

void writeImage32(struct bmpHeader header, struct bmpInfoHeader headerInfo, struct img32 picture, char** argv);