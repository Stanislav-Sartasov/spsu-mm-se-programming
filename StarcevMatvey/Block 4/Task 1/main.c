#define _CRT_SECURE_NO_WARNINGS
#define BASE 16
#include <stdio.h>
#include <stdlib.h>

struct l_n
{
	int size;
	unsigned char* numbers;
};
typedef struct l_n light_number;

struct h_n
{
	int size;
	int* numbers;
};
typedef struct h_n heavy_number;

// size must be bigger than log(16, value) + 1
struct light_number* create_light_number(int size, int value)
{
	light_number* a = (light_number*)malloc(sizeof(light_number));
	a->size = size;
	a->numbers = (unsigned char*)malloc(a->size * sizeof(unsigned char));

	int k = 0;
	while (value)
	{
		a->numbers[k] = value % BASE;
		value = value / BASE;
		k++;
	}
	for (int i = k; i < a->size; i++)
	{
		a->numbers[i] = 0;
	}

	return a;
}

void normalize(heavy_number* a)
{
	if (a->numbers[a->size - 1] >= BASE)
	{
		a->size += 4;
		a->numbers = realloc(a->numbers, (a->size * sizeof(int)));
		for (int i = 1; i < 5; i++)
		{
			a->numbers[a->size - i] = 0;
		}
	}

	for (int i = 0; i < a->size - 1; i++)
	{
		if (a->numbers[i] >= BASE)
		{
			a->numbers[i + 1] += a->numbers[i] / BASE;
			a->numbers[i] = a->numbers[i] % BASE;
		}
	}
}

void printf_number(light_number* a)
{
	int k = a->size - 1;
	while (!(a->numbers[k]))
	{
		k--;
	}

	printf("0x");
	for (int i = k; i > -1; i--)
	{
		printf("%X", a->numbers[i]);
	}
}

struct light_number* multy(light_number* a, light_number* b)
{
	heavy_number* heavy_rezult = (heavy_number*)malloc(sizeof(heavy_number));
	heavy_rezult->size = a->size + b->size;
	heavy_rezult->numbers = (int*)malloc(heavy_rezult->size * sizeof(int));

	for (int i = 0; i < heavy_rezult->size; i++)
	{
		heavy_rezult->numbers[i] = 0;
	}

	for (int i = 0; i < a->size; i++)
	{
		for (int j = 0; j < b->size; j++)
		{
			heavy_rezult->numbers[i + j] += a->numbers[i] * b->numbers[j];
		}
	}

	normalize(heavy_rezult);

	int k = heavy_rezult->size - 1;
	while (!(heavy_rezult->numbers[k]))
	{
		k--;
	}
	k = (k + 4) & (-4);

	light_number* light_rezult = create_light_number(k, 0);

	for (int i = 0; i < light_rezult->size; i++)
	{
		light_rezult->numbers[i] = (unsigned char)(heavy_rezult->numbers[i]);
	}

	free(heavy_rezult->numbers);
	free(heavy_rezult);

	return light_rezult;
}

struct light_number* power(light_number* a, int pow)
{
	if (pow == 1)
	{
		return a;
	}

	light_number* b = power(a, pow / 2);

	if (pow % 2)
	{
		return multy(multy(b, b), a);
	}

	return multy(b, b);
}

int main()
{
	printf("Calculates 3 to the power of 5000\n");
	
	light_number* a = create_light_number(4, 3);

	a = power(a, 5000);

	printf_number(a);

	free(a->numbers);
	free(a);
	return 0;
}
