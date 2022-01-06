#include <stdio.h> 
#include <math.h>

int is_simple(int number)
{
	int i = 0;
	for (i = 2; i < pow(number, 0.5); ++i)
	{
		if (number % i == 0) 
			return 0;
	}
	if (number == 1)
	{
		return 0;
	}
	return 1;
}

int main()
{
	printf("\033[33mMersenne numbers on the segment [1; 2^31-1]:\n");
	int n;
	n = 1;
	for (int i = 0; i < 31; i++)
	{
		n = n * 2;
		if (is_simple(n - 1))
		{
			printf("%d\n", n - 1);
		}
	}
	return 0;
}
