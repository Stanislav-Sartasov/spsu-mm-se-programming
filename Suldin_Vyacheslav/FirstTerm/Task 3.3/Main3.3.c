#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <math.h>

double gaussFunction(int x, int y, float sigma)
{
	double tmp = 2 * sigma * sigma;
	return (1 / (tmp * 3.14)) * pow(2.7, -((x * x + y * y) / tmp));
}

int compare(const void* x1, const void* x2) 
{
	return (*(int*)x1 - *(int*)x2);
}

struct BMPHeader
{
	char name[2];
	unsigned int size;
	int somes;
	unsigned int offSetBits;
};

struct DIBHeader
{
	unsigned int headerSize;
	unsigned int widht;
	unsigned int height;
	unsigned short int colorPlanes;
	unsigned short int bitsPerPixel;
	unsigned int compression;
	unsigned int imageSize;
	int tmp[4];
};

void corrector(unsigned char** image, int row, int h,int bits, int layer)
{
	for (int k = layer-1; k >= 0; k--)
	{
		for (int i = 0 + k; i < h + k; i += h - 1 - 2*k)
			for (int j = 0 + k * bits; j < row - k * bits; j += bits)
				for (int n = 0; n < 3;n++)
					if (i == 0 + k) image[i][j + n] = image[i + 1][j + n];
					else image[i][j + n] = image[i - 1][j + n];
		for (int j = 0 + k * bits; j < row + k * bits; j += row - bits - 2 * k * bits)
			for (int i = 0 + k; i < h - k; i++)
				for (int n = 0; n < 3;n++)
					if (j == 0 + k * bits) image[i][j + n] = image[i][j + bits + n];
					else image[i][j + n] = image[i][j - bits + n];
	}
}

void grey(unsigned char** image, int row, int h, int bits)
{
	for (int i = 0; i < h; i++)
		for (int j = 0; j < row; j += bits)
			image[i][j] = image[i][j + 1] = image[i][j + 2] = image[i][j] * 0.2126 + image[i][j + 1] * 0.7152 + image[i][j + 2] * 0.0722;
}

void negative(unsigned char** image, int row, int h, int bits)
{
	for (int i = 0; i < h; i++)
		for (int j = 0; j < row; j += bits)
			for (int k = 0; k < 3; k++)
				image[i][j + k] = (unsigned char)(255 - image[i][j + k]);
}			

void median(unsigned char** image, int row, int h, int bits)
{
	for (int i = 1; i < h - 1; i++)
		for (int j = bits; j < row - bits; j += bits)
			for (int n = 0; n < 3;n++)
			{
				int a[9] = { 0 };
				int count = 0;
				for (int p = 0; p < 3;p++)
					for (int p1 = 0; p1 < 3;p1++)
					{
						a[count] = (int)image[i - 1 + p][j - bits + bits * p1 + n];
						count++;
					}
				qsort(a, 9, sizeof(int), compare);
				image[i][j + n] = (unsigned char)a[4];
			}
	corrector(image, row, h, bits, 1);
}

void fastMedian(unsigned char** image, int row, int h, int bits)
{
	int** a = (int**)malloc(256 * sizeof(int*));
	for (int l = 0; l < 256; l++)
	{
		a[l] = (int*)malloc(bits * sizeof(int));
		for (int y = 0; y < bits;y++)
			a[l][y] = 0;
	}

	for (int i = 1; i < h - 1; i++)
	{
		for (int l = 0; l < 256; l++)
			for (int y = 0; y < bits;y++)
				a[l][y] = 0;
		for (int j = bits; j < row - bits; j += bits)
			for (int n = 0; n < 3;n++)
			{
				if (j == 3)
					for (int p = 0; p < 3;p++)
						for (int p1 = 0; p1 < 3;p1++)
							a[(int)image[i - 1 + p][j - 3 + 3 * p1 + n]][n] += 1;
				else
					for (int p = 0; p < 3;p++)
					{
						a[(int)image[i - 1 + p][j - 6 + n]][n] -= 1;
						a[(int)image[i - 1 + p][j + 3 + n]][n] += 1;
					}
				int count = 0;
				for (int m = 0; m < 256; m++)
				{
					count += a[m][n];
					if (count >= 5)
					{
						a[image[i][j + n]][n]--;
						image[i][j + n] = m;
						a[image[i][j + n]][n]++;
						break;
					}
				}
				
		}
	}
	for (int o = 0; o < 256; o++)
	{
		free(a[o]);
	}
	free(a);
	corrector(image, row, h, bits, 1);
}

void gauss(unsigned char** image, int row, int h, int bits, int coreSize, int power)
{
	int imp = (int)(coreSize / 2 + 1) - coreSize;
	double** core = (double**)malloc(coreSize * sizeof(double*));
	for (int l = 0; l < coreSize; l++)
	{
		core[l] = (double*)malloc(coreSize * sizeof(double));
		for (int y = 0; y < coreSize;y++)
			core[l][y] = 0;
	}
	double sum = 0;
	for (int p = 0; p < coreSize;p++)
		for (int p1 = 0; p1 < coreSize;p1++)
		{
			double gau = gaussFunction(imp + p, imp + p1, 1);
			core[p][p1] = gau;
			sum += gau;
		}
	for (int p = 0; p < coreSize;p++)
		for (int p1 = 0; p1 < coreSize;p1++)
			core[p][p1] = core[p][p1] / sum;
	for (int u = 0; u < power;u++)
		for (int i = (-1) * imp; i < h + imp; i++)
			for (int j = (-1) * imp * bits; j < row + imp * bits; j += bits)
				for (int n = 0; n < 3;n++)
				{
					double sum = 0;
					for (int p = 0; p < coreSize;p++)
						for (int p1 = 0; p1 < coreSize;p1++)
							sum += core[p][p1] * (int)image[i + imp + p][j + bits * (imp + p1) + n];
					image[i][j + n] = (int)sum;
				}
	corrector(image, row, h, bits, imp*(-1));
}

void sobelSharr(unsigned char** image, int row, int h, int bits, int type, int s)
{
	grey(image, row, h, bits);
	gauss(image, row, h, bits, 3, 1);
	int sensivity = 0;
	int coreSobelX[3][3] = { {-1, 0, 1}, {-2, 0, 2}, {-1, 0, 1} };
	int coreSobelY[3][3] = { {-1, -2, -1}, {0, 0, 0}, {1, 2, 1} };
	int coreSharrX[3][3] = { {3, 0, -3}, {10, 0, -10}, {3, 0, -3} };
	int coreSharrY[3][3] = { {3, 10, 3}, {0, 0, 0}, {-3, -10, -3} };
	int corex[3][3] = { {0} };
	int corey[3][3] = { {0} };
	if (s == 0)
	{
		sensivity = 75;
		for (int i = 0; i < 3; i++)
			for (int j = 0; j < 3; j++)
			{
				corex[i][j] = coreSobelX[i][j];
				corey[i][j] = coreSobelY[i][j];
			}
	}
	else
	{
		sensivity = 300;
		for (int i = 0; i < 3; i++)
			for (int j = 0; j < 3; j++)
			{
				corex[i][j] = coreSharrX[i][j];
				corey[i][j] = coreSharrY[i][j];
			}
	}
	
	double** a = (double**)malloc(h * sizeof(double*));
	for (int l = 0; l < h; l++)
		a[l] = (double*)malloc((row/ bits) * sizeof(double));

	for (int i = 1; i < h - 1; i++)
	{
		for (int j = bits; j < row - bits; j += bits)
		{
			int sumx = 0, sumy = 0;
			for (int p = 0; p < 3;p++)
				for (int p1 = 0; p1 < 3;p1++)
				{
					sumx += corex[p][p1] * (int)image[i - 1 + p][j - bits + bits * p1];
					sumy += corey[p][p1] * (int)image[i - 1 + p][j - bits + bits * p1];
				}
			if (type == 0) a[i][j / bits] = abs(sumx) < sensivity ? 0 : 255;
			else if (type == 1) a[i][j / bits] = abs(sumy) < sensivity ? 0 : 255;
			else if (type == 2) a[i][j / bits] = sqrt(sumx * sumx + sumy * sumy) < sensivity ? 0 : 255;
			else a[i][j / bits] = sqrt(sumx * sumx + sumy * sumy) < sensivity ? -10 : atan((sumx / (double)sumy+0.000001));
		}
	}
	for (int i = 1; i < h - 1; i++)
		for (int j = bits; j < row - bits; j += bits)
			for (int n = 0; n < 3; n++)
				image[i][j + n] = a[i][j / bits];
	corrector(image, row, h, bits, 1);
}

int main(int argc, char* argv[])
{
	printf("Descriprion: BMP 24 and 32 bit filters: negative, median, fastMedian(using histogram), gauss(3,5,...),\n sobelX, sobelY, sobelXY, scharrX, scharrY, scharrXY, grey.\n\n");
	if (argc != 4) return printf("wrong input");
	FILE* fin, *fout;
	if (!(fin = fopen(argv[1], "rb"))) return printf("file <in> wasnt opened");
	struct BMPHeader header;
	struct DIBHeader dibHeader;
	fread(header.name, 2, 1, fin);
	fread(&header.size, 3*sizeof(int), 1, fin);
	fread(&dibHeader.headerSize, sizeof(struct DIBHeader), 1, fin);
	
	int w = dibHeader.widht, h = dibHeader.height, row = w * dibHeader.bitsPerPixel / 8;
	unsigned char** image = (unsigned char**)malloc(h * sizeof(unsigned char*));

	for (int i = 0; i < h; i++)
	{
		image[i] = (unsigned char*)malloc((row + 1) * sizeof(unsigned char));
		fread(image[i], sizeof(unsigned char), row * sizeof(unsigned char), fin);
	}
	fclose(fin);

	if (!(fout = fopen(argv[3], "wb"))) return printf("file <out> wasnt opened");
	fwrite(header.name, 2, 1, fout);
	fwrite(&header.size, 3 * sizeof(int), 1, fout);
	fwrite(&dibHeader,sizeof(struct DIBHeader),1, fout);

	char filters[11][11] = {"negative","median","fastMedian","gauss","sobelX","sobelY","sobelXY","scharrX","scharrY","scharrXY", "grey"};

	int filter = -1;
	for (int f = 0; f < 11;f++)
	{
		int p0 = 0,p1 =0;
		for ( p1 = 0; argv[2][p1] != '\0';p1++)
			if (filters[f][p1] == argv[2][p1])
				p0++;
		if (p0 == p1)
		{
			filter = f;
			break;
		}
	}

	if (filter == -1)
		printf("input error: no such filter");
	else if (filter == 0) negative(image, row, h, dibHeader.bitsPerPixel / 8);
	else if (filter == 1) median(image, row, h, dibHeader.bitsPerPixel / 8);
	else if (filter == 2) fastMedian(image, row, h, dibHeader.bitsPerPixel / 8);
	else if (filter == 3) gauss(image, row, h, dibHeader.bitsPerPixel / 8, 5, 1);

	else if (filter == 4) sobelSharr(image, row, h, dibHeader.bitsPerPixel / 8, 0,0);
	else if (filter == 5) sobelSharr(image, row, h, dibHeader.bitsPerPixel / 8, 1,0);
	else if (filter == 6) sobelSharr(image, row, h, dibHeader.bitsPerPixel / 8, 2,0);

	else if (filter == 7) sobelSharr(image, row, h, dibHeader.bitsPerPixel / 8, 0,1);
	else if (filter == 8) sobelSharr(image, row, h, dibHeader.bitsPerPixel / 8, 1,1);
	else if (filter == 9) sobelSharr(image, row, h, dibHeader.bitsPerPixel / 8, 2,1);

	else if (filter == 10) grey(image, row, h, dibHeader.bitsPerPixel / 8);

	for (int i = 0; i < h; i++)
		fwrite(image[i], sizeof(unsigned char), row * sizeof(unsigned char), fout);
	fclose(fout);

	printf("Completed");
	return 0;
}