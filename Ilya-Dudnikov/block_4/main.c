#include <stdio.h>
#include "big_integer.h"

int main()
{
	printf("This program prints value of the number 3^5000 in hexadecimal form\n");

	big_int number;
	set_value(&number, 3);

	number = big_int_power(&number, 5000);
	char *result = big_int_to_hexadecimal(&number);
	printf("3^5000 = %s\n", result);
	return 0;
}