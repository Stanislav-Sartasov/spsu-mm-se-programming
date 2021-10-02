#include <stdio.h>
#include <math.h>

void input(char string[], long long *a);

int main()
{
	long long d;
	printf("This program prints the continued fraction of square root of given natural number and it's period\n");
	input("Please enter natural number:\n", &d);
	unsigned long r = floor(sqrt(d));
	int k = 1;
	printf("[ %lu ", r);
	unsigned long a = r, p = 0, q = 1;
	do
	{
		p = a * q - p;
		q = (d - p * p) / q;
		a = (r + p) / q;
		printf("%lu ", a);
		k += 1;
	}
	while (q != 1);
	printf("]\nPeriod is: %d", k);
}

void input(char string[], long long *a)
{
	int result;
	do
	{
		printf(string);
		result = scanf("%lld", a);
		fflush(stdin);
	}
	while (!(result == 1 && *a > 0));
}