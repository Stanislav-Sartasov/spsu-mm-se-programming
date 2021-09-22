#define _CRT_SECURE_NO_WARNINGS
#define M_PI 3.14159265358979323846

#include <stdio.h>
#include <math.h>

float angleDegrees(float a, float b, float c)
{
	return acos((b * b + c * c - a * a) / (2 * b * c)) * 180 / M_PI;
}

float angleMinutes(float a, float b, float c)
{
	return (angleDegrees(a, b, c) - trunc(angleDegrees(a, b, c))) * 60;
}

float angleSeconds(float a, float b, float c)
{
	return (angleMinutes(a, b, c) - trunc(angleMinutes(a, b, c))) * 60;
}

int main()
{
	float a, b, c;
	printf("Enter three sides of triangle: \n");
	scanf("%f%f%f", &a, &b, &c);

	if (a < b + c && b < a + c && c < a + b)
	{
		printf("From these sides, you can build a non-degenerate triangle with angles:\n");
		printf("<ABC = %.0f degrees, %.0f minutes, %.0f seconds\n", trunc(angleDegrees(b, a, c)), trunc(angleMinutes(b, a, c)), trunc(angleSeconds(b, a, c)));
		printf("<BCA = %.0f degrees, %.0f minutes, %.0f seconds\n", trunc(angleDegrees(c, b, a)), trunc(angleMinutes(c, b, a)), trunc(angleSeconds(c, b, a)));
		printf("<CAB = %.0f degrees, %.0f minutes, %.0f seconds\n", trunc(angleDegrees(a, c, b)), trunc(angleMinutes(a, c, b)), trunc(angleSeconds(a, c, b)));
	}
	else
		printf("From these sides, you can't build a non-degenerate triangle");

	return 0;
}