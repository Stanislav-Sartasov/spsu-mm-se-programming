#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <math.h>

int sum_of_digits(int n)
{
	return n % 9 == 0 ? 9 : n % 9;
}

int mdrs(int num, int* massive)
{
	massive[num] = sum_of_digits(num);

	for (int del = 2; del <= sqrt(num); del++)
	{
		if (num % del == 0)
			massive[num] = fmax(massive[num], massive[del] + massive[num / del]);
	}

	return massive[num];
}

int main()
{
	printf("The program calculates the sums of all MDRS(n) for n = [2; 999999]\n\n");

	int total_summ = 0;
	int* sums = (int*)malloc(1000000 * sizeof(int));

	for (int i = 2; i <= 999999; i++)
	{
		total_summ += mdrs(i, sums);
	}

	printf("Maximum sum: %d\n", total_summ);

	free(sums);

	return 0;
}