//
// Created by Вячеслав Бучин on 26.11.2021.
//

#include "filter.h"
#include "kernel/kernel.h"

typedef unsigned char byte;

double sum(double a, double b)
{
	return a + b;
}

double max(double a, double b)
{
	return a > b ? a : b;
}

void filter_apply_grayscale(bmp_t* image)
{
	int bytes_per_pixel = image->header.bits_per_pixel / 8;
	for (int i = 0; i < image->header.height; i++)
	{
		byte* pixel = image->content[i];
		for (int j = 0; j < image->header.width * bytes_per_pixel; j += bytes_per_pixel)
		{
			int gray_value = (pixel[j] + pixel[j + 1] + pixel[j + 2]) / 3;
			pixel[j] = pixel[j + 1] = pixel[j + 2] = gray_value;
		}
	}
}

void filter_apply_avg(bmp_t* image)
{
	double ninth = 1.0 / 9;
	double kernel[3][3] = {
			{ninth, ninth, ninth },
			{ninth, ninth, ninth },
			{ninth, ninth, ninth }
	};
	apply_kernel(image, kernel, sum);
}

void filter_apply_gaussian(bmp_t* image)
{
	double eighth = 1.0 / 8;
	double sixteenth = 1.0 / 16;
	double fourth = 1.0 / 4;
	double kernel[3][3] = {
			{ sixteenth, 	eighth, 	sixteenth 	},
			{ eighth, 		fourth, 	eighth 		},
			{ sixteenth, 	eighth, 	fourth 		}
	};
	apply_kernel(image, kernel, sum);
}

void filter_apply_sobelX(bmp_t* image)
{
	double kernel[3][3] = {
			{ -1, -2, -1 },
			{ 0, 0, 0 },
			{ 1, 2, 1 }
	};
	apply_kernel(image, kernel, sum);
}

void filter_apply_sobelY(bmp_t* image)
{
	double kernel[3][3] = {
			{ -1, 0, 1 },
			{ -2, 0, 2 },
			{ -1, 0, 1 }
	};
	apply_kernel(image, kernel, sum);
}

void filter_apply_max(bmp_t* image)
{
	double kernel[3][3] = {
			{ 1, 1, 1 },
			{ 1, 1, 1 },
			{ 1, 1, 1 }
	};
	apply_kernel(image, kernel, max);
}
