#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>


bool isPythagorean(int a, int b, int c)
{
	return (a * a + b * b == c * c) || (b * b + c * c == a * a) || (a * a + c * c == b * b);
}

int gcd(int a, int b)
{
	while ((a != 0) && (b != 0))
	{
		if (a > b)
			a %= b;
		else
			b %= a;
	}
	return a + b;
}

bool isPrime(a, b, c)
{
	return gcd(a, gcd(b, c)) == 1;
}

int main()
{
	int result;
	int a, b, c;
	do
	{
		printf("Please enter natural numbers:\n");
		result = scanf_s("%d%d%d", &a, &b, &c);
		fflush(stdin);
	}
	while (result != 3 || a <= 0 || b <= 0 || c <= 0);
	if (isPythagorean(a, b, c))
	{
		if (isPrime(a, b, c))
		{
			printf("Is prime Pythagorean triplet\n");
			return 0;
		}
		printf("Is Pythagorean triplet\n");
		return 0;
	}
	printf("Is not Pythagorean triplet\n");
	return 0;
}