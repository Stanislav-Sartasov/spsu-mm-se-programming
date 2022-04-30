#include <stdio.h>
#include <stdbool.h>

union int_float
{
	float float_number;
	int integer_number;
};

union int_double
{
	double double_number;
	long long integer_number;
};

void print_bits(long long n, int bits)
{
	for (int i = bits - 1; i >= 0; i--)
	{
		bool current_bit = (n & (1LL << i));
		printf("%d", current_bit);
	}
	printf("\n\n");
}

int main()
{
	printf("This program prints binary representation of integer number, number with a floating point and number with a floating point with double precision\n");

	int product = 8 * 4 * 10;

	printf("Binary representation of %d:\n", -product);
	print_bits(-product, 32);

	/*
	 * Since union is a data type for storing multiple variables in the same memory space,
	 * we can store a float value in the first field (which is of type float or double) of the union and then
	 * output the values of its bits using the second field (of type int or long long) as
	 * it is much easier to find a binary representation of an integer number.
	 */

	union int_float float_product = {product};
	printf("IEEE 754 binary representation of a floating point number %d:\n", product);
	print_bits(float_product.integer_number, 32);

	union int_double double_product = {-product};
	printf("IEEE 754 binary representation of a double-precision floating point number %d:\n", -product);
	print_bits(double_product.integer_number, 64);

	return 0;
}