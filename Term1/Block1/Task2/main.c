#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <math.h>

int GCD(int a, int b) // greatest common divisor of two numbers
{
	if (b == 0)
		return a;
	return GCD(b, a % b);
}

int main()
{
	int x, y, z;
	printf("Enter three numbers:\n");
	scanf("%d%d%d", &x, &y, &z);

	if (x <= 0 || y <= 0 || z <= 0)
	{
		printf("The entered numbers must be natural. Please repeat the input.\n");
		return 0;
	}

	if (y * y + z * z == x * x || x * x + z * z == y * y || x * x + y * y == z * z)
	{
		if (GCD(x, y) == 1 && GCD(x, z) == 1 && GCD(y, z) == 1)
			printf("The entered numbers form a primitive Pythagorean triple");
		else
			printf("The entered numbers form a non-primitive Pythagorean triple");
	}
	else
		printf("The entered numbers do not form a Pythagorean triple");

	return 0;
}