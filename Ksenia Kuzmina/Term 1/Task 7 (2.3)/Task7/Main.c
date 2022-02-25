#include <stdio.h>
#include <stdlib.h>

int digital_root(int number)
{
	if (number % 9 == 0)
		return 9;
	else
		return number % 9;
}

int main()
{
	int maximum = 0;
	int mdrs = 0;
	int* res = (int*)malloc(sizeof(int) * 999998);

	printf("This program calculates the maximum digital roots sum in the range from 2 to 99999.\n");

	for (int i = 0; i < 999998; i++)
	{
		maximum = 0;
		for (int j = 2; j * j <= (i + 2); ++j)
		{
			if (((i + 2) % j == 0) && (maximum <= res[((i + 2) / j) - 2] + res[j - 2]))
				maximum = res[((i + 2) / j) - 2] + res[j - 2];
		}

		if (maximum > digital_root(i + 2))
			res[i] = maximum;
		else
			res[i] = digital_root(i + 2);

		mdrs += res[i];
	}

	free(res);

	printf("The maximum digital roots sum is %d", mdrs);
	return 0;
}