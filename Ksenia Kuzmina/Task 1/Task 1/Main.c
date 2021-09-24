#include <stdio.h> 
#include <math.h>

int is_prime(int n)
{
	int i = 0;
	for (i = 2; i < pow(n, 0.5); ++i)
	{
		if (n % i == 0) 
			return 0;
	}
	if (n == 1)
	{
		return 0;
	}
	return 1;
}

int main()
{
	printf("This program outputs Mersenne numbers.\n");
	int n;
	n = 1;
	for (int i = 0; i < 31; i++)
	{
		n = n * 2;
		if (is_prime(n - 1))
		{
			printf("%ld\n", n - 1);
		}
	}
	
}