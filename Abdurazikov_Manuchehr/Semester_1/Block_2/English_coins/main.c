#include <stdio.h>

int british_coins(int add, int length);

long long int iterator;

void stdin_num(void);

int get_number(int* digit, const int end);

int main()
{
	int start = 0;
	if (iterator == 0)
	{
		printf("This program displays the number of ways in which you can dial the entered number using any number of English coins\n");
	}
	iterator = 0;
	while (printf("Please enter a number >>>") && get_number(&start, 100000000) || start <= 0)
	{
		fprintf(stderr, "Error!  ( use int nubmers, > 0 and < 10000000) \n");
		stdin_num();
	}
	british_coins(start, 8);
	printf("%lld", iterator);
	int answer;
	while (printf("\nAnother number? (0 - no, 1 - yes)>>>") && get_number(&answer, 10) || answer < 0 || answer > 1)
	{
		fprintf(stderr, "Wrong input! ( use '0' or '1' )\n");
		stdin_num();
	}
	if (answer)
	{
		main();
	}
	else
	{
		printf("The program has been successfully completed!!!");
		return 0;
	}
}

void stdin_num(void)
{
	char ch = " ";
	while (scanf("%c", &ch) == 1 && ch != '\n')
	{
	}
}

int get_number(int* digit, const int end)
{
	return !(scanf("%d", digit) == 1 && *digit < end);
}


int british_coins(int beg, int length)
{
	if (beg == 0)
	{
		iterator++;
	}
	else
	{
		int i;
		for (i = length - 1; i >= 1; i--)
		{
			int m[] = { 1, 2, 5, 10, 20, 50, 100, 200 };
			if (i == 1)
			{
				if (beg % 2 == 0)
				{
					iterator += beg / 2 + 1;
				}
				else
				{
					iterator += 1.5 + beg / 2;
				}
			}
			else if (beg == 1)
			{
				iterator++;
			}
			else if (beg - m[i] >= 0)
			{
				british_coins(beg - m[i], i + 1);
			}
		}
	}
	return 0;
}