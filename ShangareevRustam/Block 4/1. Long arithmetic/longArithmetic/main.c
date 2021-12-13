#include "myPow.h"

int main()
{
	int numb = 3, exp = 5000;
	base256 result;
	printf("This program uses long arithmetic algorithms"
		" to calculate the value of %d^%d and represent"
		" it in hexadecimal notation:\n\n", numb, exp);
	result = my256NumPow(numb, exp);
	printHexNumber(result);
	return 0;
}