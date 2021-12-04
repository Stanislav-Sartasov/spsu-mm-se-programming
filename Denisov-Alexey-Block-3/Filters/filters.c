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
						int position = (k + 1) * 3 + l + 1;
						if ( position == 0 || position == 2 || position == 6 || position == 8)
							pixel += 0.0626 * (*image)[i + k][j + l][c];
						else if (position == 1 || position == 3 || position == 5 || position == 7)
							pixel += 0.125 * (*image)[i + k][j + l][c];
						else
							pixel += 0.2497 * (*image)[i + k][j + l][c];
					}
				}
				(*image)[i][j][c] = (BYTE)pixel;
			}
		}
	}
}

void sobelX(BYTE**** image, bmpHeader header, int myWidth, int channels)
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
						int position = (k + 1) * 3 + l + 1;
						if (position == 0 || position == 6)
							pixel -= (*image)[i + k][j + l][c];
						else if (position == 2 || position == 8)
							pixel += (*image)[i + k][j + l][c];
						else if (position == 3)
							pixel -= 2 * (*image)[i + k][j + l][c];
						else if (position == 5)
							pixel += 2 * (*image)[i + k][j + l][c];
					}
				}
				if (pixel == 0)
					copy[i][j][c] = 0;
				else if (abs(pixel) < 255)
					copy[i][j][c] = abs(pixel);
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

void sobelY(BYTE**** image, bmpHeader header, int myWidth, int channels)
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
						int position = (k + 1) * 3 + l + 1;
						if (position == 0 || position == 2)
							pixel -= (*image)[i + k][j + l][c];
						else if (position == 6 || position == 8)
							pixel += (*image)[i + k][j + l][c];
						else if (position == 1)
							pixel -= 2 * (*image)[i + k][j + l][c];
						else if (position == 7)
							pixel += 2 * (*image)[i + k][j + l][c];

					}
				}
				if (pixel == 0)
					copy[i][j][c] = 0;
				else if (abs(pixel) < 255)
					copy[i][j][c] = abs(pixel);
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

int cmp(const void* p1, const void* p2)
{
		return (*(int*)p1 - *(int*)p2);
}