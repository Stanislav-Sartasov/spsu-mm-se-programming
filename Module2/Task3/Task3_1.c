#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>


unsigned int check_number(unsigned int num)
{
	for (unsigned int i = 2; i * i <= num; i++)
	{
		if (num % i == 0)
		{
			return i;
		}
	}
	return 0;

}

unsigned int digital_root(unsigned int x)
{
	unsigned int s;
	for (; x > 9; x = s)
		for (s = 0; x; x /= 10)
			s += x % 10;
	return (unsigned int)x;

}

int main()
{
	int predel = 999999;
	unsigned int* MDRS = malloc(((predel + 1) * sizeof(unsigned int)));
	unsigned int sum = 0;

	for (unsigned int i = 2; i < 10; i++)
	{
		MDRS[i] = i;
		sum += i;
	}

	for (unsigned int i = 10; i <= predel; i++)
	{
		MDRS[i] = digital_root(i);

		unsigned int point_start = check_number(i);

		if (point_start)
		{
			for (unsigned int j = point_start; j * j <= i; j++)
			{
				if (i % j == 0)
				{
					unsigned int check = MDRS[i / j] + MDRS[j];

					if (check > MDRS[i])
					{
						MDRS[i] = check;
					}
				}
			}

		}

		sum += MDRS[i];
	}

	system("chcp 1251");
	system("cls");
	printf("Сумма всех MDRS(n) при n=[2; 999999] равна %u", sum);

	free(MDRS);

	return 0;
}