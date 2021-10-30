#include <stdio.h>
#include <math.h>
#include <stdlib.h>

int digitRoot(int num)
{
	int digitSum = 0;
	while (num > 0) 
	{
		digitSum += num % 10;
		num /= 10;
	}
	return digitSum;
}

int mdrs(int num, int* arrMdrs)
{
	int maxMdrs;
	int sqrtNum = (int)sqrt(num) + 1;
	int digRootNum = digitRoot(num);
	if (digRootNum % 9)
	{
		arrMdrs[num] = digRootNum % 9;
	}
	else
	{
		arrMdrs[num] = 9;
	}
	for (int i = 2; i < sqrtNum; i++)
	{
		if (num % i == 0)
		{
			arrMdrs[num] = max(arrMdrs[num], arrMdrs[i] + arrMdrs[num / i]);
		}
	}
	maxMdrs = arrMdrs[num];
	return maxMdrs;
}

int main()
{
	printf("The digital root is a decimal number obtained from the digits of the original number by adding them and repeating \nthis process over the resulting sum until a number less than 10 is obtained.\n");
	printf("Let us denote the maximum sum of digital roots among all factorizations of the number n as MDRS (n).\n");
	printf("This program calculates the sum of all MDRS (n) with n = [2; 999999]\n");
	int result = 0;
	int* arrMdrs = (int*)malloc(sizeof(int) * 1000000);
	for (int i = 2; i < 1000000; i++)
	{
		result += mdrs(i, arrMdrs);
	}
	printf("Sum of all MDRS (n): %d", result);
	free(arrMdrs);
	return 0;
}
