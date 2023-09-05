#define _CRT_NO_SECURE_WARNINGS

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

struct bmpHeader
{
	char identifier[2]; // ������������� 
	unsigned int size; // ������ bmp �����
	int garbage; // �� �����
	unsigned int imageOffset; // �������� �����������
	// (��������� ����� �����, � ������� �����������)
};

struct bmpInfoHeader
{
	unsigned int headerSize; // ������ ����� ���������
	// � �����������
	int width; // ������ �����������
	int height; // ������ �����������
	unsigned short int numberOfColorPlanes;
	unsigned short int numberBitsPerPixel; // ��������� ���/����
	unsigned int compressionMethod; // ����� ������
	unsigned int imageSize; // ������ �����������
	int horizontalResolution; // �������� �� �������������� ����
	int verticalResolution; // �������� �� ������������ ����
	unsigned int numberColors; // ���������� ������ � �������
	unsigned int numberImportantColors; // ���������� ������ ������
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