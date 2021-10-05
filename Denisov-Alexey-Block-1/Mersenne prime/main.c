#include <stdio.h>
#include <stdbool.h>
#include <math.h>

bool isSimple(int a);

int main()
{
	printf("This program displays simple Mersenne primes to 2^31-1\n\n");

	for (int n = 1; n <= 31; n++)
	{
		if (isSimple(pow(2, n) - 1))
			printf("%d\n", (int)pow(2, n) - 1);
	}
}

bool isSimple(int a)
{
	switch (a)
	{
	case 1:
		return false;
		break;
	case 2:
		return true;
		break;
	default:
		for (int i = 2; i < a; i++)
		{
			if (a % i == 0)
				return false;
		}
		return true;
	}
}