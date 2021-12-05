#include <stdio.h>

void print_negative_complement_binary(_int32 x)
{
	x -= 1;
	for (int i = 31; i >= 0; i--)
	{
		printf("%d", ((x >> i) % 2) ^ 1);
	}
	printf("\n");
}

void print_positive_float(float x)
{
	unsigned int int_x = (unsigned int)x;
	int counter_digits = 0;
	while (int_x)
	{
		counter_digits++;
		int_x /= 2;
	}
	int exp = 127 + counter_digits - 1, real = (unsigned int)x;
	printf("0");
	for (int i = 7; i >= 0; i--)
	{
		printf("%d", (exp >> i) % 2);
	}
	for (int i = counter_digits - 2; i >= 0; i--)
	{
		printf("%d", (real >> i) % 2);
	}
	for (int i = 23 - counter_digits; i >= 0; i--)
	{
		printf("0");
	}
	printf("\n");
}

void print_negative_double(double x)
{
	unsigned int int_x = (unsigned int)x;
	int counter_digits = 0;
	while (int_x)
	{
		counter_digits++;
		int_x /= 2;
	}
	int exp = 1023 + counter_digits - 1, real = (unsigned int)x;
	printf("1");
	for (int i = 10; i >= 0; i--)
	{
		printf("%d", (exp >> i) % 2);
	}
	for (int i = counter_digits - 2; i >= 0; i--)
	{
		printf("%d", (real >> i) % 2);
	}
	for (int i = 52 - counter_digits; i >= 0; i--)
	{
		printf("0");
	}
	printf("\n");
}

int main()
{
	printf("This program displays the product of the lengths of the first name, "
		"last name and patronymic as a binary number in various data formats.\n");
	_int32 name_length = 6, surname_length = 9, patronymic_length = 10,
		product_name_length = name_length * surname_length * patronymic_length;
	printf("Negative 32 bit integer whose absolute value is equal to the product: \n");
	print_negative_complement_binary(product_name_length);
	printf("Single precision positive floating point number according to the IEEE 754 standard, "
		"the absolute value of which is equal to the product: \n");
	print_positive_float((float)product_name_length);
	printf("Negative IEEE 754 double-precision floating-point number, "
		"the absolute value of which is equal to the product: \n");
	print_negative_double((double)product_name_length);
	return 0;
}
