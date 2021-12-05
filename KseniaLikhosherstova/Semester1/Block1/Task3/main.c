#include <stdio.h>
#include <math.h>


long double in_degree(long double angle)
{
	return angle * (180.0 / 3.14159265358979323846);
}

double print_angle(double a, double b, double c)
{
	double angle, degrees, minutes, roundmin, seconds;
	angle = in_degree(acos((b * b + c * c - a * a) / (2.0 * b * c))); 
	degrees = floor(angle);
	minutes = (angle - degrees) * 60.0;
	roundmin = floor(minutes);
	seconds = round((minutes - roundmin) * 60.0);
	printf("%d degrees %d minutes %d seconds\n", (int)degrees, (int)roundmin, (int)seconds);

}

int main()
{
	printf("This program defines whether it is possible to construct a non-degenerate triangle with the entered sides.\n");
	printf("If possible, its angles in degrees, minutes and seconds will be outputed.\n");
	double a, b, c;
	printf("Enter the lengths of the sides of the triangle:\n");
	scanf("%lf%lf%lf", &a, &b, &c);
	while ((a <= 0) || (b <= 0) || (c <= 0)) 
	{
		printf("Invalid input. Enter another numbers:\n");
		char clean = 0;
		while (clean != '\n' && clean != EOF)
			clean = getchar();
		scanf("%lf%lf%lf", &a, &b, &c);
	}

	if ((a + b > c) && (a + c > b) && (b + c > a))
	{
		printf("It is possible to construct a non-degenerate triangle with the entered sides.\n");
		print_angle(a, b, c); 
		print_angle(b, c, a);
		print_angle(c, a, b);
	}
	else
	{
		printf("It is impossible to construct a non-degenerate triangle.\n");
	}
	return 0;
}