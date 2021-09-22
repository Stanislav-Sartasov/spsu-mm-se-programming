#include <stdio.h>
#define _USE_MATH_DEFINES
#include <math.h>


double search_corner_degree(double first, double second, double third)
{
	double cos_first;
	cos_first = (pow(second, 2) + pow(third, 2) - pow(first, 2)) / (2 * second * third);
	return acos(cos_first) * 180 / M_PI;
}


void print_degree_minutes_seconds(double degrees, char* symbol)
{
	int only_degree, only_minutes, only_seconds;
	char* string;
	double minutes_with_seconds;
	only_degree = (int)degrees;
	minutes_with_seconds = (degrees - only_degree) * 60;
	only_minutes = (int)(minutes_with_seconds);
	only_seconds = (int)((minutes_with_seconds - only_minutes) * 60);
	printf("Corner %s is %d degree %d minutes and %d seconds\n", symbol, only_degree, only_minutes, only_seconds);
}


int main()
{
	double a, b, c;
	double degrees_a, degrees_b, degrees_c;
	int count_right_num;
	printf("This programm determines can we build a right triangle or not.\n");
	printf("If we can do it, programm counts degree of every corners\n");
	do
	{
		printf("Enter 3 side lengths of triangle: ");
		count_right_num = scanf("%lf %lf %lf", &a, &b, &c);
		while (getchar() != '\n');
	} while (!(count_right_num == 3 && a > 0 && b > 0 && c > 0));
	
	
	if (a + b > c && a + c > b && c + b > a)
	{
		degrees_a = search_corner_degree(a, b, c);
		degrees_b = search_corner_degree(b, c, a);
		degrees_c = search_corner_degree(c, a, b);
		printf("Triangle is exist. It's corners:\n");
		print_degree_minutes_seconds(degrees_a, "a");
		print_degree_minutes_seconds(degrees_b, "b");
		print_degree_minutes_seconds(degrees_c, "c");
	}
	else
	{
		if (a == b + c || b == a + c || c == a + b)
			printf("This is a degenerate triangle.");
		else
			printf("This triangle is not exist.");
	}
}