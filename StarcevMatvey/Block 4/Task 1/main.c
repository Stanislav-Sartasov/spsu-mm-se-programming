#define _CRT_SECURE_NO_WARNINGS
#define BASE 4096
#include <stdio.h>
#include <stdlib.h>

struct numb
{
	int size;
	int* numbers;
};
typedef struct numb number;

struct number* create_number(int size, int value)
{
	number* a = (number*)malloc(sizeof(number));
	a->size = size;
	a->numbers = (int*)calloc(a->size, sizeof(int));

	int k = 0;
	while (value)
	{
		a->numbers[k] = value % BASE;
		value = value / BASE;
		k++;
	}

	return a;
}

void del_number(number* a)
{
	free(a->numbers);
	free(a);
}

void normalize(number* a)
{
	if (a->numbers[a->size - 1] >= BASE)
	{
		a->size += 4;
		a->numbers = (int*)realloc(a->numbers, (a->size * sizeof(int)));
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

void printf_number(number* a)
{
	int k = a->size - 1;
	while (!(a->numbers[k]))
	{
		k--;
	}

	printf("0x%X", a->numbers[k]);
	for (int i = k - 1; i > -1; i--)
	{
		printf("%03X", a->numbers[i]);
	}
}

struct number* multy(number* a, number* b)
{
	number* rezult = create_number(a->size + b->size, 0);

	for (int i = 0; i < a->size; i++)
	{
		for (int j = 0; j < b->size; j++)
		{
			rezult->numbers[i + j] += a->numbers[i] * b->numbers[j];
		}
	}

	normalize(rezult);

	return rezult;
}

struct number* power(number* a, int pow)
{
	if (pow == 1)
	{
		return a;
	}

	number* b = power(a, pow / 2);

	if (pow % 2)
	{
		return multy(multy(b, b), a);
	}

	return multy(b, b);
}

int main()
{
	printf("Calculates 3 to the power of 5000\n");

	number* a = create_number(2, 3);

	a = power(a, 5000);

	printf_number(a);

	del_number(a);

	return 0;
}