#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include "long_arithmetic.h"

int main()
{
	printf("Calculates 3 to the power of 5000:\n\n");

	bit_integer* number = power(3, 5000);

	print_long_num(number);

	free_int(number);

	return 0;
}