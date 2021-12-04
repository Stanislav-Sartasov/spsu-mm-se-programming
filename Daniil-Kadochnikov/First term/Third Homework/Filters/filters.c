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
	for (int coefficient = 0; coefficient < image->height * image->width; coefficient++)
	{
		average = (image->pixels[coefficient].blue + image->pixels[coefficient].green + image->pixels[coefficient].red) / 3;
		image->pixels[coefficient].blue = (int)average;
		image->pixels[coefficient].green = (int)average;
		image->pixels[coefficient].red = (int)average;
	}
}

int sobelYFunction(int* massive)
{
	int proportions[9] = { -1,-2,-1,0,0,0,1,2,1 };
	int result = 0;
	for (int coefficient = 0; coefficient < 9; coefficient++)
	{
		result += massive[coefficient] * proportions[coefficient];
	}
	return (int)min(255, abs(result));
}

int sobelXFunction(int* massive)
{
	int proportions[9] = { -1,0,1,-2,0,2,-1,0,1 };
	int result = 0;
	for (int coefficient = 0; coefficient < 9; coefficient++)
	{
		result += massive[coefficient] * proportions[coefficient];
	}
	return (int)min(255, abs(result));
}

int gaussFunction(int* massive)
{
	int proportions[9] = { 1,2,1,2,4,2,1,2,1 };
	double result = 0;
	for (int coefficient = 0; coefficient < 9; coefficient++)
	{
		result += massive[coefficient] * proportions[coefficient] / 16;
	}
	return (int)result;
}

int medianFunction(int* massive)
{
	qsort(massive, 9, sizeof(unsigned int), compare);
	return massive[4];
}

int filter(struct image* image, int (*filterFunction)(int*))
{
	struct rgb* buffer;
	if ((buffer = (struct rgb*)malloc(image->height * image->width * sizeof(struct rgb))) == NULL)
	{
		printf("ERROR: failed to allocate memory.\n");
		printf("Completing the programm.\n");
		return -1;
	}
	memcpy(buffer, image->pixels, image->height * image->width * sizeof(struct rgb));

	unsigned int blue[9], green[9], red[9];
	int coef1, coef2, coef3, coef4, pix, n;

	for (coef1 = 1; coef1 < image->height - 1; coef1++)
	{
		for (coef2 = 1; coef2 < image->width - 1; coef2++)
		{
			int pix = coef1 * image->width + coef2;
			n = 0;
			for (coef3 = -1; coef3 < 2; coef3++)
			{
				for (coef4 = -1; coef4 < 2; coef4++)
				{
					blue[n] = buffer[pix + (image->width * coef3) + coef4].blue;
					green[n] = buffer[pix + (image->width * coef3) + coef4].green;
					red[n] = buffer[pix + (image->width * coef3) + coef4].red;
					n++;
				}
			}
			image->pixels[pix].blue = filterFunction(blue);
			image->pixels[pix].green = filterFunction(green);
			image->pixels[pix].red = filterFunction(red);
			pix++;
		}
	}
	free(buffer);
	return 0;
}