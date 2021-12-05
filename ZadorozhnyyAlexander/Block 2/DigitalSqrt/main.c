#include <stdio.h>
#include <stdlib.h>
#include <math.h>

int sum_symbols_in_digit(int n)
{
	return n % 9 != 0 ? n % 9 : 9;
}

int mdrs(int n, int* results)
{
	results[n - 2] = sum_symbols_in_digit(n);
	for (int d = 2; d <= sqrt(n); d++)
	{
		if (n % d == 0)
			results[n - 2] = max(results[n - 2], sum_symbols_in_digit(d) + results[n / d - 2]);
	}
	return results[n - 2];
}

int main()
{
	printf("This program calculates and show the sum of all MDRS(n) for n in [2; 999999].\n");

	int sum = 0;
	int* table_results = (int*)malloc(999998 * sizeof(int));

	for (int i = 0; i < 999998; i++)
	{
		table_results[i] = 0;
	}

	for (int n = 2; n < 1000000; n++)
	{
		sum += mdrs(n, table_results);
	}

	printf("Result is: %d.\n", sum);
	free(table_results);
}