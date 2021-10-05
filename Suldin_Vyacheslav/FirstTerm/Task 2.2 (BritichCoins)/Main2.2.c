#include <stdio.h>

int coins(int summ, int length);

long long int count;

void flush_stdin(void)
{
	char ch = " ";
	while (scanf_s("%c", &ch) == 1 && ch != '\n')
	{
	}
}

int get_number(int* number, const int top)
{
	return !(scanf_s("%d", number) == 1 && *number < top);
}

int main()
{
	int sum = 0;
	if (count == 0)
	{
		printf("<< Descptiption: Enter a int number - summ of your coins, the program will output count of possible combinations using Britich coins>>>\n\n\n");
	}
	count = 0;
	while (printf("Enter summ>>") && get_number(&sum, 100000000) || sum <= 0)
	{
		fprintf(stderr, "Wrong input!  ( use int nubmers, > 0 and < 10^8 ) \n");
		flush_stdin();
	}
	coins(sum, 8);
	printf("%lld", count);
	int ans;
	while (printf("\nAnother summ? (0 - no, 1 - yes)>>>") && get_number(&ans, 10) || ans < 0 || ans > 1)
	{
		fprintf(stderr, "Wrong input! ( use '0' or '1' )\n");
		flush_stdin();
	}
	if (ans)
	{
		main();
	}
	else
	{
		printf("The program has completed<<<");
		return 0;
	}
}

int coins(int summ, int length)
{
	if (summ == 0)
	{
		count++;
	}
	else
	{
		int i;
		for (i = length - 1; i >= 1; i--)
		{
			int m[] = { 1, 2, 5, 10, 20, 50, 100, 200 };
			if (i == 1)
			{
				if (summ % 2 == 0)
				{
					count += summ / 2 + 1;
				}
				else
				{
					count += 1.5 + summ / 2;
				}
			}
			else if (summ == 1)
			{
				count++;
			}
			else if (summ - m[i] >= 0)
			{
				coins(summ - m[i], i + 1);
			}
		}
	}
	return 0;
}
