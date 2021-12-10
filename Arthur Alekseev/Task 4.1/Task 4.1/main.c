#include "long_int.h"

int main() {
	// Formal section
	printf("This program uses long arithmetics to count 3 to the power of 5000 (3^5000)\n\nHere is the result: ");

	// Code for 3 ^ 5000
	struct my_long_int* number = power_int(3, 5000);
	print_long_num(number);
	free_int(number);

	return 0;
}