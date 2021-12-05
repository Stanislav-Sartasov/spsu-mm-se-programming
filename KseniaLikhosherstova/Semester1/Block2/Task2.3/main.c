#include <stdio.h>
#include <stdlib.h>
#include <math.h>

int digital_root(int n)
{
	if (n % 9)
		return n % 9;
	return 9;
}

int mdrs(int n, int* result)
{
	int sqrt_root = (int)pow(n, 0.5);
	result[n] = digital_root(n);
	for (int i = 2; i <= sqrt_root; i++)
	{
		if (n % i == 0)
		{
			result[n] = max(result[n], result[i] + result[n / i]); 
		}
	}
	int answer = result[n];
	return answer;
}


int main()
{
	printf("This program calculates and outputs the sum of all MDRS for n in the range [2; 999999].");
	int answer = 0;
	int* result = (int*)malloc(1000000 * sizeof(int));
	for (int n = 2; n <= 999999; n++)
	{
		answer += mdrs(n, result);
	}
	printf("\nResult: %d", answer);
	free(result);

	return 0;
}