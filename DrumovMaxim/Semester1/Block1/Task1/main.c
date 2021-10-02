#include<stdio.h>
#include<math.h>

int primeN(int i)
{
	int prime = 1;
	for (int j = 2; j * j <= i; j++)
	{
		if (i % j == 0)
		{
			return 0;
		}
	}
	if (prime)
	{
		return 1;
	}
}

int main() {
	int numberM;
	printf("Calculate Merseen's numbers\n");
	for (int i = 2; i <= 31; ++i)
	{
		if (primeN(i) == 1)
		{
			numberM = pow(2, i) - 1;
			printf("%d\n", numberM);
		}
	}
	return 0;
}
