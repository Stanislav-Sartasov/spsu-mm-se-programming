#include "Header.h"

void filterBlackandWhite(struct infoHeaderOfFile headerFile, struct RGBTRIPLE** rgbArr, FILE* fileExport)
{
	fwrite(&headerFile, sizeof(headerFile), 1, fileExport);
	for (int i = 0; i < headerFile.biHeight; i++)
	{
		for (int j = 0; j < headerFile.biWidth; j++)
		{

			int d = (rgbArr[i][j].r + rgbArr[i][j].g + rgbArr[i][j].b) / 3;

			putc(d, fileExport);
			putc(d, fileExport);
			putc(d, fileExport);

			if (headerFile.biBitCount == 32)
				putc(0, fileExport);
		}
	}
}

void applyMatrixToPixel(struct infoHeaderOfFile headerFile, struct RGBTRIPLE** rgbArr, FILE* fileExport, double* matrix, bool sob)
{
	fwrite(&headerFile, sizeof(headerFile), 1, fileExport);

	for (int i = 0; i < headerFile.biHeight; i++)
	{
		for (int j = 0; j < headerFile.biWidth; j++)
		{
			if (i == 0 || j == 0 || i == headerFile.biHeight - 1 || j == headerFile.biWidth - 1)
			{
				putc(0, fileExport);
				putc(0, fileExport);
				putc(0, fileExport);

				if (headerFile.biBitCount == 32)
					putc(0, fileExport);

				continue;
			}

			double sumR = 0;
			double sumG = 0;
			double sumB = 0;

			for (int mask_i = 0; mask_i < 3; mask_i++)
			{
				for (int mask_j = 0; mask_j < 3; mask_j++)
				{
					sumR += rgbArr[i - 1 + mask_i][j - 1 + mask_j].r * matrix[3 * mask_i + mask_j];
					sumG += rgbArr[i - 1 + mask_i][j - 1 + mask_j].g * matrix[3 * mask_i + mask_j];
					sumB += rgbArr[i - 1 + mask_i][j - 1 + mask_j].b * matrix[3 * mask_i + mask_j];
				}
			}

			if (sob)
			{
				if (sumR < 0) sumR = 0;
				if (sumB < 0) sumB = 0;
				if (sumG < 0) sumG = 0;

				if (sumB > 255) sumB = 255;
				if (sumG > 255) sumG = 255;
				if (sumR > 255) sumR = 255;
			}

			putc((unsigned char)sumB, fileExport);
			putc((unsigned char)sumG, fileExport);
			putc((unsigned char)sumR, fileExport);

			if (headerFile.biBitCount == 32)
				putc(0, fileExport);
		}
	}
}

const double medianMatrix[9] = { 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9 };
const double gaussMatrix[9] = { 1.0 / 16, 1.0 / 8, 1.0 / 16, 1.0 / 8, 1.0 / 4, 1.0 / 8, 1.0 / 16, 1.0 / 8, 1.0 / 16 };
const double sobelxMatrixX[9] = { -1.0, -2.0, -1.0, 0.0, 0.0, 0.0, 1.0, 2.0, 1.0 };
const double sobelyMatrixY[9] = { -1.0, 0.0, 1.0, -2.0, 0.0, 2.0, -1.0, 0.0, 1.0 };

void  filterMedian(struct infoHeaderOfFile headerFile, struct pixel_info** rgbArr, FILE* fileExport)
{
	applyMatrixToPixel(headerFile, rgbArr, fileExport, medianMatrix, false);
}

void filterGauss(struct infoHeaderOfFile headerFile, struct pixel_info** rgbArr, FILE* fileExport)
{
	applyMatrixToPixel(headerFile, rgbArr, fileExport, gaussMatrix, false);
}

void filterSobelXY(struct infoHeaderOfFile headerFile, struct pixel_info** rgbArr, FILE* fileExport, int axis)
{
	if (axis == 1)
	{
		applyMatrixToPixel(headerFile, rgbArr, fileExport, sobelxMatrixX, true);
	}
	else
	{
		applyMatrixToPixel(headerFile, rgbArr, fileExport, sobelyMatrixY, true);
	}
}


