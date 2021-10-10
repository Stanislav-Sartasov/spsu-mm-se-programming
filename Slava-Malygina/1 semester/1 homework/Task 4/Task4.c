#include <stdio.h>
#include <math.h>
#include <ctype.h>


int main()
{
	long long number;
	char d;
	printf("This program takes a natural number and displays members and length of period of the continued fraction obtained from the square root of the received number. The entered number must not be a square of an integer. Enter a number:\n");
	scanf("%lld", &number);
	d = getchar();
	while (((sqrtl(number)) == (floorl(sqrtl(number)))) || isalpha(d) || number < 1)
	{
		printf("Invalid value. You must enter a natural number which is not a square of an integer. Please, re-enter: ");
		scanf("%lld", &number);
		d = getchar();
	}

	long long a = (floorl(sqrtl(number)));
	printf("[%lld; ", a);
	long long fractional = a, entire = fractional, first_fractional = number - (pow(fractional, 2));
	long long e = entire, f = first_fractional;
	long long period = 1;
	fractional = (a + entire) / first_fractional;
	entire = fractional * first_fractional - entire;
	first_fractional = (number - (pow(entire, 2))) / first_fractional;
	printf("%lld, ", fractional);
	while (first_fractional != f || entire != e)
	{
		fractional = (a + entire) / first_fractional;
		entire = fractional * first_fractional - entire;
		first_fractional = (number - (pow(entire, 2))) / first_fractional;
		if (period++ != 1)
			printf(", ");
		printf("%lld", fractional);
	}
	printf("]\n");
	printf("Period is %lld\n", period);
	return 0;
}