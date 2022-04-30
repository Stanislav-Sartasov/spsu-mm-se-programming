#include <stdio.h>
#include <stdlib.h>
#include "big_integer.h"

int main()
{
	printf("This program prints value of the number 3^5000 in hexadecimal form\n");

	big_int *tmp = set_value(3, 1);

	big_int *number = big_int_power(tmp, 5000);
	char *result = big_int_to_hexadecimal(number);
	printf("3^5000 = %s\n", result);
	delete_big_int(number);
	delete_big_int(tmp);
	free(result);
	return 0;
}