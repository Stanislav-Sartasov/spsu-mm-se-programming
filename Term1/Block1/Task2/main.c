#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int gcd(int a, int b) // greatest common divisor of two numbers
{
	if (b == 0)
		return a;
	return gcd(b, a % b);
}

int check_input(char* str) // checking the entered numbers for compliance with the conditions
{
	for (int i = 0; i < strlen(str); i++)
	{
		if (!(str[i] >= '0' && str[i] <= '9'))
			return 0;
	}

	return 1;
}

int main()
{
	char x_input[256], y_input[256], z_input[256];
	int x, y, z;
	printf("The program checks whether the entered numbers are a simple or ordinary Pythagorean triple.\n\n");
	printf("Enter three numbers:\n");
	scanf("%s%s%s", x_input, y_input, z_input);

	while (!check_input(x_input) || !check_input(y_input) || !check_input(z_input) || atoi(x_input) <= 0 || atoi(y_input) <= 0 || atoi(z_input) <= 0)
	{
		printf("\nThe sides of the triangle must be positive numbers, please repeat the input:\n");
		scanf("%s%s%s", x_input, y_input, z_input);
	}

	x = atoi(x_input);
	y = atoi(y_input);
	z = atoi(z_input);

	if (y * y + z * z == x * x || x * x + z * z == y * y || x * x + y * y == z * z)
	{
		if (gcd(gcd(x, y), z) == 1)
			printf("The entered numbers form a primitive Pythagorean triple");
		else
			printf("The entered numbers form a non-primitive Pythagorean triple");
	}
	else
		printf("The entered numbers do not form a Pythagorean triple");

	return 0;
}