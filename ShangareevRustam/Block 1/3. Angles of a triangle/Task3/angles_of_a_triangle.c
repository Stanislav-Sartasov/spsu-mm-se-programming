#define _USE_MATH_DEFINES

#include <math.h>
#include <stdio.h>
#include <stdbool.h>


int get_angle(long double opposite_side, long double adjacent_side1, long double adjacent_side2)
{
	long double cos_angle = ((adjacent_side1 * adjacent_side1 + adjacent_side2 * adjacent_side2 - opposite_side * opposite_side) / (2 * adjacent_side1 * adjacent_side2));
	int angle = round((long double)3600 * 180 * acos(cos_angle) / M_PI);
	return angle;
}

void input(long double* a, long double* b, long double* c)
{
	printf("Please enter three positive numbers separated by spaces:\n");
	char char_after_numbers;
	while (true)
	{
		int input_check = scanf_s("%lf %lf %lf%c", a, b, c, &char_after_numbers);
		if (input_check == 4 && *a > 0 && *b > 0 && *c > 0 && (char_after_numbers == ' ' || char_after_numbers == '\n'))
		{
			break;
		}
		else
		{
			printf("At least one of the parameters entered is not a positive number. Please re-enter:\n");
			fseek(stdin, 0, 0);
		}
	}
}

void get_answer(long double opposite_side, long double adjacent_side1, long double adjacent_side2)
{
	int angle = get_angle(opposite_side, adjacent_side1, adjacent_side2);
	int angle_degree = angle / 3600, angle_minutes = (angle % 3600) / 60, angle_seconds = angle % 60;
	printf("%d degrees %d minutes and %d seconds\n", angle_degree, angle_minutes, angle_seconds);
}

void print_answer(double a, double b, double c)
{
	printf("The numbers entered can be the lengths of the sides of a non-degenerate triangle:\n");
	printf("The first angle of the triangle is ");
	get_answer(a, b, c);
	printf("The second angle of the triangle is ");
	get_answer(b, a, c);
	printf("The third angle of the triangle is ");
	get_answer(c, a, b);
}

int main()
{
	printf("This program determines whether it is possible, based on three numbers entered by the user, "
		"to build a non-degenerate triangle with the corresponding sides. "
		"If possible, it will determine the angles of the triangle in degrees, minutes, and seconds to the nearest second.\n");
	long double a, b, c;
	input(&a, &b, &c);
	if ((a + b > c) && (a + c > b) && (b + c > a))
	{
		print_answer(a, b, c);
	}
	else
	{
		printf("The numbers entered cannot be the lengths of the sides of a non-degenerate triangle.\n");
	}
	return 0;
}