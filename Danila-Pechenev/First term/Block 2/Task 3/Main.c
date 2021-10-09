#include <stdio.h>
#include <stdlib.h>
#include <math.h>


int digital_root(int n)
{
	int remainder = n % 9;
	if (remainder)
	{
		return remainder;
	}
	else
	{
		return 9;
	}
}


int mdrs(int n, int* results)
{
	int sq_root = sqrt(n);
	int answer = digital_root(n);
	for (int d = 2; d <= sq_root; d++)
	{
		if (n % d == 0)
		{
			if (results[n / d - 2])
			{
				answer = max(answer, digital_root(d) + results[n / d - 2]);
			}
			else
			{
				answer = max(answer, digital_root(d) + mdrs(n / d, results));
			}
		}
	}
	results[n - 2] = answer;
	return answer;
}


int main()
{
	printf("This program calculates and displays the sum of all MDRS(n) for n in the range [2; 999999].\n");

	int sum_of_mdrses = 0;
	int* results = (int*)malloc((999998) * sizeof(int));

	for (int i = 0; i <= (999997); i++)
	{
		results[i] = 0;
	}

	for (int n = 2; n <= 999999; n++)
	{
		sum_of_mdrses += mdrs(n, results);
	}

	printf("Result: %d.\n", sum_of_mdrses);

	free(results);
}