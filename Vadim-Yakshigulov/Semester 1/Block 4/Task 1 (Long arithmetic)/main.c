#include "big_int.h"

int main()
{
	printf("This program prints 3^5000 in hex\n");
	big_int_t *num = big_int_by_value(3);
	big_int_t *res = big_int_by_value(1);

	for (int i = 0; i < 5000; i++)
		res = big_int_mul(res, num);

	big_int_print_hex(*res);
	big_int_free(num);
	big_int_free(res);
}