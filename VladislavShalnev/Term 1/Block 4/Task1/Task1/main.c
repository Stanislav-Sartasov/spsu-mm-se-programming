#include <stdio.h>

#include "bigint/bigint.h"

int main()
{
	printf("This program calculates 3^5000 using algorithms of long arithmetics and displays it in hex format.\n\n");

	printf("Result:\n");

	bigint_t* bigint = new_bigint(1);

	for (int i = 0; i < 5000; i++)
		multiply_bigint(bigint, 3);

	print_hex_bigint(bigint);

	free_bigint(bigint);

	return 0;
}