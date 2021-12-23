#include "longarithmetic.h"

int main()
{
	printf("This program shows 3^5000 in hexadecimal representation using long arithmetics alorithms.\n\n");
	
	longNumber number = longPow(3, 5000);
	longPrint(&number);
	longFree(&number);

	return 0;
}