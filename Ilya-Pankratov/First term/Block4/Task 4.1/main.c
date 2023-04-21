#include <stdio.h>
#include <stdlib.h>
#include "long_arithmetic.h"

#define BASE 65536

int main(int argc, char* argv[])
{
	printf("This program calculates 3^(5000)\n16-digit number notation of 3^(5000):\n");
	long_number* three = create_number(1, 3, BASE);
	long_number* result = long_power(three, 5000);
	print_long_number(result);
	free_number(three);
	free_number(result);
	return 0;
}