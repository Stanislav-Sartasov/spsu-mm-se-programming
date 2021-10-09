#include <stdio.h>

int digitalRoot(int number);

int main()
{
	int* digitalRoots = (int*)malloc(1000000 * sizeof(int));
	int sum = 0;
	printf("This program calculates the maximum amount of digital roots of fractorization of numbers in range [2, 999999]\n\n");

	for (int i = 2; i < 10; i++)
	{
		digitalRoots[i] = digitalRoot(i);
		sum += digitalRoots[i];
	}

	for (int i = 10; i < 1000000; i++)
	{
		digitalRoots[i] = digitalRoot(i);
		for (int j = 2; j * j <= i; j++)
		{
			if (i % j == 0)
			{
				digitalRoots[i] = (digitalRoots[i] > (digitalRoots[i / j] + digitalRoots[j]) ? digitalRoots[i] : (digitalRoots[i / j] + digitalRoots[j]));
			}
		}
		sum += digitalRoots[i];
	}

	free(digitalRoots);
	printf("MDRS = %d\n", sum);
	return 0;
}

int digitalRoot(int number)
{
	number = number % 9;
	if (number)
		return number;
	return 9;
}