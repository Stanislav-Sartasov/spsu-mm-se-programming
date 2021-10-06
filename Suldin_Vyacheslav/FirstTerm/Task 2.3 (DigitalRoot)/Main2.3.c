#include <stdio.h>

int get_prime_d(int number);

int main()
{
	int j, count = 0, c = 0, n;

	printf("Description: The program calculates the sum of all the maximum digital roots of numbers on a given interval.\n");
	for (j = 2;j <= 999999; j++)
	{
		n = j;

		int drr[5] = { 0 };

		while (n % 2 == 0)
		{
			drr[2]++;
			n = n / 2;
		}

		int prime_div;

		while (n % 9 == 0)
		{
			count = count + 9;
			n = n / 9;
		}
		while (n > 1)
		{
			prime_div = get_prime_d(n);
			n = n / prime_div;
			if ((prime_div % 9 == 2 && drr[4] > 0) || (prime_div % 9 == 4 && drr[2] > 0))
			{
				count = count + 8;
				if (prime_div % 9 == 2)
				{
					drr[4]--;
				}
				else
				{
					drr[2]--;
				}
			}
			else if (prime_div % 9 == 2 || prime_div % 9 == 3 || prime_div % 9 == 4)
			{
				drr[prime_div % 9]++;
			}
			else
			{
				count = count + prime_div % 9;
			}
		}
		while (drr[2] >= 3)
		{
			drr[2] = drr[2] - 3;
			count = count + 8;
		}
		while (drr[3] > 0 && drr[2] > 0)
		{
			drr[3]--;
			drr[2]--;
			count = count + 6;
		}
		count = count + drr[3] * 3 + drr[2] * 2 + drr[4] * 4;
	}
	printf("\n\t%d", count);
	return 0;
}

int get_prime_d(int number)
{
	for (int i = 3; i < (number / 1024) + 260; i = i + 2)
	{
		if (number % i == 0)
		{
			return i;
		}
	}
	return number;
}