#define _USE_MATH_DEFINES
#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <math.h>


int is_triangle(double a, double b, double c)
{
	return ((a + b) > c && (a + c) > b && (b + c) > a);
}

// This function counts the value of the angle between sides 'a' and 'b'
void angle(double a, double b, double c)
{
	double angle, temp;
	int degrees, minutes, seconds;
	angle = acos((a * a + b * b - c * c) / (2 * a * b)) * 180 / M_PI;
	
	degrees = (int)angle;
	temp = (angle - degrees)*60;
	minutes = (int)temp;
	temp = (temp - minutes) * 60;
	seconds = (int)temp;
	printf("%d degrees, %d minutes, %d seconds.\n", degrees, minutes, seconds);
}

int main()
{
	printf("This program calculates the value of the angles of the triangle with entered sides, ");
	printf("if such a triangle exists!\n\n");
	double a, b, c;
	char end;
	printf("Enter lengths of the triangle sides:\n");
	
	while (scanf("%lf %lf %lf%c", &a, &b, &c, &end) != 4 || a <= 0 || b <= 0 || c <= 0 || end != '\n')
	{
		while (end != '\n')
			scanf("%c", &end);
		end = '\0';
		printf("Input error, please try again:\n");
	}

	if (is_triangle(a, b, c))
	{
		printf("\nFirst angle:\t");
		angle(a, b, c);
		printf("Second angle:\t");
		angle(a, c, b);
		printf("Third angle:\t");
		angle(b, c, a);
	}
	else
		printf("\nThere is no triangle with such sides!\n");
	
	return 0;
}