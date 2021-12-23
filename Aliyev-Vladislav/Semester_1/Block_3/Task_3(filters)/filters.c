#include <math.h>
#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include "filters.h"
#include "tools.h"

void gray(struct pixel* pixels, int height, int width)
{
	int total;
	for (int i = 0; i < height * width; i++)
	{
		total = (pixels[i].r + pixels[i].g + pixels[i].b) / 3;
		pixels[i].r = total;
		pixels[i].g = total;
		pixels[i].b = total;
	}
}

float gauss(int* a)
{
	float total = 0;
	for (int i = 0; i < 9; i++)
	{
		if ((i == 0) || (i == 2) || (i == 6) || (i == 8))
			total += a[i] / 16;
		if ((i == 1) || (i == 3) || (i == 5) || (i == 7))
			total += a[i] / 8;
		if (i == 4)
			total += a[i] / 4;
	}
	return total;
}

int sobelX(int* a)
{
	float total = 0;
	for (int i = 0; i < 9; i++)
	{
		if ((i == 0) || (i == 6))
			total -= a[i];
		if (i == 3)
			total -= a[i] * 2;
		if ((i == 2) || (i == 8))
			total += a[i];
		if (i == 5)
			total += a[i] * 2;
	}
	return (int)min(255, max(0, abs(total)));
}

int sobelY(int* a)
{
	float total = 0;
	for (int i = 0; i < 9; i++)
	{
		if ((i == 0) || (i == 2))
			total -= a[i];
		if (i == 1)
			total -= a[i] * 2;
		if ((i == 6) || (i == 8))
			total += a[i];
		if (i == 7)
			total += a[i] * 2;
	}
	return (int)min(255, max(0, abs(total)));
}

void filter(struct pixel* pixels, int height, int width, char* filter)
{
	struct pixel* pixels_copy;
	pixels_copy = (struct pixel*)malloc(width * height * sizeof(struct pixel));
	memcpy(pixels_copy, pixels, width * height * sizeof(struct pixel));
	int* red;
	int* green;
	int* blue;
	red = (int*)malloc(9 * sizeof(int));
	green = (int*)malloc(9 * sizeof(int));
	blue = (int*)malloc(9 * sizeof(int));
	for (int i = 0; i < height * width - 2 * width; i++)
	{
		for (int j = 0; j < 3; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				if ((i % width != 0) && (i < (width * height - width)))
				{
					red[j + 3 * k] = pixels_copy[i + j + width * k].r;
					green[j + 3 * k] = pixels_copy[i + j + width * k].g;
					blue[j + 3 * k] = pixels_copy[i + j + width * k].b;
				}
			}
		}
		if (strcmp(filter, "median") == 0)
		{
			pixels[i + 1 + width].r = sort(red);
			pixels[i + 1 + width].g = sort(green);
			pixels[i + 1 + width].b = sort(blue);
		}
		if (strcmp(filter, "gauss") == 0)
		{
			pixels[i + 1 + width].r = (int)gauss(red);
			pixels[i + 1 + width].g = (int)gauss(green);
			pixels[i + 1 + width].b = (int)gauss(blue);
		}
		if (strcmp(filter, "sobelX") == 0)
		{
			pixels[i + 1 + width].r = (int)sobelX(red);
			pixels[i + 1 + width].g = (int)sobelX(green);
			pixels[i + 1 + width].b = (int)sobelX(blue);
		}
		if (strcmp(filter, "sobelY") == 0)
		{
			pixels[i + 1 + width].r = (int)sobelY(red);
			pixels[i + 1 + width].g = (int)sobelY(green);
			pixels[i + 1 + width].b = (int)sobelY(blue);
		}
	}
	free(red);
	free(green);
	free(blue);
	free(pixels_copy);
}