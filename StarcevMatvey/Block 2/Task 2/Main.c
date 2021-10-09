#include <stdio.h>
#include "inputAndTools.h"

#define CUSTOM_TYPE unsigned long long

int main()
{
	int ranks[] = { 1, 2, 5, 10, 20, 50, 100, 200 };

	printf("Calculates the number of ways to issue the sum in coins\n");

	// input
	int s;
	do
	{
		printf("Input your summ (greater than zero)\n");
		s = get_int();
		s++;
	} while (s <= 0);

	// array of ways
	CUSTOM_TYPE** f = (CUSTOM_TYPE**)malloc(8 * sizeof(CUSTOM_TYPE*));
	for (int i = 0; i < 8; i++)
	{
		f[i] = (CUSTOM_TYPE*)malloc((CUSTOM_TYPE)(s * sizeof(CUSTOM_TYPE)));
	}

	for (int i = 0; i < 8; i++)
	{
		for (int j = 0; j < 4; j++)
		{
			if (j < 2)
			{
				f[i][j] = 1;
			}
			else
			{
				f[i][j] = 0;
			}
		}
	}

	for (int sum = 2; sum < s; sum++)
	{
		for (int i = 0; i < 8; i++)
		{
			if (i == 0)
			{
				f[i][sum] = 1;
			}
			else if (i == 1)
			{
				f[i][sum] = sum / 2 + 1;
			}
			else
			{
				int k = i;
				f[k][sum] = 1;
				for (int j = k; j > 0; j--)
				{
					if (sum - ranks[j] >= 0)
					{
						f[k][sum] += f[j][sum - ranks[j]];
					}
				}
			}
		}
	}

	printf("Total ways: %lld\n", f[7][s - 1]);

	// memory free
	for (int i = 0; i < 8; i++)
	{
		free(f[i]);
	}
	free(f);

	return 0;
}