#include <stdio.h>
#include <stdlib.h>

int digitalRoot(int x);

int main()
{
	printf("This program displays a sum of MDRS from segment [2; 999999]\n\n");
	
	int* MDRS = (int*)malloc(1000000 * sizeof(int));
	int sum = 0;

	for (int i = 2; i < 10; i++)
	{
		MDRS[i] = i;
		sum += MDRS[i];
	}

	for (int i = 10; i < 1000000; i++)
	{
		MDRS[i] = digitalRoot(i);

		for (int j = 2; j * j <= i; j++)
		{
			if (i % j == 0)
				if (MDRS[j] + MDRS[i / j] > MDRS[i])
					MDRS[i] = MDRS[j] + MDRS[i / j];
		}

		sum += MDRS[i];
	}
	
	free(MDRS);

	printf("Resust is %d\n", sum);

	return 0;
}

int digitalRoot(int x)
{
	int sum = 10;
	while (sum >= 10)
	{
		sum = 0;
		while (x > 0)
		{
			sum += x % 10;
			x /= 10;
		}
		x = sum;
	}
	return sum;
}