
#include <stdio.h>

#define MASK_32 0x80000000
#define MASK_64 0x8000000000000000

#define SURNAME "Shalnev"
#define NAME "Vladislav"
#define PATRONYMIC "Aleksandrovich"


void print_bin(long long number, int size, long long mask)
{
	for (int i = 0; i < size; i++)
	{
		printf("%d", number & mask ? 1 : 0);
		number <<= 1;
	}
	printf("\n");
}

void print_bin_int(int number)
{
	// 2 ^ (32 - 1) = 0x80000000
	print_bin(number, 32, MASK_32);
}

void print_bin_long_long(long long number)
{
	// 2 ^ (64 - 1) = 0x8000000000000000
	print_bin(number, 64, MASK_64);
}

void print_bin_float(float number)
{
	print_bin_int(*(int*)&number);
}

void print_bin_double(double number)
{
	print_bin_long_long(*(long long*)&number);
}

int main()
{
	printf("This program prints composition of lengths of ");
	printf("surname %s, name %s and patronymic %s in:\n", SURNAME, NAME, PATRONYMIC);
	printf("1) Negative binary int32\n");
	printf("2) Positive binary float IEEE754 standart\n");
	printf("3) Negative binary double IEEE754 standart\n\n");

	int composition = strlen(NAME) * strlen(SURNAME) * strlen(PATRONYMIC);

	printf("Composition of surname, name and patronymic lengths: %d\n\n", composition);

	printf("1) Binary int32 representation of %d:\n", -composition);
	print_bin_int(-composition);

	printf("\n2) Binary float IEEE754 representation of %f:\n", (float)composition);
	print_bin_float(composition);

	printf("\n3) Binary double IEEE754 representation of %lf:\n", -(double)composition);
	print_bin_double(-composition);

	return 0;
}
