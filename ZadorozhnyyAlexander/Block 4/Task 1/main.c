#include "long_arithmitics_functions.h"

int main()
{
	printf("This program calculates the value of 3^5000 using long arithmetic algorithms "
		"and represents it in hexadecimal notation.\n");
	long_aritcmetic_int* result = (long_aritcmetic_int*)malloc(sizeof(long_aritcmetic_int));
	init_new_int(result, 3, START_LIM_LENGHT);
	
	result = pow_long_numbers(result, 5000);
	printf("The result of 3^5000 = ");
	print_hexadecimal_long_numbers(result);

	free_long_numbers(result);
}
