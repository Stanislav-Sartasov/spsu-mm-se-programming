#define _CRT_SECURE_NO_WARNINGS
#define _USE_MATH_DEFINES
#include <stdio.h>
#include <math.h>
#include <stdbool.h>

bool isTriangle(int a, int b, int c)
{
	if (a + b > c && a + c > b && b + c > a)
	{
		return true;
	}
	return false;
}

double getAngle(double a, double b, double c)
{
	return acos((pow(a, 2) + pow(b, 2) - pow(c, 2)) / (2 * a * b));
}

double getDegrees(double a)
{
	a = a / M_PI * 180;

	double degrees = a;
	return degrees;
}

double getMinutes(double a)
{
	double whole, fractional;
	fractional = modf(getDegrees(a), &whole);
	return fractional * 60;
}

double getSeconds(double a)
{
	double whole, fractional;
	fractional = modf(getMinutes(a), &whole);
	return fractional * 60;
}

int main()
{
	printf("The program defines the possibility of creating a triangle by given the leghth of its 3 sides.\n");

	double sideA, sideB, sideC, angleA, angleB, angleC;
	printf("Enter 3 numbers separated by space (use dot if float): ");
	scanf_s("%lf%lf%lf", &sideA, &sideB, &sideC);
	if (isTriangle(sideA, sideB, sideC))
	{
		angleA = getAngle(sideC, sideB, sideA);
		angleB = getAngle(sideA, sideC, sideB);
		angleC = getAngle(sideA, sideB, sideC);
		printf("Through the given numbers the triangle CAN BE created.\n");
		printf("Angle A is equal to %.0lf degrees, %.0lf minutes, %.0lf seconds\n", floor(getDegrees(angleA)), floor(getMinutes(angleA)), rint(getSeconds(angleA)));
		printf("Angle B is equal to %.0lf degrees, %.0lf minutes, %.0lf seconds\n", floor(getDegrees(angleB)), floor(getMinutes(angleB)), rint(getSeconds(angleB)));
		printf("Angle C is equal to %.0lf degrees, %.0lf minutes, %.0lf seconds\n", floor(getDegrees(angleC)), floor(getMinutes(angleC)), rint(getSeconds(angleC)));
	}
	else
	{
		printf("Through the given numbers the triangle DOES NOT exist");
	}
	return 0;
}