//Homework 2.3
//Digital root

#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <stdbool.h>

#define MAX 999999
#define MIN 2

int result_of_sum(int* array);
int mdrs(int number, int* array);
int digital_root(int number);

int main()
{
	printf("A digital root is a number in decimal notation obtained ");
	printf("from the digits of the original number by adding them and\n");
	printf("repeating this process over the resulting sum until a number ");
	printf("less than 10. For example, the digital root 467 is 8.\nLet's ");
	printf("denote the maximum sum of digital roots among all factorizations ");
	printf("of the number n as MDRS (n).\nCalculate the sum of all MDRS (n)");
	printf("with n = [2; 999999].\n\n");

	int* array = (int*)malloc(MAX * sizeof(int));

	printf("\nCalculate the sum of all MDRS (n) with n = [2; 999999] is:");
	printf(" %d\n\n", result_of_sum(array));

	free(array);

	return 0;
}

int result_of_sum(int* array)
{
	int number, sum = 0;

	for (number = MIN; number <= MAX; number++)
	{
		sum = sum + mdrs(number, array);
	}

	return sum;
}

int mdrs(int number, int* array)
{
	int i;
	array[number] = digital_root(number);
	int sqrt_number = (int)sqrt(number) + 1;

	for (i = MIN; i < sqrt_number; i++)
	{
		if (f1(number, i, array))
		{
			*(array + number) = *(array + number);
		}
		else if (!(f1(number, i, array)))
		{
			*(array + number) = *(array + i) + *(array + (number / i));
		}
	}

	return array[number];
}

int digital_root(int number)
{
	if (number % 9)
	{
		return number % 9;
	}
	else
	{
		return 9;
	}
}

int f1(int number, int i, int* array)
{
	if (number % i == 0 && *(array + number) > *(array + i) + *(array + (number / i)))
	{
		return true;
	}
	else if (number % i == 0 && *(array + number) <= *(array + i) + *(array + (number / i)))
	{
		return false;
	}
}