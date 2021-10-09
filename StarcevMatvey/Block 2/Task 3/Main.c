#include <stdio.h>
#include <math.h>

int main()
{
	printf("Calculates the sum of all MDRS(n) for n from 1 to 999999\n");

	int s = 999999;
	int summa = 0;
	int* sums = (int*)malloc(1000000 * sizeof(int));

	for (int i = 2; i < 10; i++)
	{
		sums[i] = i;
		summa += sums[i];
	}

	for (int n = 10; n < s + 1; n++)
	{
		sums[n] = n % 9;
		for (int i = 2; i * i <= n + 2; i++)
		{
			if (n % i == 0)
			{
				if (sums[i] + sums[n / i] > sums[n])
				{
					sums[n] = sums[i] + sums[n / i];
				}
			}
		}
		summa += sums[n];
	}

	printf("The sum is: %d\n", summa);

	free(sums);

	return 0;
}