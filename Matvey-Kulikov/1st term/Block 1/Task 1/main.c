#include <stdio.h>
#include <math.h>

int is_prime(int num)
{
	if (num < 2)
	{
	return 0;
	}
	for (int i = 2; i < floor(sqrt(num)) + 1; i++)
	{
		if (num % i == 0)
		{
			return 0;
		}
	}
	return 1;
}



int main()
{
	printf("This program outputs Mersenne primes from 1 to 2^31 - 1\n");
	for (int i = 1; i < 31; i++)
	{
		int mersenne_num = (2 << i) - 1;
		if (is_prime(mersenne_num))
		{
			printf("%d\n", mersenne_num);
		}
	}

}
