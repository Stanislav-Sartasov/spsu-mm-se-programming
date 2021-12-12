#include "myPow.h"

int main()
{
	int numb = 3, exp = 5000;
	hex result;
	printf("This program uses long arithmetic algorithms"
		" to calculate the value of %d^%d and represent"
		" it in hexadecimal notation:\n\n", numb, exp);
	result = myHexPow(numb, exp);
	printNumber(result);
	return 0;
}