#include <stdio.h>
#include <math.h>
#include <stdbool.h>

int gcd(long long a, long long b)
{
	if (b == 0)
		return (int) a;
	return gcd(b, a % b);
}

int main()
{
	printf("This program will check pythagorean triples\n");
	long long x, y, z;
	int count;
	do
	{
		printf("Enter three natural number: \n");
		count = scanf("%lld %lld %lld", &x, &y, &z);
		while (getchar() != '\n');
	}
	while (count != 3 || x <= 0 || y <= 0 || z <= 0);
	if (x * x + y * y == z * z || x * x + z * z == y * y || y * y + z * z == x * x)
	{
		printf("This is Pythagorean triple\n");
		if (gcd(gcd(x, y), z) == 1)
			printf("Pythagorean triple is prime\n");
		else
			printf("Pythagorean triple isn't prime\n");
	}
	else
		printf("This is not Pythagorean triple\n");
	return 0;
}