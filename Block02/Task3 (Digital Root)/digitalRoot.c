#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>


int main()
{
	const int maxN = 1000000;
	int mdrs = 0;
	int* resMem = (int*)malloc(sizeof(int) * maxN);

	printf("The programm calculates the sum of digital root's max multiplier in a range [2; 999999]\n");

	for (int i = 2; i < 10; i++)
	{
		resMem[i] = i;
	}

	for (int i = 10; i < maxN; i++)
	{
		int buffer = i;
		int base = 10;
		while (base >= 10)
		{
			base = 0;
			while (buffer > 0)
			{
				base += buffer % 10;
				buffer = buffer / 10;
			}
			buffer = base;
		}
		resMem[i] = base;

		for (int k = 2; k * k <= i; k++)
		{
			if (i % k == 0)
			{
				if (resMem[i] < (resMem[i / k] + resMem[k]))
				{
					resMem[i] = resMem[i / k] + resMem[k];
				}
			}
		}
		mdrs += resMem[i];
	}
	
	for (int i = 2; i < 10; i++)
	{

		mdrs += i;
	}

	printf("\nThe sum of digital root's max multiplier equals to %d\n", mdrs);
	free(resMem);
	return 0;
}