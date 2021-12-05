#include <stdio.h>
#include <math.h>

int digital_root(int n);

int mdrs(int n, int* results);

int main()
{
	printf("The digital root is a decimal number obtained from the digits of the original number by adding them and repeating\nthis process over the resulting sum until a number less than 10 is obtained.\n");
	printf("Let us denote the maximum sum of digital roots among all factorizations of the number n as MDRS (n).\n");
	printf("This program calculates the sum of all MDRS (n) with n = [2; 999999]\n");
	int answer = 0;
	int* results = (int*)malloc(sizeof(int)*1000000);
	for (int i = 2; i <= 999999; i++)
	{
		answer += mdrs(i, results);
	}
	printf("Answer: %d", answer);
	free(results);
	return 0;
}

int digital_root(int n)
{
	return n % 9 ? n % 9 : 9;
}

int mdrs(int n, int* results)
{
	int sqrt_root = (int)pow(n, 0.5) + 1;
	results[n] = digital_root(n);
	for (int i = 2; i < sqrt_root; i++)
	{
		if (n % i == 0)
		{
			results[n] = results[n] > results[i] + results[n / i] ? results[n] : results[i] + results[n / i];
		}
	}
	int ans = results[n];
	return ans;
}