#include <stdio.h>
#include <math.h>
#include <stdlib.h>



int memory[999998];
int sumMDRS = 0;

void countMDRSFactors(int factor1, int factor2, int* maxMDRS)
{
	int MDRS;
	MDRS = memory[factor1 - 2] + memory[factor2 - 2];
	*maxMDRS = max(MDRS, *maxMDRS);
}

void countMaxMDRS1Num(int number, int* maxMDRS)
{
	int MDRS = 0;
	while (number > 0)
	{
		MDRS += number % 10;
		number /= 10;
	}
	if (MDRS > 9) countMaxMDRS1Num(MDRS, maxMDRS);
	else *maxMDRS = max(MDRS, *maxMDRS);
}

int main() 
{
	printf("The programm counts the sum of all maximum digital roots on the period [2, 999999].\n\n");
	int number;
	for (number = 2; number < 1000000; number++)
	{
		int maxMDRS = 0;
		countMaxMDRS1Num(number, &maxMDRS);
		int devider;
		for (devider = 2; devider < (int)sqrt(number) + 1; devider++)
		{
			if (number % devider == 0)
			{
				countMDRSFactors(number / devider, devider, &maxMDRS);
			}
		}
		memory[number - 2] = maxMDRS;
		sumMDRS += maxMDRS;
	}
	printf(">>>The sum of all maximum digital roots on the period [2, 999999] is %d.\n", sumMDRS);
	return 0;
}