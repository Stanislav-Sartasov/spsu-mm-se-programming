#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <locale.h>
#include <string.h>
#include <malloc.h>

struct bitmap
{
	int size;
	int width;
	int height;
	int bias_bits_count;
	short bit_planes_count;
	short bit_count;
	int colors_used_count;
	int colors_significant_count;
};

struct bitmap get_bitmap(FILE* file)
{
	struct bitmap bmp;

	fseek(file, 2, SEEK_SET);
	fread(&(bmp.size), sizeof(int), 1, file);

	fseek(file, 10, SEEK_SET);
	fread(&(bmp.bias_bits_count), sizeof(int), 1, file);

	fseek(file, 18, SEEK_SET);
	fread(&(bmp.width), sizeof(int), 1, file);

	fseek(file, 22, SEEK_SET);
	fread(&(bmp.height), sizeof(int), 1, file);

	fseek(file, 26, SEEK_SET);
	fread(&(bmp.bit_planes_count), sizeof(short int), 1, file);

	fseek(file, 28, SEEK_SET);
	fread(&(bmp.bit_count), sizeof(short int), 1, file);

	fseek(file, 46, SEEK_SET);
	fread(&(bmp.colors_used_count), sizeof(int), 1, file);

	fseek(file, 50, SEEK_SET);
	fread(&(bmp.colors_significant_count), sizeof(int), 1, file);

	return bmp;
}

int number_comparator(const void* p1, const void* p2)
{
	return (*(int*)p1 - *(int*)p2);
}

int abs(int x)
{
	return x > 0 ? x : -x;
}

void gray(struct bitmap bmp_info, unsigned char*** picture, int row_size, int foramen)
{
	for (int i = 0; i < bmp_info.height; ++i)
	{
		for (int j = 0; j < row_size; j += foramen)
		{
			// Colors are stored in BGR order
			unsigned char color = (unsigned char)(int)(0.11 * (*picture)[i][j] +
				0.59 * (*picture)[i][j + 1] +
				0.3 * (*picture)[i][j + 2]);
			(*picture)[i][j] = color;
			(*picture)[i][j + 1] = color;
			(*picture)[i][j + 2] = color;
		}
	}
}

void gauss(struct bitmap bmp_info, unsigned char*** picture, int row_size, int foramen)
{
	double gauss_ratios[3] = { 0.25, 0.125, 0.0625 };
	for (int channel = 0; channel < 3; ++channel)
	{
		for (int i = 1; i < bmp_info.height - 1; ++i)
		{
			for (int j = foramen * 1; j < row_size - foramen * 1; j += foramen)
			{
				double result = 0;
				for (int n = -1; n < 2; ++n)
				{
					for (int m = -1; m < 2; ++m)
					{
						double ratio;
						if (m == 0 && n == 0)
						{
							ratio = gauss_ratios[0];
						}
						else if (abs(m) && abs(n))
						{
							ratio = gauss_ratios[2];
						}
						else
						{
							ratio = gauss_ratios[1];
						}
						result += ((*picture)[i + n][j + channel + foramen * m] * ratio);
					}
				}
				(*picture)[i][j + channel] = (unsigned char)result;
			}
		}
	}
}

void sobel(struct bitmap bmp_info, unsigned char*** picture, int row_size, int foramen, int type)
{
	gray(bmp_info, picture, row_size, foramen);
	double sobel_y_ratios[3][3] = { {-1, -2, -1},
										 {0,  0,  0},
										 {1,  2,  1} };
	double sobel_x_ratios[3][3] = { {-1, 0, 1},
										 {-2, 0, 2},
										 {-1, 0, 1} };
	const int sobel_edge_limit = 70;

	unsigned char** picture_copy = (unsigned char**)malloc(bmp_info.height * sizeof(unsigned char*));
	for (int i = 0; i < bmp_info.height; ++i)
	{
		picture_copy[i] = (unsigned char*)malloc((bmp_info.width) * sizeof(unsigned char));
	}


	for (int i = 1; i < bmp_info.height - 1; ++i)
	{
		for (int j = foramen * 1; j < row_size - foramen * 1; j += foramen)
		{
			int result = 0;
			for (int n = -1; n < 2; ++n)
			{
				for (int m = -1; m < 2; ++m)
				{
					if (type)
					{
						result += ((*picture)[i + n][j + foramen * m] * sobel_y_ratios[n + 1][m + 1]);
					}
					else
					{
						result += ((*picture)[i + n][j + foramen * m] * sobel_x_ratios[n + 1][m + 1]);
					}
				}
			}
			picture_copy[i][j / foramen] = abs(result) < sobel_edge_limit ? 0 : 255;
		}
	}


	for (int channel = 0; channel < 3; ++channel)
	{
		for (int i = 1; i < bmp_info.height - 1; ++i)
		{
			for (int j = foramen * 1; j < row_size - foramen * 1; j += foramen)
			{
				(*picture)[i][j + channel] = picture_copy[i][j / foramen];
			}
		}
	}

	for (int i = 0; i < bmp_info.height; ++i)
	{
		free(picture_copy[i]);
	}
	free(picture_copy);
}

void median(struct bitmap bmp_info, unsigned char*** picture, int row_size, int foramen)
{
	for (int channel = 0; channel < 3; ++channel)
	{
		for (int i = 1; i < bmp_info.height - 1; ++i)
		{
			for (int j = foramen * 1; j < row_size - foramen * 1; j += foramen)
			{
				int near_colors[9];
				for (int n = -1; n < 2; ++n)
				{
					for (int m = -1; m < 2; ++m)
					{
						near_colors[(n + 1) * 3 + m + 1] = (*picture)[i + n][j + channel + foramen * m];
					}
				}
				qsort(near_colors, 9, sizeof(int), number_comparator);
				(*picture)[i][j + channel] = near_colors[4];
			}
		}
	}
}


int main(int argc, char* argv[])
{
	printf("This program applies the following filters to images:\n");
	printf("3x3 Gaussian filter, median filter, gray filter, Sobel(X) filter and Sobel(Y) filter\n");

	if (argc != 4)
	{
		printf("Invalid number of arguments. \nFormat: <program> <input_file_name> <filter_name> <output file name>\n");
		return -1;
	}

	if (strcmp(argv[2], "median") != 0 && strcmp(argv[2], "gauss") != 0 && strcmp(argv[2], "sobelx") != 0 &&
		strcmp(argv[2], "sobely") != 0 && strcmp(argv[2], "gray") != 0)
	{
		printf("The introduced filter does not apply to the list of available ones. Try again.");
		return -1;
	}

	FILE* origin_image = fopen(argv[1], "rb");
	if (origin_image == NULL)
	{
		printf("The input file was not found.");
		return -1;
	}

	FILE* purpose_image = fopen(argv[3], "wb");
	if (purpose_image == NULL)
	{
		printf("Could not open output file.");
		return -1;
	}

	struct bitmap bmp_info = get_bitmap(origin_image);

	unsigned char** picture = (unsigned char**)malloc(bmp_info.height * sizeof(unsigned char*));
	int line_size, foramen;
	if (bmp_info.bit_count == 24)
	{
		line_size = 3 * bmp_info.width + bmp_info.width % 4;
		foramen = 3;
	}
	else if (bmp_info.bit_count == 32)
	{
		line_size = 4 * bmp_info.width;
		foramen = 4;
	}
	else
	{
		printf("Unsupported BMP file bitness.");
		return -1;
	}

	fseek(origin_image, 0, SEEK_SET);
	char head_information[54];
	fread(head_information, sizeof(unsigned char), 54 * sizeof(unsigned char), origin_image);
	fseek(origin_image, 54, SEEK_SET);
	for (int i = 0; i < bmp_info.height; ++i)
	{
		picture[i] = (unsigned char*)malloc((line_size) * sizeof(unsigned char));
		fread(picture[i], sizeof(unsigned char), line_size * sizeof(unsigned char), origin_image);
	}
	fwrite(head_information, sizeof(unsigned char), 54, purpose_image);

	if (strcmp("gray", argv[2]) == 0)
	{
		gray(bmp_info, &picture, line_size, foramen);
	}
	else if (strcmp("median", argv[2]) == 0)
	{
		median(bmp_info, &picture, line_size, foramen);
	}
	else if (strcmp("gauss", argv[2]) == 0)
	{
		gauss(bmp_info, &picture, line_size, foramen);
	}
	else if (strcmp("sobelx", argv[2]) == 0)
	{
		sobel(bmp_info, &picture, line_size, foramen, 0);
	}
	else if (strcmp("sobely", argv[2]) == 0)
	{
		sobel(bmp_info, &picture, line_size, foramen, 1);
	}

	printf("The filter has been applied\n");

	for (int i = 0; i < bmp_info.height; ++i)
	{
		fwrite(picture[i], sizeof(unsigned char), line_size * sizeof(unsigned char), purpose_image);
	}

	for (int i = 0; i < bmp_info.height; ++i)
	{
		free(picture[i]);
	}
	free(picture);
	fclose(origin_image);
	fclose(purpose_image);
	return 0;
}