#include <stdio.h>
#include <math.h>
#define PI 3.14159265357989

float get_angle(double a, double b, double c)
{
	return 180 * acos((a * a + b * b - c * c) / (2 * a * b)) / PI;
}

void print_angle(double a)
{
	printf("%d %d'%d\"\n", (int)floor(a), (int)floor((a - floor(a)) * 60), (int)floor((a - floor(a)) * 3600) % 60);
}

int main()
{
	printf("This program checks the existence of triangles and counts degrees of angles\n");
	double a, b, c, alpha, beta, gamma;
	printf("Enter the lengths of the sides of the triangle:\n");
	scanf_s("%lf%lf%lf", &a, &b, &c);
	while ((a < 0) || (b < 0) || (c < 0))
	{
		printf("You cannot input negative numbers or letters. Please try again.");
		char clean = 0;
		while (clean != '\n' && clean != EOF)
			clean = getchar();
		scanf_s("%lf%lf%lf", &a, &b, &c);
	}

	if ((a + b > c) && (a + c > b) && (b + c > a))
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