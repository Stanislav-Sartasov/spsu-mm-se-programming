#include <stdio.h>
#include <math.h>
#include <stdlib.h>

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
	/* �������, ��� ��� - �� �������� ��������� ����� m � ������� n ����� �����
	1) ���� m ������ ��� ����� �������� ���������� ������, ���-�� �������� ��������� m � ������� ���� �����, ����� ����������
	+ ���-�� �������� ��������� (m - ������� ���������� ������) ����� ��������;
	2) ���� m ������ �������� ���������� ������, ���-�� �������� ��������� m � ������� ���� �����, ����� ����������.
	�������� ��������� ������ (ways) ������� (9 * sum + 1). ��� ���� ����� ������� ��� ����� �������, �� ��������
	1 ������� � 1 ������ � ��������� ������, ������ ������� �������� ���������, � ������ ������ - ��������, ����� �������� ��������.
	� ������� ways[i][j] ������� ���-�� �������� ��������� ����� j � ������� i-������ �����. */
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
	for (i = 0; i < 9; ++i)
	{
		free(ways[i]);
	}
	free(ways);
	return 0;
}