#include <stdio.h>
#define _USE_MATH_DEFINES
#include <math.h>

void clear_buffer()
{
	while (getchar() != '\n');
}

int is_a_triangle(double a, double b, double c)
{
	return ((a + b > c) && (b + c > a) && (a + c > b));
}

double converting_radians(double angle)
{
	return angle * 180 / M_PI;
}

double printed_angles(double angle)
{
	int degrees = (int) angle;
	int minutes = (int) (angle * 60) % 60;
	int seconds = (int) (angle * 3600) % 60;
	printf("%d %d' %d\" \n", degrees, minutes, seconds);
}

int main()
{
	double a, b, c;
	int correctly_read;
	printf("This program prints angles of no degenerate triangle.\n");
	do
	{
		printf("Enter the numerical values for the sides of the triangle: ");
		correctly_read = scanf_s("%lf%lf%lf", &a, &b, &c);
		clear_buffer(stdin);
	} 
	while (correctly_read != 3 || a < 0 || b < 0 || c < 0);
	if (!is_a_triangle(a, b, c))
	{
		printf("This is a degenerate triangle\n");
		return 0;
	}
	double angles[] = {0, 0, 0};
	angles[0] = acos((a * a + b * b - c * c) / (2.0 * a * b));
	angles[1] = acos((a * a + c * c - b * b) / (2.0 * a * c));
	angles[2] = acos((b * b + c * c - a * a) / (2.0 * b * c));
	int i = 0;
	while (i < 3)
	{
		printed_angles(converting_radians(angles[i]));
		i++;
	}
	return 0;
}