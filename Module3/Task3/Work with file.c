#include"Header.h"

int fileValidation(struct infoHeaderOfFile* inf, FILE* importFile)
{
	if (importFile == NULL)
	{
		printf("��������� ������! ��������� �� ������ ������� ��������� ���� ����!");
		return 0;
	}

	fread(inf, sizeof(struct infoHeaderOfFile), 1, importFile);

	if (inf->bfType != 0x4D42 && inf->bfType != 0x4349 && inf->bfType != 0x5450)
	{
		printf("��������� ������! ������ �� ������ ������ (�� BMP)!");
		return 0;
	}

	if (inf->biSize != 40 && inf->biSize != 108 && inf->biSize != 124)
	{

		printf("��������� ������! �������� ������ BITMAPINFO!");
		return 0;
	}

	if (inf->biBitCount != 24 && inf->biBitCount != 32)
	{
		printf("��������� �����, ��������� �������� ������ � 24 � 32-�������� �������\n");
		return 0;
	}

	return inf->biBitCount;
}
struct RGBTRIPLE** readArray(int* padding, struct infoHeaderOfFile infMAP, FILE* importFile)
{
	struct RGBTRIPLE** rgbArr = calloc(sizeof(struct RGBTRIPLE*), infMAP.biHeight);

	for (int i = 0; i < infMAP.biHeight; i++)
	{
		rgbArr[i] = calloc(infMAP.biWidth, sizeof(struct RGBTRIPLE));
	}

	*padding = (4 - (infMAP.biWidth * (infMAP.biBitCount / 8)) % 4) & 3;

	for (int i = 0; i < infMAP.biHeight; i++)
	{
		for (int j = 0; j < infMAP.biWidth; j++)
		{
			rgbArr[i][j].b = (unsigned char)getc(importFile);
			rgbArr[i][j].g = (unsigned char)getc(importFile);
			rgbArr[i][j].r = (unsigned char)getc(importFile);
			if (infMAP.biBitCount == 32)
			{
				getc(importFile);
			}
		}

		for (int k = 0; k < *padding; k++)
		{
			getc(importFile);
		}
	}

	fclose(importFile);

	return rgbArr;
}

