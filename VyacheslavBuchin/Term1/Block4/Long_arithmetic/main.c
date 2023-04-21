#include <stdio.h>

#include "big_int.h"

int main() {
	printf("This program computes 3^5000 in HEX\n");

	big_int_t* number228 = big_int_by_value(3);
	big_int_t* number337;

	for (int i = 1; i < 5000; i++)
	{
		number337 = big_int_by_value(0);
		big_int_add(number337, number228);
		big_int_add(big_int_add(number228, number337), number337);
		big_int_free(number337);
	}

	printf("%s\n", big_int_to_string(number228));

	big_int_free(number228);

	return 0;
}
