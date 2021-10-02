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
	printf("Please input three natural numbers:\n");
	scanf_s("%d%d%d", &a, &b, &c);

	while ((a <= 0) || (b <= 0) || (c <= 0))
	{
		printf("At least one string is not a natural number. Please try again");
		char clean = 0;
		while (clean != '\n' && clean != EOF)
			clean = getchar();
		scanf_s("%d%d%d", &a, &b, &c);
	}

	if ((a * a + b * b == c * c) || (a * a + c * c == b * b) || (b * b + c * c == a * a))
	{
		if ((alg_euclid(alg_euclid(a, b), c)))
			printf("The numbers are a Pythagorean triple and coprime");
		else
			printf("The numbers are a Pythagorean triple and they are not coprime");
	}
	else if ((alg_euclid(alg_euclid(a, b), c)))
		printf("The numbers are not a Pythagorean triple and they are coprime");
	else
		printf("The numbers are not a Pythagorean triple and they are not coprime");
	return 0;
}