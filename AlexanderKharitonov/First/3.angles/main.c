#include <stdio.h>
#include <math.h>
#include <stdbool.h>

#define _USE_MATH_DEFINES

void angle(double a, double b, double c)
{
	double angle = (acos((pow(b, 2) + pow(c, 2) - pow(a, 2)) / (2 * b * c)) * 180 / M_PI);
	int min = (int) ((angle - (int) angle) * 60);
	int sec = (int) (((angle - (int) angle) * 60 - min) * 60);
	printf("%d %d' %d''\n", (int) angle, min, sec);
}

int main()
{
	double a, b, c;
	int count;
	printf("This program displays if a triangle is possible and its angles.\n");
	do
	{
		printf("Enter 3 side lengths of triangle: \n");
		count = scanf("%lf %lf %lf", &a, &b, &c);
		while (getchar() != '\n');
	}
	while (count != 3 || a <= 0 || b <= 0 || c <= 0);
	if (a + b > c && a + c > b && c + b > a)
	{
		printf("Triangle is exist. Angles are:\n");
		angle(a, b, c);
		angle(b, c, a);
		angle(c, a, b);
	}
	else
		printf("Triangle is not exist\n");
	return 0;
}
