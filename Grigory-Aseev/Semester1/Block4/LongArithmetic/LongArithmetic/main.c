#include "big_library.h"

int main()
{
	printf("The program calculates the value of the number 3^5000 using long arithmetic algorithms and represents it in hexadecimal notation.\n");
	big_integer* number = power(3, 5000);
	printf_big_int_hex(number);
	delete_big_int(number);
	return 0;
}