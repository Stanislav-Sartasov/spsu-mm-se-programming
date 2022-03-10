#include <stdio.h>

int max(int a, int b)
{
	return (a > b) ? a : b;
}

int digital_root(int number)
{
	return (number % 9) ? (number % 9) : 9;
}

int mdrs(int number, int* arr)
{
	arr[number] = digital_root(number);
	for (int multiplier = 2; multiplier * multiplier <= number; multiplier++)
		if (number % multiplier == 0)
			arr[number] = max(arr[number], arr[multiplier] + arr[number / multiplier]);
	return arr[number];
}

int main()
{
	printf("MDRS(n) is the maximum sum of digital roots "
		"among all factorizations of the number n.\n");
	printf("This program outputs sum of MDRS(n) for n in range[2;999999]:\n");
	int* all_mdrs = (int*)malloc(sizeof(int) * 1000000);
	int sum_mdrs = 0;
	for (int n = 2; n <= 999999; n++)
		sum_mdrs += mdrs(n, all_mdrs);
	free(all_mdrs);
	printf("%d\n", sum_mdrs);
	return 0;
}