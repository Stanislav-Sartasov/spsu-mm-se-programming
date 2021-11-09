#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <math.h>
#include <stdlib.h>

int digital_root(int num)
{
	int sum_digits = 0;
	while (num > 0)
	{
		sum_digits += num % 10;
		num /= 10;
	}
	if (sum_digits >= 10)
	{
		digital_root(sum_digits);
	}
	else
		return sum_digits;
}

int main()
{
	printf("The digital root is a decimal number obtained from the digits of the original number by adding them and repeating\nthis process over the resulting sum until a number less than 10 is obtained.\n");
	printf("Let us denote the maximum sum of digital roots among all factorizations of the number n as MDRS (n).\n");
	printf("This program calculates the sum of all MDRS (n) with n = [2; 999999]\n");
	int num, max_root, answer, j;
	int* factor = (int*)malloc(sizeof(int) * 999999);
	answer = 0;

	for (num = 2; num < 10; ++num)
	{
		factor[num - 1] = num;
		answer += factor[num - 1];
	}

	for (num = 10; num < 1000000; ++num)
	{
		max_root = digital_root(num);
		for (j = 2; j < (int)(sqrt(num)) + 1; ++j)
		{
			if (num % j == 0 && factor[j - 1] + factor[(num / j) - 1] > max_root)
			{
				max_root = factor[j - 1] + factor[(num / j) - 1];
			}
		}
		factor[num - 1] = max_root;
		answer += factor[num - 1];
	}

	printf("Answer: %d", answer);
	free(factor);

	return 0;
}