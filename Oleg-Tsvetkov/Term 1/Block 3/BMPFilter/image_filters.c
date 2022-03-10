#include "image_filters.h"

int number_comparator(const void *p1, const void *p2)
{
	return (*(int *) p1 - *(int *) p2);
}

int my_abs(int x)
{
	return x > 0 ? x : -x;
}

void apply_gray(struct bitmap_info bmp_info, unsigned char ***picture, int row_size, int channels)
{
	for (int i = 0; i < bmp_info.height; ++i)
	{
		for (int j = 0; j < row_size; j += channels)
		{
			// Colors are stored in BGR order
			unsigned char color = (unsigned char) (int) (GRAY_FILTER_BLUE_COEFFICIENT * (*picture)[i][j] +
														 GRAY_FILTER_GREEN_COEFFICIENT * (*picture)[i][j + 1] +
														 GRAY_FILTER_RED_COEFFICIENT * (*picture)[i][j + 2]);
			(*picture)[i][j] = color;
			(*picture)[i][j + 1] = color;
			(*picture)[i][j + 2] = color;
		}
	}
}

void apply_gauss_3x3(struct bitmap_info bmp_info, unsigned char ***picture, int row_size, int channels)
{
	//0 - для центрального пикселя, 1 - для контактирующих с центральным, 2 - для контактирующих по диагонали
	//То есть коэффициенты применяются так:
	//  212
	//  101
	//  212
	double gauss_coefficients[3] = {0.25, 0.125, 0.0625};
	for (int channel = 0; channel < 3; ++channel)
	{
		for (int i = 1; i < bmp_info.height - 1; ++i)
		{
			for (int j = channels * 1; j < row_size - channels * 1; j += channels)
			{
				double result = 0;
				for (int n = -1; n < 2; ++n)
				{
					for (int m = -1; m < 2; ++m)
					{
						double coefficient;
						if (m == 0 && n == 0)
						{
							coefficient = gauss_coefficients[0];
						}
						else if (my_abs(m) && my_abs(n))
						{
							coefficient = gauss_coefficients[2];
						}
						else
						{
							coefficient = gauss_coefficients[1];
						}
						result += ((*picture)[i + n][j + channel + channels * m] * coefficient);
					}
				}
				(*picture)[i][j + channel] = (unsigned char) result;
			}
		}
	}
}

void apply_sobel_3x3(struct bitmap_info bmp_info, unsigned char ***picture, int row_size, int channels, int type)
{
	apply_gray(bmp_info, picture, row_size, channels);
	double sobel_y_coefficients[3][3] = {{-1, -2, -1},
										 {0,  0,  0},
										 {1,  2,  1}};
	double sobel_x_coefficients[3][3] = {{-1, 0, 1},
										 {-2, 0, 2},
										 {-1, 0, 1}};
	const int sobel_edge_limit = 70;

	unsigned char **picture_copy = (unsigned char **) malloc(bmp_info.height * sizeof(unsigned char *));
	for (int i = 0; i < bmp_info.height; ++i)
	{
		picture_copy[i] = (unsigned char *) malloc((bmp_info.width) * sizeof(unsigned char));
	}


	for (int i = 1; i < bmp_info.height - 1; ++i)
	{
		for (int j = channels * 1; j < row_size - channels * 1; j += channels)
		{
			int result = 0;
			for (int n = -1; n < 2; ++n)
			{
				for (int m = -1; m < 2; ++m)
				{
					if (type)
					{
						result += ((*picture)[i + n][j + channels * m] * sobel_y_coefficients[n + 1][m + 1]);
					}
					else
					{
						result += ((*picture)[i + n][j + channels * m] * sobel_x_coefficients[n + 1][m + 1]);
					}
				}
			}
			picture_copy[i][j / channels] = my_abs(result) < sobel_edge_limit ? 0 : 255;
		}
	}


	for (int channel = 0; channel < 3; ++channel)
	{
		for (int i = 1; i < bmp_info.height - 1; ++i)
		{
			for (int j = channels * 1; j < row_size - channels * 1; j += channels)
			{
				(*picture)[i][j + channel] = picture_copy[i][j / channels];
			}
		}
	}

	for (int i = 0; i < bmp_info.height; ++i)
	{
		free(picture_copy[i]);
	}
	free(picture_copy);
}

void apply_median_3x3(struct bitmap_info bmp_info, unsigned char ***picture, int row_size, int channels)
{
	for (int channel = 0; channel < 3; ++channel)
	{
		for (int i = 1; i < bmp_info.height - 1; ++i)
		{
			for (int j = channels * 1; j < row_size - channels * 1; j += channels)
			{
				int near_colors[9];
				for (int n = -1; n < 2; ++n)
				{
					for (int m = -1; m < 2; ++m)
					{
						near_colors[(n + 1) * 3 + m + 1] = (*picture)[i + n][j + channel + channels * m];
					}
				}
				qsort(near_colors, 9, sizeof(int), number_comparator);
				(*picture)[i][j + channel] = near_colors[4];
			}
		}
	}
}