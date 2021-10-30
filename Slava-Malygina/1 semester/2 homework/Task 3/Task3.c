#include <stdio.h>
#include <stdlib.h>


int main()
{
	int mdrs = 0;
	int* table = (int*)malloc(sizeof(int) * 1000000);
	if (table == NULL)
		printf("Failed to allocate memory\n");
	printf("This program calculates the sum of all maximum sums of digital roots among all decompositions of each number in the range [2; 999999].\n\n");

	for (int i = 2; i < 10; i++)
	{
		table[i] = i;
	}

	for (int i = 10; i <= 999999; i++)
	{
		int buffer = i;
		int n = 10;
		while (n >= 10)
		{
			n = 0;
			while (buffer > 0)
			{
				n += buffer % 10;
				buffer = buffer / 10;
			}
			buffer = n;
		}
		table[i] = n;

		for (int j = 2; j * j <= i; j++)
		{
			if (i % j == 0)
				if (table[i] < (table[i / j] + table[j]))
					table[i] = table[i / j] + table[j];
		}
		mdrs += table[i];
	}

	for (int i = 2; i < 10; i++)
	{
		mdrs += i;
	}

	printf("Sum of maximum digital roots sum is: %d\n", mdrs);
	free(table);
	return 0;
}