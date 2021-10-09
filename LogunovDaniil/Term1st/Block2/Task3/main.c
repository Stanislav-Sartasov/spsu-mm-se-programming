#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>

#define REM_SIZE 9
#define MAX_DIV 1000

void greetingsMessage()
{
	printf("This program is designed to calculate the sum of MDRS(n) function\n");
	printf(" for all n in range [2, 999999].\n");
}

int main()
{
	greetingsMessage();
	int mdrsSum = 0;
	int remainder[REM_SIZE];

	int isPrime[MAX_DIV];
	memset(isPrime, 1, sizeof(int) * MAX_DIV);
	isPrime[0] = 0;
	isPrime[1] = 0;
	for (int i = 0; i < MAX_DIV; i++)
	{
		if (isPrime[i])
		{
			int j = 2 * i;
			while (j < MAX_DIV)
			{
				isPrime[j] = 0;
				j += i;
			}
		}
	}

	for (int i = 2; i < 1000000; i++)
	{
		int n = i;

		// counting digital roots of prime decomposition of n
		memset(remainder, 0, REM_SIZE * sizeof(int));
		int divisor = 1;
		for (int div = 1; div<MAX_DIV;div++)
		{
			if (!isPrime[div])
				continue;
			while (n % div == 0)
			{
				n /= div;
				remainder[div % REM_SIZE]++;
			}
		}
		if (n > 1)
			remainder[n % REM_SIZE]++;

		// DRS increases greatly by muptiplying two primes with digital sums of 3
		mdrsSum += 9 * (remainder[3] / 2);
		remainder[3] %= 2;

		// the other ways to increase DRS is by multiplying 2 and 4, or three 2s,
		// and then 2 and 3
		int drs2and4 = min(remainder[2], remainder[4]);
		mdrsSum += 8 * drs2and4;
		remainder[2] -= drs2and4;
		remainder[4] -= drs2and4;

		mdrsSum += 8 * (remainder[2] / 3);
		remainder[2] %= 3;

		int drs2and3 = min(remainder[2], remainder[3]);
		mdrsSum += 6 * drs2and3;
		remainder[2] -= drs2and3;
		remainder[3] -= drs2and3;

		// adding the rest of primes to the MDRS
		// remainder[0] equals 0 since no prime can be divisible by 9
		for (int i = 1; i < 9; i++)
		{
			mdrsSum += remainder[i] * i;
		}
	}
	printf("The sum of all MDRS(n) values is %d.\n\n", mdrsSum);

	return 0;
}