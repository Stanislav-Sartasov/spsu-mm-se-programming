#include <stdio.h>
#include <stdint.h>
#include <math.h>

int main()
{
	printf("This program prints three binary representations of the product of the lenghts of\nmy name, ");
	printf("surname and patronymic in below mentioned formats.\n");
	printf("Negative 32-bit integer number whose absolute value is equal to the product:\n");
	int product, first_product, i, exp;
	float second_product;
	double third_product;
	// Михаил Выродов Владимирович
	product = 6 * 7 * 12;
	first_product = ~(product) + 0b1;
	printf("%d", 1);
	for (i = 31 - 1; i >= 0; --i) 
	{
		printf("%d", (first_product >> i) & 1);
	}
	printf("\nSingle precision positive float number according to the IEEE754 standart:\n");
	second_product = (float) product;
	exp = 0;
	while (second_product >= 2)
	{
		second_product /= 2;
		++exp;
	}
	exp += 127;
	printf("0");
	for (i = 8 - 1; i >= 0; --i)
	{
		printf("%d", (exp >> i) & 1);
	}
	second_product -= 1;
	for (i = -1; i >= -23; --i)
	{
		if (second_product - pow(2, i) >= 0)
		{
			printf("%d", 1);
			second_product -= pow(2, i);
		}
		else
		{
			printf("%d", 0);
		}
	}
	printf("\nDouble precision negative float number according to the IEEE754 standart:\n");
	third_product = (double) product;
	exp = 0;
	while (third_product >= 2)
	{
		third_product /= 2;
		++exp;
	}
	exp += 1023;
	printf("1");
	for (i = 11 - 1; i >= 0; --i)
	{
		printf("%d", (exp >> i) & 1);
	}
	third_product -= 1;
	for (i = -1; i >= -52; --i)
	{
		if (third_product - pow(2, i) >= 0)
		{
			printf("%d", 1);
			third_product -= pow(2, i);
		}
		else
		{
			printf("%d", 0);
		}
	}
	return 0;
}