#include <stdio.h>
#include <stdlib.h>

int digital_root(int n)
{
	if (n % 9)
		return n % 9;
	return 9;
}

void count_root(int n, int* arr)
{
	arr[n] = digital_root(n + 2);

	// Going through all divisors of number
	for (int i = 2; i * i <= n + 2; i++)
		if ((n + 2) % i == 0)
			if (arr[n] < arr[(n + 2) / i - 2] + arr[i - 2])
				arr[n] = arr[(n + 2) / i - 2] + arr[i - 2];
}

int main()
{
	printf("This program counts the sum of max digital roots for numbers in range [2, 999999].\n\n");
	// Array or storing all the roots counted
	int* arr = (int*)malloc(sizeof(int) * 999998);
	int result = 0;
	// fill first elements for dynamic programming method (from 2 to 10)
	for (int i = 0; i < 8; i++)
		arr[i] = digital_root(i + 2);

	// Count and put all digital roots in an array
	for (int i = 8; i < 999998; i++)
		count_root(i, arr);

	// Count summ of all digital roots
	for (int i = 0; i < 999998; i++)
		result += arr[i];

	printf("The sum is: %d", result);
}


