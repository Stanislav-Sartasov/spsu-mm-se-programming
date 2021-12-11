#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#include "filters.h"



int compare(const int* x1, const int* x2)
{
	return *x1 - *x2;
}

void greyFilter(struct image* image)
{
	double average;
	for (int coefficient = 0; coefficient < image->height; coefficient++)
	{
		for (int coefficient2 = 0; coefficient2 < image->width; coefficient2++)
		{
			average = (image->pixels[coefficient][coefficient2].blue + image->pixels[coefficient][coefficient2].green + image->pixels[coefficient][coefficient2].red) / 3;
			image->pixels[coefficient][coefficient2].blue = (int)average;
			image->pixels[coefficient][coefficient2].green = (int)average;
			image->pixels[coefficient][coefficient2].red = (int)average;
		}
	}
}

int sobelYFunction(int* massive)
{
	int proportions[9] = { -1, -2, -1, 0, 0, 0, 1, 2, 1 };
	int result = 0;
	for (int coefficient = 0; coefficient < 9; coefficient++)
	{
		result += massive[coefficient] * proportions[coefficient];
	}
	return (int)min(255, abs(result));
}

int sobelXFunction(int* massive)
{
	int proportions[9] = { -1, 0, 1, -2, 0, 2, -1, 0, 1 };
	int result = 0;
	for (int coefficient = 0; coefficient < 9; coefficient++)
	{
		result += massive[coefficient] * proportions[coefficient];
	}
	return (int)min(255, abs(result));
}

int gauss3Function(int* massive)
{
	int proportions[9] = { 1, 2, 1, 2, 4, 2, 1, 2, 1 };
	double result = 0;
	for (int coefficient = 0; coefficient < 9; coefficient++)
	{
		result += massive[coefficient] * proportions[coefficient] / 16;
	}
	return (int)result;
}

int gauss5Function(int* massive)
{
	int proportions[25] = { 1, 4, 7, 4, 1, 4, 16, 26, 16, 4, 7, 26, 41, 26, 7, 4, 16, 26, 16, 4, 1, 4, 7, 4, 1 };
	double result = 0;
	for (int coefficient = 0; coefficient < 25; coefficient++)
	{
		result += massive[coefficient] * proportions[coefficient] / 273;
	}
	return (int)result;
}

int medianFunction(int* massive)
{
	qsort(massive, 9, sizeof(unsigned int), compare);
	return massive[4];
}

void allocationError()
{
	printf("ERROR: failed to allocate memory.\n");
	printf("Completing the programm.\n");
	exit(-1);
}

int* memoryAllocationForRGB(int number)
{
	int* buffer;
	if ((buffer = (int*)malloc(number * number * sizeof(int))) == NULL)
	{
		allocationError();
	}
	return buffer;
}

int filter(struct image* image, int (*filterFunction)(int*), int number)
{
	int* blue, * green, * red;
	blue = memoryAllocationForRGB(number);
	green = memoryAllocationForRGB(number);
	red = memoryAllocationForRGB(number);

	int coef1, coef2, coef3, coef4, offset, n, row, column;
	offset = number / 2;

	struct rgb** buffer;
	if ((buffer = (struct rgb**)malloc(image->height * sizeof(struct rgb*))) == NULL)
	{
		allocationError();
	}
	for (coef1 = 0; coef1 < image->height; coef1++)
	{
		if ((buffer[coef1] = (struct rgb*)malloc(image->width * sizeof(struct rgb))) == NULL)
		{
			allocationError();
		}
		memcpy(buffer[coef1], image->pixels[coef1], image->width * sizeof(struct rgb));
	}

	for (coef1 = 0; coef1 < image->height; coef1++)
	{
		for (coef2 = 0; coef2 < image->width; coef2++)
		{
			n = 0;
			for (coef3 = -offset; coef3 < offset + 1; coef3++)
			{
				row = coef1 + coef3;
				for (coef4 = -offset; coef4 < offset + 1; coef4++)
				{
					column = coef2 + coef4;
					if (row < 0 || row > image->height - 1)
					{
						row -= coef3;
					}
					if (column < 0 || column > image->width - 1)
					{
						column -= coef4;
					}

					blue[n] = buffer[row][column].blue;
					green[n] = buffer[row][column].green;
					red[n] = buffer[row][column].red;
					n++;
				}
			}
			image->pixels[coef1][coef2].blue = filterFunction(blue);
			image->pixels[coef1][coef2].green = filterFunction(green);
			image->pixels[coef1][coef2].red = filterFunction(red);
		}
	}

	for (coef1 = 0; coef1 < image->height; coef1++)
	{
		free(buffer[coef1]);
	}
	free(buffer);
	free(blue);
	free(green);
	free(red);
	return 0;
}