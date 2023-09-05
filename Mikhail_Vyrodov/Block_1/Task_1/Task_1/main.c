#include <stdio.h>
#include <math.h>

int prime_check(int a)
{
    int i, koren;
	if (a <= 1)
	{
		return 0;
	}
	else if (a == 2)
	{
		return 1;
	}
	else
	{
		koren = (int) sqrt(a) + 1;
		for (i = 2; i <= koren; ++i )
		{
			if (a % i == 0)
			{
				return 0;
			}
		}
		return 1;
	}
}

int main()
{
	printf("This program prints Mersenne prime numbers if they are greater than 1 and less than (2^31 - 1)\n");
	int i, chislo_mersena;
	for (i = 1; i <= 31; ++i)
	{
		chislo_mersena = (int) pow(2, i) - 1;
		if (prime_check(chislo_mersena))
		{
			printf("%d ", chislo_mersena);
		}
	}
	return 0;
}