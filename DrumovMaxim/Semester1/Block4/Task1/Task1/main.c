#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include "bigint.h"


int main()
{
	printf("This program calculates 3^5000 using algorithms of long arithmetics and displays it in hex format.\n\n");
	printf("3^5000 in hex:\n\n");

	bigInt* number = newBigInt(1, 3);
	number = power(number, 5000);

	printfBigIntHex(number);
	freeBigInt(number);
	return 0;
}

