#include "filters.h"
#include <math.h>

void median(BYTE**** image, bmpHeader header, int myWidth, int channels)
{
	for (int i = 1; i < header.biHeight - 1; i++)
	{
		for (int j = 1; j < myWidth - 1; j++)
		{
			for (int c = 0; c < channels; c++)
			{
				int surroundings[9];
				for (int k = -1; k < 2; k++)
				{
					for (int l = -1; l < 2; l++)
					{
						surroundings[(k + 1) * 3 + l + 1] = (*image)[i + k][j + l][c];
					}
				}
				qsort(surroundings, 9, sizeof(int), cmp);
				(*image)[i][j][c] = surroundings[4];
			}
		}
	}
}

void gauss(BYTE**** image, bmpHeader header, int myWidth, int channels)
{
	double kernel[9] = { 0.0626, 0.1250, 0.0626, 0.1250, 0.2497, 0.1250, 0.0626, 0.1250, 0.0626 };
	convolution(image, header, myWidth, channels, kernel);
}

void sobelX(BYTE**** image, bmpHeader header, int myWidth, int channels)
{
	double kernel[9] = { 1, 0, -1, 2, 0, -2, 1, 0, -1 };
	convolution(image, header, myWidth, channels, kernel);
}

void sobelY(BYTE**** image, bmpHeader header, int myWidth, int channels)
{
	double kernel[9] = { 1, 2, 1, 0, 0, 0, -1, -2, -1 };
	convolution(image, header, myWidth, channels, kernel);
}

void greyscale(BYTE**** image, bmpHeader header, int myWidth, int channels)
{
	for (int i = 1; i < header.biHeight - 1; i++)
	{
		for (int j = 1; j < myWidth - 1; j++)
		{
			for (int c = 0; c < channels; c++)
			{
				if (c == 3)
					(*image)[i][j][c] = 255;
				else
					(*image)[i][j][c] = (BYTE)fmax((*image)[i][j][2], fmax((*image)[i][j][1], (*image)[i][j][0]));
			}
		}
	}
}

void convolution(BYTE**** image, bmpHeader header, int myWidth, int channels, double kernel[9])
{
	BYTE*** copy = (BYTE***)malloc(header.biHeight * sizeof(BYTE**));
	for (int i = 0; i < header.biHeight; i++)
	{
		copy[i] = (BYTE**)malloc(myWidth * sizeof(BYTE*));
		for (int j = 0; j < myWidth; j++)
		{
			copy[i][j] = (BYTE*)malloc(channels * sizeof(BYTE));
		}
	}
	
	for (int i = 1; i < header.biHeight - 1; i++)
	{
		for (int j = 1; j < myWidth - 1; j++)
		{
			for (int c = 0; c < channels; c++)
			{
				double pixel = 0;
				for (int k = -1; k < 2; k++)
				{
					for (int l = -1; l < 2; l++)
					{
						pixel += kernel[(k + 1) * 3 + l + 1] * (*image)[i + k][j + l][c];
					}
				}
				if (pixel == 0)
					copy[i][j][c] = 0;
				else if (abs(pixel) < 255)
					copy[i][j][c] = (BYTE)abs(pixel);
				else
					copy[i][j][c] = 255;
			}
		}
	}

	for (int i = 0; i < header.biHeight; i++)
	{
		for (int j = 0; j < myWidth; j++)
		{
			for (int c = 0; c < channels; c++)
			{
				(*image)[i][j][c] = copy[i][j][c];
			}
		}
	}

	for (int i = 0; i < header.biHeight; i++)
	{
		for (int j = 0; j < myWidth; j++)
		{
			free(copy[i][j]);
		}
		free(copy[i]);
	}
	free(copy);
}

int cmp(const void* p1, const void* p2)
{
	return (*(int*)p1 - *(int*)p2);
}