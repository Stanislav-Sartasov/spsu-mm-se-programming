#include <stdio.h>
#include <math.h>

void clear_input()
{
	char step;
	step = '0';
	while (step != '\n' && step != EOF)
	{
		step = getchar();
	}
}

void get_number(long long* x)
{
	printf("Enter 1 natural number.\n");
	while (1)
	{
		if (scanf_s("%lld", x))
		{
			if (*x <= 0)
			{
				printf("This number is less than one. ");
			}
			else if (getchar() == '\n')
			{
				break;
			}
		}
		printf("Input was incorrect, please try again:\n");
		clear_input();
	}
}


int main()
{
	long long sum, j;
	int i;
	printf("This program calculates the number of ways to pay the entered amount of money in pence\nusing 1p, 2p, ");
	printf("5p, 10p, 20p, 50p, one pound, two pounds coins.\n");
	get_number(&sum);
	/* «аметим, что кол - во способов выплатить сумму m с помощью n монет равно
	1) ≈сли m больше или равно номиналу наибольшей монеты, кол-ву способов выплатить m с помощью всех монет, кроме наибольшей
	+ кол-во способов выплатить (m - номинал наибольшей монеты) всеми монетами;
	2) ≈сли m меньше номинала наибольшей монеты, кол-ву способов выплатить m с помощью всех монет, кроме наибольшей.
	—оздадим двумерный массив (ways) размера (9 * sum + 1). ƒл€ того чтобы подсчЄт был более удобным, мы добавили
	1 столбец и 1 строку в двумерный массив, первый столбец заполним единицами, а первую строку - ноликами, кроме нулевого элемента.
	¬ элемент ways[i][j] запишем кол-во способов выплатить сумму j с помощью i-первых монет. */
	long long coins[] = { 1, 2, 5, 10, 20, 50, 100, 200 };
	long long** ways = (long long**) malloc(9 * sizeof(long long*));
	for (i = 0; i < 9; i++) 
	{
		ways[i] = (long long*) malloc((sum + 1) * sizeof(long long));
	}
	for (i = 0; i < 9; ++i)
	{
		ways[i][0] = 1;
	}
	for (j = 1; j <= sum; ++j)
	{
		ways[0][j] = 0;
	}
	for (i = 1; i <= 8; ++i)
	{
		for (j = 1; j < sum + 1; ++j)
		{
			if (coins[i - 1] <= j)
			{
					ways[i][j] = ways[i - 1][j] + ways[i][j - coins[i - 1]];
			}
			else
			{
				ways[i][j] = ways[i - 1][j];
			}
		}
	}
	printf("%lld", ways[8][sum]);
	return 0;
}