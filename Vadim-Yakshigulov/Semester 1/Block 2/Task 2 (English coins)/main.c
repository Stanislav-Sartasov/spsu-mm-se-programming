#include <stdio.h>

void input(char string[], int *a)
{
	int result;
	do
	{
		printf(string);
		result = scanf("%d", a);
		fflush(stdin);
	}
	while (!(result == 1 && (0 < *a <= 30000)));
}

int main()
{
	int values[] = {1, 2, 5, 10, 20, 50, 100, 200};
	int len = 8;
	int maxsize = 30000;

	unsigned long long d[len][maxsize];
	for (int i = 0; i < len; ++i)
		for (int j = 0; j < maxsize; ++j)
			d[i][j] = 0;
	d[0][0] = 1;

	for (int i = 0; i < maxsize; ++i)
		for (int j = 0; j < len; ++j)
			for (int k = j; k < len; ++k)
				if (i + values[k] <= maxsize)
					d[k][i + values[k]] += d[j][i];

	int n;
	printf("This program displays the number of ways in which you can dial the entered number of coins, if there is a nominal value 1, 2, 5, 10, 20, 50, 100 and 200 pence.\n");
	input("Please enter one positive number <= 30000:\n", &n);

	unsigned long long ans = 0;
	for (int i = 0; i < len; ++i)
		ans += d[i][n];

	printf("There is a %llu way(s) to dial the entered number of coins", (ans > 0) ? ans : 1);
	return 0;
}