#include <stdio.h>
#include <string.h>

void print_binary(long long int num, int position)
{
	for (int i = 0; i < position; i++)
	{
		printf("%lld", (num >> (position - (i + 1))) & 1);
	}
}

int exponent_order_float(int num)
{
	long long order = -1;
	while (num > 0)
	{
		num = num / 10;
		order += 1;
	}
	return (order + 127);
}

int exponent_order_double(int num)
{
	long long order = -1;
	while (num > 0)
	{
		num = num / 10;
		order += 1;
	}
	return (order + 1023);
}

int binary_number(int num)
{
	int bin = 0;
	int k = 1;
	while (num > 0)
	{
		bin += ((int)num % 2) * k;
		k *= 10;
		num /= 2;
	}
	return bin;
}

const char* new_str(const char* string)
{
	return string + 1;
}

int main()
{
	int product = 5 * 9 * 10;
	printf("The program displays a binary representation of the product of the lengths of my first name, last name and patronymic.\n\n");
	printf("The product of the lengths of the first name, last name, patronymic is 5 * 9 * 12 = %d \n", product);
	printf("Binary representation of %d:\n", -product);
	print_binary(~(product - 1), 32);
	printf("\n\n");

	char plus_sing[33] = "0";
	char order_float[9];
	char mantissa_float[24];
	char null[] = "0";
	_itoa(exponent_order_float(binary_number(product)), order_float, 2);
	_itoa(product, mantissa_float, 2);
	printf("Standard IEEE 754 binary representation of floating point number:\n");
	printf("%s%s%s", plus_sing, order_float, new_str(mantissa_float));
	int i = 0;
	while (i < (23 - (exponent_order_float(binary_number(product)) - 127)))
	{
		printf("%s", null);
		i++;
	}
	printf("\n\n");

	char minus_sing[65] = "1";
	char order_double[12];
	char mantissa_double[53];
	_itoa(exponent_order_double(binary_number(product)), order_double, 2);
	_itoa(product, mantissa_double, 2);
	printf("Standard IEEE 754 binary representation of double-precision floating point number:\n");
	printf("%s%s%s", minus_sing, order_double, new_str(mantissa_double));
	int j = 0;
	while (j < (52 - (exponent_order_double(binary_number(product)) - 1023)))
	{
		printf("%s", null);
		j++;
	}
	return 0;
}