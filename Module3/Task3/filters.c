#include"Header.h"

void swap(unsigned char* a, unsigned char* b)
{
	unsigned char temp = *a;
	*a = *b;
	*b = temp;
}

void filterMedian(int width, int height, struct RGBTRIPLE** rgbArr, struct RGBTRIPLE** newArr)
{
	for (int i = 1; i < height - 3; i++)
	{
		for (int j = 1; j < width - 3; j++)
		{
			applyMedianToPixels(rgbArr, newArr, j, i);
		}
	}
}

void applyMedianToPixels(struct RGBTRIPLE** arr, struct RGBTRIPLE** newArr, int idX, int idY)
{
	unsigned char arrG[9], arrB[9], arrR[9];

	for (int i = 0; i < 9; i++)
	{
		struct RGBTRIPLE* tempRGB = &arr[idY - 1 + i / 3][idX - 1 + i % 3];

		arrB[i] = tempRGB->b;
		arrG[i] = tempRGB->g;
		arrR[i] = tempRGB->r;
	}

	for (int j = 0; j < 9; j++)
	{
		for (int i = 0; i < 9; i++)
		{
			if (arrB[i] > arrB[j])
			{
				swap(&arrB[j], &arrB[i]);
			}

			if (arrG[i] > arrG[j])
			{
				swap(&arrG[j], &arrG[i]);
			}

			if (arrR[i] > arrR[j])
			{
				swap(&arrR[j], &arrR[i]);
			}
		}
	}

	newArr[idY][idX].r = arrR[4];
	newArr[idY][idX].b = arrB[4];
	newArr[idY][idX].g = arrG[4];

}

void filterBlackandWhite(int width, int height, struct RGBTRIPLE** rgbARR, struct RGBTRIPLE** newARR)
{
	for (int i = 0; i < height; i++)
	{
		for (int j = 0; j < width; j++)
		{
			unsigned char d = (unsigned char)((rgbARR[i][j].g + rgbARR[i][j].b + rgbARR[i][j].r) / 3);

			newARR[i][j].b = d;
			newARR[i][j].g = d;
			newARR[i][j].r = d;
		}
	}
}

const int gaussMatrix5x5[5][5] = { {1, 4,  6,  4,  1},
								   {4, 16, 24, 16, 4},
								   {6, 24, 36, 24, 6},
								   {4, 16, 24, 16, 4},
								   {1, 4,  6,  4,  1} };

const int gaussMatrix3x3[3][3] = { {1, 2, 1},
								   {2, 4, 2},
								   {1, 2, 1} };

const int sobelx_matrix[3][3] = { {1,  2,  1},
								 {0,  0,  0},
								 {-1, -2, -1} };

const int sobely_matrix[3][3] = { {-1, 0, 1},
								 {-2, 0, 2},
								 {-1, 0, 1} };

void filterSobelXY(int width, int height, struct RGBTRIPLE** rgbARR, struct RGBTRIPLE** newARR, int axis)
{
	for (int i = 1; i < height - 3; i++)
	{
		for (int j = 1; j < width - 3; j++)
		{
			applySobelToPixels(rgbARR, newARR, j, i, axis);
		}
	}
}


void filterGauss(int width, int height, struct RGBTRIPLE** rgbARR, struct RGBTRIPLE** newARR, int size)
{
	for (int i = size / 2; i < height - size; i++)
	{
		for (int j = size / 2; j < width - size; j++)
		{
			applyGaussToPixels(rgbARR, newARR, j, i, size);
		}
	}
}

void applyGaussToPixels(struct RGBTRIPLE** arr, struct RGBTRIPLE** newARR, int idX, int idY, int size)
{
	int sumG = 0, sumB = 0, sumR = 0;
	int div = size < 4 ? 16 : 256;

	for (int i = 0; i < size * size; i++)
	{
		if (size == 3)
		{
			struct RGBTRIPLE* tempRGB = &arr[idY - 1 + i / size][idX - 1 + i % size];

			sumB += tempRGB->b * gaussMatrix3x3[i / size][i % size];
			sumG += tempRGB->g * gaussMatrix3x3[i / size][i % size];
			sumR += tempRGB->r * gaussMatrix3x3[i / size][i % size];
		}
		else
		{
			struct RGBTRIPLE* tempRGB = &arr[idY - 2 + i / size][idX - 2 + i % size];

			sumB += tempRGB->b * gaussMatrix5x5[i / size][i % size];
			sumG += tempRGB->g * gaussMatrix5x5[i / size][i % size];
			sumR += tempRGB->r * gaussMatrix5x5[i / size][i % size];
		}
	}

	newARR[idY][idX].r = (unsigned char)(sumR / div);
	newARR[idY][idX].b = (unsigned char)(sumB / div);
	newARR[idY][idX].g = (unsigned char)(sumG / div);
}


void applySobelToPixels(struct RGBTRIPLE** arr, struct RGBTRIPLE** newARR, int idX, int idY, int axis)
{
	int sumG = 0, sumB = 0, sumR = 0;

	for (int i = 0; i < 9; i++)
	{
		struct RGBTRIPLE* tempRGB = &arr[idY - 1 + i / 3][idX - 1 + i % 3];

		if (axis == 1)
		{
			sumB += tempRGB->b * sobelx_matrix[i / 3][i % 3];
			sumG += tempRGB->g * sobelx_matrix[i / 3][i % 3];
			sumR += tempRGB->r * sobelx_matrix[i / 3][i % 3];
		}
		else
		{
			sumB += tempRGB->b * sobely_matrix[i / 3][i % 3];
			sumG += tempRGB->g * sobely_matrix[i / 3][i % 3];
			sumR += tempRGB->r * sobely_matrix[i / 3][i % 3];
		}

	}

	if (sumR < 0) sumR = 0;
	if (sumB < 0) sumB = 0;
	if (sumG < 0) sumG = 0;

	if (sumB > 255) sumB = 255;
	if (sumG > 255) sumG = 255;
	if (sumR > 255) sumR = 255;

	newARR[idY][idX].r = (unsigned char)(sumR);
	newARR[idY][idX].b = (unsigned char)(sumB);
	newARR[idY][idX].g = (unsigned char)(sumG);
}