#include <stdio.h>
#include <stdlib.h>
#include <math.h>

int mdrs(int n, int* results)
{
	results[n - 2] = n % 9 != 0 ? n % 9 : 9;
	for (int d = 2; d <= sqrt(n); d++)
	{
		if (n % d == 0)
			results[n - 2] = fmax(results[n - 2], (d % 9 != 0 ? d % 9 : 9) + results[n / d - 2]);
	}
	return results[n - 2];
}

int main()
{
	printf("This program prints the sum of MDRS(n) for n in [2; 999999]\n");
	int sum = 0;
	int* results = (int*)malloc(999998 * sizeof(int));
	for (int i = 0; i < 999998; i++)
	{
		results[i] = 0;
	}
	for (int n = 2; n < 1000000; n++)
	{
		sum += mdrs(n, results);
	}
	printf("Result: %d\n", sum);
	free(results);
}