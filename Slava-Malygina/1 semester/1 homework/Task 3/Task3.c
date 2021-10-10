#define _USE_MATH_DEFINES
#include <stdio.h>
#include <math.h>
#include <ctype.h>

double radian(double a, double b, double c)
{
	return (acos((b * b + c * c - a * a) / (2 * b * c)) * 180) / M_PI;
}

double degree(double d)
{
	int math_d, math_m, math_s;
	math_d = (int)d;
	math_m = (int)(60 * (d - math_d));
	math_s = (int)((3600 * (d - math_d)) - 60 * math_m);
	printf("%d° %d' %d'' \n", math_d, math_m, math_s);

}

int main()
{
	int a, b, c;
	char d;
	printf("This program determines whether it is possible to construct a non-degenerate triangle with sides of the entered length. If it possible, program will calculate angles. Enter numbers: ");
	scanf("%d%d%d", &a, &b, &c);
	d = getchar();
	while ((a <= 0) || (b <= 0) || (c <= 0) || isalpha(d))
	{
		printf("Invalid value. You must enter three positive numbers. Please, re-enter: ");
		scanf("%d%d%d", &a, &b, &c);
		d = getchar();
	}
	if ((a + b > c) && (a + c > b) && (c + b > a))
	{
		degree(radian(a, b, c));
		degree(radian(b, a, c));
		degree(radian(c, b, a));
	}
	else
	{
		printf("Degenerate triangle\n");
	}
	return 0;
}