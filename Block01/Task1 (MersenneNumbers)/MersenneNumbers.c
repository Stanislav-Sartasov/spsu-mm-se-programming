#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <math.h>
#include <stdbool.h>

bool isPrime(int n)
{
	for (int i = 3; i < sqrt(n) + 1; i+=2)
	{
		if ((n % i) == 0)
		{
			return false;
		}
	}
	return true;
}

int main(int argc, char** argv)
{
	printf("The program searches Mersenne Numbers within the range [1; 2^31-1]\n");
	int power = 31;
	for (int i = 2; i <= power; ++i)
	{
		int p = (int)pow(2, i);
		if (isPrime(i))
		{
			if (isPrime(p - 1))
			{
				printf("pow(2,%d) - 1 = %d\n", i, p - 1);
			}
		}
	}
	return 0;
}