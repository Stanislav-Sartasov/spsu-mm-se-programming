#include <stdio.h>
#include <math.h>
#define pi 3.1415926535

float get_angle(float a, float b, float c)
{
	return 180 * acos((a * a + b * b - c * c) / (2 * a * b)) / pi;
}

void print_angle(float a)
{
	printf("%d %d'%d\"\n", (int)floor(a), (int)floor((a - floor(a)) * 60), (int)floor((a - floor(a)) * 3600) % 60);
}

int main()
{
	printf("This program checks the existence of triangles and counts degrees of angles\n");
	double a, b, c, alpha, beta, gamma;
	printf("Enter the lengths of the sides of the triangle:\n");
	scanf_s("%lf%lf%lf", &a, &b, &c);
	if ((a < 0) || (b < 0) || (c < 0))
	{
		printf("You cannot input negative numbers. Try again.");
	}
	else if ((a + b > c) && (a + c > b) && (b + c > a))
	{
		alpha = get_angle(a, b, c);
		beta = get_angle(b, c, a);
		gamma = get_angle(c, a, b);
		print_angle(alpha);
		print_angle(beta);
		print_angle(gamma);
	}
	else
		printf("The triangle does not exist");
	return 0;
}