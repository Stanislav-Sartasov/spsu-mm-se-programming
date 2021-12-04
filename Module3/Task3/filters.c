#include"Header.h"
void swap(unsigned char* a, unsigned char* b)
{
    unsigned char temp = *a;
    *a = *b;
    *b = temp;
}

void filterMedian(int width, int height, struct RGBTRIPLE** rgb_arr, struct RGBTRIPLE** new_arr)
{
    for (int i = 1; i < height - 3; i++)
        for (int j = 1; j < width - 3; j++)
        {
            applyMedianToPixels(rgb_arr, new_arr, j, i);
        }
}

void applyMedianToPixels(struct RGBTRIPLE** arr, struct RGBTRIPLE** new_arr, int idx, int idy)
{
    unsigned char arr_g[9], arr_b[9], arr_r[9];

    for (int i = 0; i < 9; i++)
    {
        struct RGBTRIPLE* temp_rgb = &arr[idy - 1 + i / 3][idx - 1 + i % 3];

        arr_b[i] = temp_rgb->b;
        arr_g[i] = temp_rgb->g;
        arr_r[i] = temp_rgb->r;
    }

    for (int j = 0; j < 9; j++)
        for (int i = 0; i < 9; i++)
        {
            if (arr_b[i] > arr_b[j])
                swap(&arr_b[j], &arr_b[i]);

            if (arr_g[i] > arr_g[j])
                swap(&arr_g[j], &arr_g[i]);

            if (arr_r[i] > arr_r[j])
                swap(&arr_r[j], &arr_r[i]);
        }

    new_arr[idy][idx].r = arr_r[4];
    new_arr[idy][idx].b = arr_b[4];
    new_arr[idy][idx].g = arr_g[4];

}

//Black and White
void filterBlackandWhite(int width, int height, struct RGBTRIPLE** rgb_arr, struct RGBTRIPLE** new_arr)
{
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            unsigned char d = (unsigned char)((rgb_arr[i][j].g + rgb_arr[i][j].b + rgb_arr[i][j].r)/3);

            new_arr[i][j].b = d;
            new_arr[i][j].g = d;
            new_arr[i][j].r = d;
        }
    }
}

//filter gauss 3*3 and 5*5
const int gauss_matrix5x5[5][5] = { {1, 4,  6,  4,  1},
                                   {4, 16, 24, 16, 4},
                                   {6, 24, 36, 24, 6},
                                   {4, 16, 24, 16, 4},
                                   {1, 4,  6,  4,  1} };

const int gauss_matrix3x3[3][3] = { {1, 2, 1},
                                   {2, 4, 2},
                                   {1, 2, 1} };

void filterGauss(int width, int height, struct RGBTRIPLE** rgb_arr, struct RGBTRIPLE** new_arr, int size)
{
    for (int i = size / 2; i < height - size; i++)
        for (int j = size / 2; j < width - size; j++)
        {
            applyGaussToPixels(rgb_arr, new_arr, j, i, size);
        }
}

void applyGaussToPixels(struct RGBTRIPLE** arr, struct RGBTRIPLE** new_arr, int idx, int idy, int size)
{
    int sum_g = 0, sum_b = 0, sum_r = 0;
    int div = size < 4 ? 16 : 256;

    if (size ==3)
    {
        for (int i = 0; i < size * size; i++)
        {
            struct RGBTRIPLE* temp_rgb = &arr[idy - 1 + i / size][idx - 1 + i % size];

            sum_b += temp_rgb->b * gauss_matrix3x3[i / size][i % size];
            sum_g += temp_rgb->g * gauss_matrix3x3[i / size][i % size];
            sum_r += temp_rgb->r * gauss_matrix3x3[i / size][i % size];
        }
    }
    else
    {
        for (int i = 0; i < size * size; i++)
        {
            struct RGBTRIPLE* temp_rgb = &arr[idy - 2 + i / size][idx - 2 + i % size];

            sum_b += temp_rgb->b * gauss_matrix5x5[i / size][i % size];
            sum_g += temp_rgb->g * gauss_matrix5x5[i / size][i % size];
            sum_r += temp_rgb->r * gauss_matrix5x5[i / size][i % size];
        }
    }

    new_arr[idy][idx].r = (unsigned char)(sum_r / div);
    new_arr[idy][idx].b = (unsigned char)(sum_b / div);
    new_arr[idy][idx].g = (unsigned char)(sum_g / div);
}

const int sobelx_matrix[3][3] = { {1,  2,  1},
                                 {0,  0,  0},
                                 {-1, -2, -1} };

const int sobely_matrix[3][3] = { {-1, 0, 1},
                                 {-2, 0, 2},
                                 {-1, 0, 1} };


void filterSobelX(int width, int height, struct RGBTRIPLE** rgb_arr, struct RGBTRIPLE** new_arr)
{
    for (int i = 1; i < height - 3; i++)
        for (int j = 1; j < width - 3; j++)
        {
            applySobelXToPixels(rgb_arr, new_arr, j, i);
        }
}

void applySobelXToPixels(struct RGBTRIPLE** arr, struct RGBTRIPLE** new_arr, int idx, int idy)
{
    int x_sum_g = 0, x_sum_b = 0, x_sum_r = 0;

    for (int i = 0; i < 9; i++)
    {
        struct RGBTRIPLE* temp_rgb = &arr[idy - 1 + i / 3][idx - 1 + i % 3];

        x_sum_b += temp_rgb->b * sobelx_matrix[i / 3][i % 3];
        x_sum_g += temp_rgb->g * sobelx_matrix[i / 3][i % 3];
        x_sum_r += temp_rgb->r * sobelx_matrix[i / 3][i % 3];
    }

    if (x_sum_r < 0) x_sum_r = 0;
    if (x_sum_b < 0) x_sum_b = 0;
    if (x_sum_g < 0) x_sum_g = 0;

    if (x_sum_b > 255) x_sum_b = 255;
    if (x_sum_g > 255) x_sum_g = 255;
    if (x_sum_r > 255) x_sum_r = 255;

    new_arr[idy][idx].r = (unsigned char)(x_sum_r);
    new_arr[idy][idx].b = (unsigned char)(x_sum_b);
    new_arr[idy][idx].g = (unsigned char)(x_sum_g);
}

void filterSobelY(int width, int height, struct RGBTRIPLE** rgb_arr, struct RGBTRIPLE** new_arr)
{
    for (int i = 1; i < height - 3; i++)
        for (int j = 1; j < width - 3; j++)
        {
            applySobelYToPixels(rgb_arr, new_arr, j, i);
        }
}

void applySobelYToPixels(struct RGBTRIPLE** arr, struct RGBTRIPLE** new_arr, int idx, int idy)
{
    int y_sum_g = 0, y_sum_b = 0, y_sum_r = 0;

    for (int i = 0; i < 9; i++)
    {
        struct RGBTRIPLE* temp_rgb = &arr[idy - 1 + i / 3][idx - 1 + i % 3];

        y_sum_b += temp_rgb->b * sobely_matrix[i / 3][i % 3];
        y_sum_g += temp_rgb->g * sobely_matrix[i / 3][i % 3];
        y_sum_r += temp_rgb->r * sobely_matrix[i / 3][i % 3];
    }

    if (y_sum_r < 0) y_sum_r = 0;
    if (y_sum_b < 0) y_sum_b = 0;
    if (y_sum_g < 0) y_sum_g = 0;

    if (y_sum_b > 255) y_sum_b = 255;
    if (y_sum_g > 255) y_sum_g = 255;
    if (y_sum_r > 255) y_sum_r = 255;

    new_arr[idy][idx].r = (unsigned char)(y_sum_r);
    new_arr[idy][idx].b = (unsigned char)(y_sum_b);
    new_arr[idy][idx].g = (unsigned char)(y_sum_g);
}

