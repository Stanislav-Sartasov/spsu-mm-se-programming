#include <stdio.h>
#include <stdlib.h>

int main()
{
	printf("This program displays a number of ways to dial\nsome amount with british coins \n\n");
	
	int coins[8] = { 1, 2, 5, 10, 20, 50, 100, 200 };
	int usersSum;

	printf("Enter an integer ammount: ");
	while (!scanf_s("%d", &usersSum) || usersSum < 0)
	{
		scanf_s("%*[^\n]");
		printf("Input error.\n\nEnter an integer ammount: ");
	}
	printf("\n");

	unsigned long long** counter = (unsigned long long**)malloc(8 * sizeof(unsigned long long*));
	for (int i = 0; i < 8; i++) 
	{
		counter[i] = (unsigned long long*)malloc((usersSum + 1) * sizeof(unsigned long long));
	}

	for (int i = 0; i < 8; i++)
	{
		for (int j = 0; j < usersSum + 1; j++)
		{
			counter[i][0] = 1;
			counter[0][j] = 1;
		}
	}
	
	for (int i = 1; i < 8; i++)
	{
		for (int j = 1; j < usersSum + 1; j++)
		{
			if (j < coins[i])
				counter[i][j] = counter[i - 1][j];
			else
				counter[i][j] = counter[i - 1][j] + counter[i][j - coins[i]];
		}
	}

	printf("You can dial this amount in %lld ways\n\n", counter[7][usersSum]);

	for (int i = 0; i < 8; i++)
	{
		free(counter[i]);
	}
	free(counter);

	return 0;
}