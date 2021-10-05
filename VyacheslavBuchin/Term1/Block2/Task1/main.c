#include <stdio.h>
#include <stdint.h>

void printBinary(int64_t number, int bits)
{
	for (int i = 0; i < bits; i++)
	{
		printf("%lld", (number >> (bits - 1 - i)) & 1);
	}
}

int main()
{
	int product = 5 * 8 * 9;
	printf("This program prints different binary representations of numbers.\n\n");
	printf("The product of the lengths of the first name, last name, patronymic is 5 * 8 * 9 = %d.\n", product);
	printf("Binary representation of %d:\n", -product);
	printBinary(-product, 32);
	printf("\n\n");

	float product_f = (float) product;
	printf("Standard IEEE 754 binary representation of floating point number %.1f:\n", product_f);
	printBinary(*(int32_t *) &product_f, 32);
	printf("\n\n");

	double product_lf = (double) -product;
	printf("Standard IEEE 754 binary representation of double-precision floating point number %.1lf:\n", product_lf);
	printBinary(*(int64_t *) &product_lf, 64);
	printf("\n\n");

	return 0;
}
