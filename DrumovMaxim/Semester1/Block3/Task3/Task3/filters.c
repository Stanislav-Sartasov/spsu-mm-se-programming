#include "filters.h"

int32_t cmpr(const void* a, const void* b)
{
	return (*(int32_t*)a - *(int32_t*)b);
}

uint32_t normalize(int32_t number, int32_t max)
{
	if (number > 0)
	{
		return number < max ? number : max;
	}
	return 0;
}
int16_t readBmp(FILE* file, bitMapFileHeader* fHead, bitMapInfoHeader* iHead, image* img)
{
	fread(fHead, sizeof(*fHead), 1, file);
	fread(iHead, sizeof(*iHead), 1, file);

	if (fHead->bfType[0] != 'B' || fHead->bfType[1] != 'M')
	{
		printf("This is not a BMP type\n");
		return 1;
	}

	if (iHead->biBitCount != 24 && iHead->biBitCount != 32)
	{
		printf("It's not 24 or 32 bits per pixel.\n");
		return 1;
	}

	if (iHead->biCompression != 0)
	{
		printf("The file is compressed\n");
		return 1;
	}

	img->height = iHead->biHeight;
	img->width = iHead->biWidth;
	img->paletteSize = fHead->bfOffBits - sizeof(bitMapFileHeader) - sizeof(bitMapInfoHeader);

	if (img->paletteSize)
	{
		img->palette = (int8_t*)malloc(img->paletteSize);
		fread(img->palette, img->paletteSize, 1, file);
	}

	img->padding = iHead->biBitCount == 32 ? 0 : (4 - iHead->biWidth % 4) % 4;
	img->height = iHead->biHeight;
	img->width = iHead->biWidth;
	img->pixels = (colors**)malloc(sizeof(colors*) * img->height);

	if (img->pixels == NULL)
	{
		printf("We cannot allocate memory for image pixels.\n");
		return 1;
	}

	fseek(file, fHead->bfOffBits, SEEK_SET);
	int8_t* useless = (int8_t*)malloc(sizeof(int8_t) * img->padding);

	if (useless == NULL)
	{
		printf("We cannot allocate memory for useless bytes.\n");
		return 1;
	}

	for (size_t i = 0; i < img->height; i++)
	{
		img->pixels[i] = (colors*)malloc(sizeof(colors) * img->width);
		if (img->pixels[i] == NULL)
		{
			printf("We cannot allocate memory for image pixels.\n");
			return 1;
		}

		for (size_t j = 0; j < img->width; j++)
		{
			fread(&img->pixels[i][j].rgb, 1, 3, file);
			if (iHead->biBitCount == 32)
			{
				fread(&img->pixels[i][j].alpha, 1, 1, file);
			}
		}
		fread(useless, img->padding, 1, file);
	}
	free(useless);
	return 0;
}

int16_t writeBmp(FILE* file, bitMapFileHeader fHead, bitMapInfoHeader iHead, image* img)
{
	fwrite(&fHead, sizeof(bitMapFileHeader), 1, file);
	fwrite(&iHead, sizeof(bitMapInfoHeader), 1, file);
	if (img->paletteSize)
	{
		fwrite(img->palette, img->paletteSize, 1, file);
		free(img->palette);
	}

	for (size_t i = 0; i < img->height; ++i)
	{
		for (size_t j = 0; j < img->width; j++)
		{
			fwrite(&img->pixels[i][j].rgb, 1, 3, file);
			if (iHead.biBitCount == 32)
			{
				fwrite(&img->pixels[i][j].alpha, 1, 1, file);
			}
		}
		free(img->pixels[i]);
		fwrite('0', img->padding, 1, file);
	}
	free(img->pixels);
	return 0;
}

void gray(image* img)
{
	uint8_t result;
	for (size_t i = 0; i < img->height; i++)
	{
		for (size_t j = 0; j < img->width; j++)
		{
			result = (uint8_t)(img->pixels[i][j].rgb[0] * 0.1 + img->pixels[i][j].rgb[1] * 0.6 + img->pixels[i][j].rgb[2] * 0.2);
			img->pixels[i][j].rgb[0] = img->pixels[i][j].rgb[1] = img->pixels[i][j].rgb[2] = result;
		}
	}
}

void median(image* img)
{
	int32_t squarePix[9];
	uint32_t p;
	for (size_t i = 0; i < img->height; i += 3)
	{
		for (size_t j = 0; j < img->width; j += 3)
		{
			for (size_t rgb = 0; rgb < 3; rgb++)
			{
				p = 0;
				for (int32_t x = i - 1; x < i + 2; x++)
				{
					for (int32_t y = j - 1; y < j + 2; y++)
					{
						squarePix[p++] = img->pixels[normalize(x, img->height - 1)][normalize(y, img->width - 1)].rgb[rgb];
					}
				}
				qsort(squarePix, 9, sizeof(int32_t), cmpr);
				for (int32_t k = i - 1; k < i + 2; k++)
				{
					for (int32_t l = j - 1; l < j + 2; l++)
					{
						img->pixels[normalize(k, img->height - 1)][normalize(l, img->width - 1)].rgb[rgb] = squarePix[4];
					}
				}
			}

		}
	}
}

void applyKernel(image* img, double* kernel, uint32_t kernelSize)
{
	colors** newPixels = (colors**)malloc(sizeof(colors*) * img->height);

	if (newPixels == NULL)
	{
		printf("We cannot allocate memory for image pixels.\n");
		return 1;
	}

	int64_t newValue;

	for (size_t i = 0; i < img->height; i++)
	{
		newPixels[i] = (colors*)malloc(sizeof(colors) * img->width);
		if (newPixels[i] == NULL)
		{
			printf("We cannot allocate memory for image pixels.\n");
			return 1;
		}
	}

	for (size_t i = 0; i < img->height; i++)
	{
		for (size_t j = 0; j < img->width; j++)
		{
			for (size_t rgb = 0; rgb < 3; rgb++)
			{
				newValue = 0;
				for (size_t x = 0; x < kernelSize; x++)
				{
					for (size_t y = 0; y < kernelSize; y++)
					{
						newValue += img->pixels[normalize(i - kernelSize / 2 + x, img->height - 1)][normalize(j - kernelSize / 2 + y, img->width - 1)].rgb[rgb] * kernel[x * kernelSize + y];
					}
				}
				newValue = abs(newValue) < 255 ? abs(newValue) : 255;
				newPixels[i][j].rgb[rgb] = (uint8_t)newValue;
			}
		}
	}
	for (size_t i = 0; i < img->height; i++)
	{
		for (size_t j = 0; j < img->width; j++)
		{
			for (size_t rgb = 0; rgb < 3; rgb++)
			{
				img->pixels[i][j].rgb[rgb] = newPixels[i][j].rgb[rgb];
			}
		}
	}
	for (size_t i = 0; i < img->height; i++)
	{
		free(newPixels[i]);
	}
	free(newPixels);
}

void gauss(image* img, uint32_t kernelSize)
{
	double* kernel = malloc(sizeof(double) * kernelSize * kernelSize);
	double sum = 0;
	double sigma = 0.3 * ((kernelSize * kernelSize - 1) * 0.5 - 1) + 0.8;

	for (int32_t i = 0; i < kernelSize; i++)
	{
		for (int32_t j = 0; j < kernelSize; j++)
		{
			kernel[i * kernelSize + j] = exp(-pow(i * kernelSize + j - (kernelSize * kernelSize - 1) / 2, 2) / pow(2 * sigma, 2));
			sum += kernel[i * kernelSize + j];
		}
	}
	sum = 1.0 / sum;
	for (size_t i = 0; i < kernelSize; i++)
	{
		for (size_t j = 0; j < kernelSize; j++)
		{
			kernel[i * kernelSize + j] *= sum;
		}
	}
	applyKernel(img, kernel, kernelSize);
	free(kernel);
}

void sobel(image* img, uint8_t name)
{
	gray(img);
	double kernelX[] = { -1, 0, 1,-2, 0, 2, -1, 0, 1 };
	double kernelY[] = { -1, -2, -1, 0, 0, 0, 1, 2, 1 };
	if (name == 'X')
	{
		applyKernel(img, kernelX, 3);
	}
	else if (name == 'Y')
	{
		applyKernel(img, kernelY, 3);
	}
}

void filter(image* img, int8_t* nameFilter)
{
	if (!strcmp(nameFilter, "gray"))
	{
		gray(img);
	}
	else if (!strcmp(nameFilter, "median"))
	{
		median(img);
	}
	else if (!strcmp(nameFilter, "gaussFive"))
	{
		gauss(img, 5);
	}
	else if (!strcmp(nameFilter, "gaussThree"))
	{
		gauss(img, 3);
	}
	else if (!strcmp(nameFilter, "sobelX"))
	{
		sobel(img, 'X');
	}
	else if (!strcmp(nameFilter, "sobelY"))
	{
		sobel(img, 'Y');
	}
	else
	{
		printf("No filter with such name\n");
		return;
	}
}