#include <stdio.h>
#include <math.h>

double get_double_number()
{
	printf(">>> ");
	double num;
	char end;
	int read_result = scanf("%lf%c", &num, &end);
	if ((read_result == 2) && (end == '\n'))
	{
		if (num > 0)
		{
			return num;
		}

		printf("Number must be positive!\n");
	}
	else
	{
		printf("Please enter correct number\n");
	}

	while (end != '\n')
	{
		scanf("%c", &end);
	}

	return get_double_number();
}

void get_angle(double op_side, double adj_side_1, double adj_side_2)
{
	double angle_radians = acos((pow(adj_side_1,2) + pow(adj_side_2, 2) - pow(op_side, 2)) / (2 * adj_side_1 * adj_side_2));
	int angle_secs = angle_radians * 180 * 3600 / acos(-1);
	int angle_mins = angle_secs / 60;
	int angle_degs = angle_mins / 60;
	printf("Angle opposite to %lf side is %d degrees, %d minutes, %d seconds\n", op_side, angle_degs, angle_mins % 60, angle_secs % 60);
}

int main()
{
	printf("This program calculates angles of triangle by its sides' lengths (if one with such sides exists)\n");
	printf("Enter 3 sides' lengths (each one on next line)\n");
	double a = get_double_number();
	double b = get_double_number();
	double c = get_double_number();
	if ((a + b > c) && (b + c > a) && (a + c > b))
	{
		get_angle(a, b, c);
		get_angle(b, a, c);
		get_angle(c, a, b);

	}
	else
	{
		printf("Such triangle doesn't exist");
	}
	return 0;
}
