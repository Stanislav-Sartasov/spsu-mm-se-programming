#include <stdio.h>

void print_float(int n)
{
	int exp = 127;
	int degree = 1;
	while ((degree << 1) <= n)
	{
		degree <<= 1;
		exp++;
	}
	printf("0");
	for (int i = 7; i >= 0; i--)
	{
		printf("%d", (exp >> i) & 1);
	}
	int left_border = exp - 127;
	for (int i = left_border - 1; i >= 0; i--)
	{
		printf("%d", (n >> i) & 1);
	}
	for (int i = 22 - left_border; i >= 0; i--)
	{
		printf("0");
	}
}

void print_double(int n)
{
	int exp = 1023;
	int degree = 1;
	while ((degree << 1) <= n)
	{
		degree <<= 1;
		exp++;
	}
	printf("1");
	for (int i = 10; i >= 0; i--)
	{
		printf("%d", (exp >> i) & 1);
	}
	int left_border = exp - 1023;
	for (int i = left_border - 1; i >= 0; i--)
	{
		printf("%d", (n >> i) & 1);
	}
	for (int i = 51 - left_border; i >= 0; i--)
	{
		printf("0");
	}
}

int main()
{
	printf("Representation of the number\n\nPrint binary representation of product my name, second name and patronymic\n");
	int product = (int) (sizeof("Ivan") - 1) * (int) (sizeof("Shurenkov") - 1) * (int) (sizeof("Andreevich") - 1);
	printf("A)Negative 32-bit integer: ");
	for (int i = 32; i >= 0; i--)
	{
		printf("%d", ((-product) >> i) & 1);
	}
	printf("\nB)Positive single-precision floating point number: ");
	print_float(product);
	printf("\nC)Negative double-precision floating point number: ");
	print_double(product);
	return 0;
}
