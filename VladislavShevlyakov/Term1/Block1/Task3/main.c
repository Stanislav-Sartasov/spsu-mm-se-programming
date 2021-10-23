#define _CRT_SECURE_NO_WARNINGS
#define M_PI 3.14159265358979323846

#include <stdio.h>
#include <math.h>
#include <string.h>

float angle_degrees(float a, float b, float c)
{
	return acos((b * b + c * c - a * a) / (2 * b * c)) * 180 / M_PI;
}

float angle_minutes(float a, float b, float c)
{
	return (angle_degrees(a, b, c) - trunc(angle_degrees(a, b, c))) * 60;
}

float angle_seconds(float a, float b, float c)
{
	return (angle_minutes(a, b, c) - trunc(angle_minutes(a, b, c))) * 60;
}

int check_input(char* str) // checking the entered numbers for compliance with the conditions
{
	for (int i = 0; i < strlen(str); i++)
	{
		if (str[i] == '.')
		{
			if (!(str[i - 1] >= '0' && str[i - 1] <= '9' && str[i + 1] >= '0' && str[i + 1] <= '9'))
				return 0;
		}
		else if (!(str[i] >= '0' && str[i] <= '9'))
			return 0;
	}

	return 1;
}

int main()
{
	float a, b, c;
	char a_input[256], b_input[256], c_input[256];
	printf("The program determines whether it is possible to construct a non-degenerate triangle. And, if possible, outputs its angles.\n\n");
	printf("Enter three sides of triangle: \n");
	scanf("%s%s%s", a_input, b_input, c_input);

	while (!check_input(a_input) || !check_input(b_input) || !check_input(c_input) || atof(a_input) <= 0 || atof(b_input) <= 0 || atof(c_input) <= 0)
	{
		printf("\nThe sides of the triangle must be positive numbers, please repeat the input:\n");
		scanf("%s%s%s", a_input, b_input, c_input);
	}

	a = atof(a_input);
	b = atof(b_input);
	c = atof(c_input);

	if (a < b + c && b < a + c && c < a + b)
	{
		printf("From these sides, you can build a non-degenerate triangle with angles:\n");
		printf("<ABC = %.0f degrees, %.0f minutes, %.0f seconds\n", trunc(angle_degrees(b, a, c)), trunc(angle_minutes(b, a, c)), trunc(angle_seconds(b, a, c)));
		printf("<BCA = %.0f degrees, %.0f minutes, %.0f seconds\n", trunc(angle_degrees(c, b, a)), trunc(angle_minutes(c, b, a)), trunc(angle_seconds(c, b, a)));
		printf("<CAB = %.0f degrees, %.0f minutes, %.0f seconds\n", trunc(angle_degrees(a, c, b)), trunc(angle_minutes(a, c, b)), trunc(angle_seconds(a, c, b)));
	}
	else
		printf("From these sides, you can't build a non-degenerate triangle");

	return 0;
}