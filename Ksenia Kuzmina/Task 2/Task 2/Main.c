#include <stdio.h>

int alg_euclid(int a, int b)
{
	while ((a != 0) && (b != 0))
	{
		if (a > b)
			a = a % b;
		else
			b = b % a;
	}
	return a + b;
}

int main()
{
	printf("This program checks if the inputted numbers are coprime Pythagorean triples.\n");
	int a, b, c;
	printf("Input three numbers:\n");
	scanf_s("%d%d%d", &a, &b, &c);
	if ((a * a + b * b == c * c) || (a * a + c * c == b * b) || (b * b + c * c == a * a))
	{
		if ((alg_euclid(a, b) == 1) && (alg_euclid(b, c) == 1) && (alg_euclid(a, c) == 1))
			printf("The numbers are a Pythagorean triple and coprime");
		else
			printf("The numbers are a Pythagorean triple and they are not coprime");
	}
	else if ((alg_euclid(a, b) == 1) && (alg_euclid(b, c) == 1) && (alg_euclid(a, c) == 1))
		printf("The numbers are not a Pythagorean triple and they are coprime");
	else
		printf("The numbers are not a Pythagorean triple and they are not coprime");
	return 0;
}