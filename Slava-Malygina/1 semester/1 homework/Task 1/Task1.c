#include <stdio.h>
#include <math.h>

int main()
{
	int a;
	printf("This program prints Mersenne prime numbers:\n");
	for (int i = 2; i < 32; i += 1)
	{
		int k = 1;
		a = (pow(2, i)) - 1;
		for (int j = 2; j < a; j += 1)
		{
			if (a % j == 0)
			{
				k = 0;
				break;
			}
		}
		if (k)
			printf("%li\n", a);
	}
	return 0;
}