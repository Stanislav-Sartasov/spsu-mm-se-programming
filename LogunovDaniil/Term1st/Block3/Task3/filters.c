#include <string.h>
#include <stdlib.h>
#include <stdio.h>

#define DX_ARR { -1, 0, 1, -1, 0, 1, -1, 0, 1 }
#define DY_ARR { -1, -1, -1, 0, 0, 0, 1, 1, 1 }

typedef unsigned char byte;

int cmp(byte* a, byte* b)
{
	return (int)*a - (int)*b;
}

int grayscale(byte** img, int height, int width, int byteCount)
{
	for (int i = 0; i < height; i++)
	{
		for (int j = 0; j < width; j += byteCount)
		{
			int avg = ((int)img[i][j] + img[i][j + 1] + img[i][j + 2]) / 3;
			img[i][j] = img[i][j + 1] = img[i][j + 2] = avg;
		}
	}
	return 0;
}

int noClip(int min, int max, int val)
{
	if (val < min)
		return min;
	if (val > max)
		return max;
	return val;
}

byte applyMatrixSingle(byte* values, int* matrix, int normal)
{
	int avg = 0;
	for (int i = 0; i < 9; i++)
		avg += (int)values[i] * matrix[i];
	return noClip(0, 255, avg / normal);
}

int applyFilter(byte** img, int height, int width, int byteCount, byte (*filter)(byte*))
{
	int dx[] = DX_ARR;
	int dy[] = DY_ARR;
	byte toFilter[3][9];

	byte** newImg = malloc(sizeof(byte*) * height);
	if (newImg == NULL)
		return -1;
	int success = 1;
	for (int i = 0; i < height; i++)
	{
		newImg[i] = malloc(sizeof(byte) * width);
		success &= newImg[i] != NULL;
	}
	if (!success)
		return -1;

	for (int i = 0; i < height; i++)
	{
		for (int j = 0; j < width; j += byteCount)
		{
			for (int k = 0; k < 9; k++)
			{
				int nxtX = i + dx[k];
				int nxtY = j + dy[k] * byteCount;
				for (int clr = 0; clr < 3; clr++)
					if (nxtX >= 0 && nxtX < height && nxtY >= 0 && nxtY < width)
						toFilter[clr][k] = img[nxtX][nxtY + clr];
					else
						toFilter[clr][k] = 0;
			}
			for (int clr = 0; clr < 3; clr++)
				newImg[i][j + clr] = filter(toFilter[clr]);
		}
	}

	for (int i = 0; i < height; i++)
	{
		for (int j = 0; j < width; j += byteCount)
		{
			for (int clr = 0; clr < 3; clr++)
				img[i][j + clr] = newImg[i][j + clr];
		}
		free(newImg[i]);
	}
	free(newImg);

	return 0;
}

byte gaussFilter(byte* values)
{
	int matrix[] = { 1, 2, 1, 2, 4, 2, 1, 2, 1 };
	return applyMatrixSingle(values, matrix, 16);
}

byte sobelYFilter(byte* values)
{
	int matrix[] = { 1, 2, 1, 0, 0, 0, -1, -2, -1 };
	return applyMatrixSingle(values, matrix, 1);
}

byte sobelXFilter(byte* values)
{
	int matrix[] = { 1, 0, -1, 2, 0, -2, 1, 0, -1 };
	return applyMatrixSingle(values, matrix, 1);
}

byte medianFilter(byte* values)
{
	qsort(values, 9, sizeof(byte), cmp);
	return values[4];
}

int gauss(byte** img, int height, int width, int byteCount)
{
	return applyFilter(img, height, width, byteCount, gaussFilter);
}

int median(byte** img, int height, int width, int byteCount)
{
	return applyFilter(img, height, width, byteCount, medianFilter);
}

int sobelX(byte** img, int height, int width, int byteCount)
{
	grayscale(img, height, width, byteCount);
	return applyFilter(img, height, width, byteCount, sobelXFilter);
}

int sobelY(byte** img, int height, int width, int byteCount)
{
	grayscale(img, height, width, byteCount);
	return applyFilter(img, height, width, byteCount, sobelYFilter);
}