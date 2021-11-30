#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <math.h>

int get_filter(char* argv[])
{
	char filters[10][11] = { "median","fastMedian","gauss","sobelX","sobelY","sobelXY","scharrX","scharrY","scharrXY", "grey" };

	int filter = -1;
	for (int f = 0; f < 10;f++)
	{
		int i = 0, j = 0;
		for (j = 0; argv[2][j] != '\0';j++)
			if (filters[f][j] == argv[2][j])
				i++;
		if (i == j)
		{
			filter = f;
			break;
		}
	}
	return filter;
}


double gauss_function(int x, int y, float sigma)
{
	double tmp = 2 * sigma * sigma;
	return (1 / (tmp * 3.14)) * pow(2.7, -((x * x + y * y) / tmp));
}

int compare(const void* first, const void* second)
{
	return (*(int*)first - *(int*)second);
}

struct first_header
{
	char name[2];
	unsigned int size;
	int somes;
	unsigned int offset;
};

struct second_header
{
	unsigned int header_size;
	unsigned int widht;
	unsigned int height;
	unsigned short int colors;
	unsigned short int bits_per_pixel;
	unsigned int compression;
	unsigned int image_size;
	int tmp[4];
};
typedef struct second_header second_header;

void corrector(unsigned char** image, second_header sec_header, int layer)
{
	int bits = sec_header.bits_per_pixel / 8;

	for (int k = layer - 1; k >= 0; k--)
	{
		for (int i = 0 + k; i < sec_header.height + k; i += sec_header.height - 1 - 2 * k)
			for (int j = 0 + k * bits; j < sec_header.widht * bits - k * bits; j += bits)
				for (int n = 0; n < 3;n++)
					if (i == 0 + k) image[i][j + n] = image[i + 1][j + n];
					else image[i][j + n] = image[i - 1][j + n];
		for (int j = 0 + k * bits; j < sec_header.widht * bits + k * bits; j += sec_header.widht * bits - bits - 2 * k * bits)
			for (int i = 0 + k; i < sec_header.height - k; i++)
				for (int n = 0; n < 3;n++)
					if (j == 0 + k * bits) image[i][j + n] = image[i][j + bits + n];
					else image[i][j + n] = image[i][j - bits + n];
	}
}

void grey(unsigned char** image, second_header sec_header)
{
	int bits = sec_header.bits_per_pixel / 8;

	for (int i = 0; i < sec_header.height; i++)
		for (int j = 0; j < sec_header.widht * bits; j += bits)
			image[i][j] = image[i][j + 1] = image[i][j + 2] = image[i][j] * 0.2126 + image[i][j + 1] * 0.7152 + image[i][j + 2] * 0.0722;
}

void median(unsigned char** image, second_header sec_header)
{
	int bits = sec_header.bits_per_pixel / 8;

	for (int i = 1; i < sec_header.height - 1; i++)
		for (int j = bits; j < sec_header.widht * bits - bits; j += bits)
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
	corrector(image, sec_header, 1);
}

void fast_median(unsigned char** image, second_header sec_header)
{
	int bits = sec_header.bits_per_pixel / 8;

	int** bar_graph = (int**)malloc(256 * sizeof(int*));
	for (int l = 0; l < 256; l++)
	{
		bar_graph[l] = (int*)malloc(bits * sizeof(int));
		for (int y = 0; y < bits;y++)
			bar_graph[l][y] = 0;
	}

	for (int i = 1; i < sec_header.height - 1; i++)
	{
		for (int l = 0; l < 256; l++)
			for (int y = 0; y < bits;y++)
				bar_graph[l][y] = 0;
		for (int j = bits; j < sec_header.widht * bits - bits; j += bits)
			for (int n = 0; n < 3;n++)
			{
				if (j == 3)
					for (int p = 0; p < 3;p++)
						for (int p1 = 0; p1 < 3;p1++)
							bar_graph[(int)image[i - 1 + p][j - 3 + 3 * p1 + n]][n] += 1;
				else
					for (int p = 0; p < 3;p++)
					{
						bar_graph[(int)image[i - 1 + p][j - 6 + n]][n] -= 1;
						bar_graph[(int)image[i - 1 + p][j + 3 + n]][n] += 1;
					}
				int count = 0;
				for (int m = 0; m < 256; m++)
				{
					count += bar_graph[m][n];
					if (count >= 5)
					{
						bar_graph[image[i][j + n]][n]--;
						image[i][j + n] = m;
						bar_graph[image[i][j + n]][n]++;
						break;
					}
				}

			}
	}
	for (int i = 0; i < 256; i++)
	{
		free(bar_graph[i]);
	}
	free(bar_graph);

	corrector(image, sec_header, 1);
}

void apply_filter(unsigned char** image, second_header sec_header, double** filter_x, double** filter_y, int core_size, int type)
{
	int bits = sec_header.bits_per_pixel / 8;

	int imp = (int)(core_size / 2 + 1) - core_size;

	int k = 0, sensivity = 0;

	if (type == 1) k = 2;

	if (type <= 5)  sensivity = 60;
	else sensivity = 250;

	double** tmp = (double**)malloc(sec_header.height * sizeof(double*));
	for (int l = 0; l < sec_header.height; l++)
		tmp[l] = (double*)malloc((sec_header.widht * bits) * sizeof(double));

	for (int i = (-1) * imp; i < sec_header.height + imp; i++)
		for (int j = (-1) * imp * bits; j < sec_header.widht * bits + imp * bits; j += bits)
			for (int n = 0; n < 1 + k;n++)
			{
				tmp[i][j + n] = 0;
				int sum_x = 0, sum_y = 0;
				for (int y = 0; y < core_size;y++)
					for (int x = 0; x < core_size;x++)
					{
						if (type == 1)	tmp[i][j + n] += filter_x[y][x] * (int)image[i + imp + y][j + bits * (imp + x) + n];
						else
						{
							sum_x += filter_x[y][x] * (int)image[i + imp + y][j + bits * (imp + x) + n];
							sum_y += filter_y[y][x] * (int)image[i + imp + y][j + bits * (imp + x) + n];
						}
					}
				if (type == 1) continue;
				else if (type == 3 || type == 6) tmp[i][j] = abs(sum_x) < sensivity ? 0 : 255;
				else if (type == 4 || type == 7) tmp[i][j] = abs(sum_y) < sensivity ? 0 : 255;
				else if (type == 5 || type == 8) tmp[i][j] = sqrt(sum_x * sum_x + sum_y * sum_y) < sensivity ? 0 : 255;
			}

	for (int i = (-1) * imp; i < sec_header.height + imp; i++)
		for (int j = (-1) * imp * bits; j < sec_header.widht * bits + imp * bits; j += bits)
			for (int n = 0; n < 3; n++)
				image[i][j + n] = tmp[i][j + k * n / 2];

	for (int i = 0; i < sec_header.height; i++)
	{
		free(tmp[i]);
	}
	free(tmp);
}

void gauss(unsigned char** image, second_header sec_header, int core_size, int power)
{
	int imp = (int)(core_size / 2 + 1) - core_size;

	double** core = (double**)malloc(core_size * sizeof(double*));
	for (int l = 0; l < core_size; l++)
	{
		core[l] = (double*)malloc(core_size * sizeof(double));
		for (int y = 0; y < core_size;y++)
			core[l][y] = 0;
	}
	double sum = 0;
	for (int p = 0; p < core_size;p++)
		for (int p1 = 0; p1 < core_size;p1++)
		{
			double gau = gauss_function(imp + p, imp + p1, 1);
			core[p][p1] = gau;
			sum += gau;
		}

	for (int p = 0; p < core_size;p++)
		for (int p1 = 0; p1 < core_size;p1++)
			core[p][p1] = core[p][p1] / sum;

	for (int u = 0; u < power;u++)
		apply_filter(image, sec_header, core, NULL, core_size, 1);

	for (int i = 0; i < 3; i++)
	{
		free(core[i]);
	}
	free(core);

	corrector(image, sec_header, imp * (-1));
}

void sobel_sharr(unsigned char** image, second_header sec_header, int filter)
{
	grey(image, sec_header);
	gauss(image, sec_header, 3, 1);

	int sensivity = 0;
	int core_sobel_x[3][3] = { {-1, 0, 1}, {-2, 0, 2}, {-1, 0, 1} };
	int core_sobel_y[3][3] = { {-1, -2, -1}, {0, 0, 0}, {1, 2, 1} };
	int core_sharr_x[3][3] = { {3, 0, -3}, {10, 0, -10}, {3, 0, -3} };
	int core_sharr_y[3][3] = { {3, 10, 3}, {0, 0, 0}, {-3, -10, -3} };

	double** core_x = (double**)malloc(3 * sizeof(double*));
	double** core_y = (double**)malloc(3 * sizeof(double*));
	for (int i = 0; i < 3; i++)
	{
		core_x[i] = (double*)malloc(3 * sizeof(double));
		core_y[i] = (double*)malloc(3 * sizeof(double));
		for (int j = 0; j < 3; j++)
			if (filter <= 5)
			{
				core_x[i][j] = core_sobel_x[i][j];
				core_y[i][j] = core_sobel_y[i][j];
			}
			else
			{
				core_x[i][j] = core_sharr_x[i][j];
				core_y[i][j] = core_sharr_y[i][j];
			}
	}

	apply_filter(image, sec_header, core_x, core_y, 3, filter);

	for (int i = 0; i < 3; i++)
	{
		free(core_x[i]);
		free(core_y[i]);
	}

	free(core_x);
	free(core_y);

	corrector(image, sec_header, 1);
}

int main(int argc, char* argv[])
{
	printf("Descriprion: BMP 24 and 32 bit filters:\nnegative, median, fastMedian, gauss(3,5,...)\nsobelX, sobelY, sobelXY\nscharrX, scharrY, scharrXY\ngrey.\n\n");
	if (argc != 4) return printf("wrong input");
	FILE* in, * out;
	if (!(in = fopen(argv[1], "rb"))) return printf("file <in> wasnt opened");
	struct first_header header;
	second_header sec_header;

	fread(header.name, 2, 1, in);
	fread(&header.size, 3 * sizeof(int), 1, in);
	fread(&sec_header.header_size, sizeof(second_header), 1, in);

	int row = sec_header.widht * sec_header.bits_per_pixel / 8;

	unsigned char** image = (unsigned char**)malloc(sec_header.height * sizeof(unsigned char*));

	printf("<in> file short information\n height x widht:%dx%d\nbit depth: %d\n\n", sec_header.height, sec_header.widht, sec_header.bits_per_pixel);

	if (sec_header.compression != 0) return printf("Your Bmp file is compessed, returning.\n");

	for (int i = 0; i < sec_header.height; i++)
	{
		image[i] = (unsigned char*)malloc((row + 1) * sizeof(unsigned char));
		fread(image[i], sizeof(unsigned char), row * sizeof(unsigned char), in);
	}
	fclose(in);

	if (!(out = fopen(argv[3], "wb"))) return printf("file <out> wasnt opened");
	fwrite(header.name, 2, 1, out);
	fwrite(&header.size, 3 * sizeof(int), 1, out);
	fwrite(&sec_header, sizeof(second_header), 1, out);

	int filter = get_filter(argv);

	if (filter == -1)
		printf("input error: no such filter");
	else if (filter == 0) median(image, sec_header);
	else if (filter == 1) fast_median(image, sec_header);
	else if (filter == 2) gauss(image, sec_header, 3, 1);

	else if (filter >= 3 && filter <= 8) sobel_sharr(image, sec_header, filter);

	else if (filter == 9) grey(image, sec_header);

	for (int i = 0; i < sec_header.height; i++)
		fwrite(image[i], sizeof(unsigned char), row * sizeof(unsigned char), out);
	fclose(out);

	printf("Completed");
	return 0;
}