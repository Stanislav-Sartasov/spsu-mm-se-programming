#include <stdio.h>
#include <locale.h>

int GSD(int a, int b);

int main()
{
	
	printf("This program identify primitive Pythagorean triple\n\n");
	printf("Enter three integers: ");

	int x, y, z;
	while (!(scanf_s("%d", &x) && scanf_s("%d", &y) && scanf_s("%d", &z)))
	{
		scanf_s("%*[^\n]");
		printf("Input error. Please, enter integer variables\n\nEnter three integers: ");
	}

	if (x >= y)
	{
		if (x > z)
		{
			x += z;
			z = x - z;
			x -= z;
		}
	}
	else
	{
		if (y > z)
		{
			y += z;
			z = y - z;
			y -= z;
		}
	}

	if (x * x + y * y == z * z)
	{
		if (GSD(GSD(x, y), z) == 1)
			printf("It is primitive Pythagorean triple.\n");
		else
			printf("It is not primitive Pythagorean triple.\n");
	}
	else
		printf("It is not Pythagorean triple.\n");
}

int GSD(int a, int b)
{
	if (a == b)
		return a;

	while (a > 0 && b > 0)
	{
		if (a > b)
			a = a % b;
		else
			b = b % a;
	}

	if (a == 0)
		return b;
	else
		return a;
}