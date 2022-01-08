#include <stdio.h>
#include <math.h>

double  get_number();
void    get_angle(double side_1, double side_2, double side_3);

int main()
{
	printf("This program determines whether it is possible to construct a non-degenerate triangle from three given numbers\n");
	printf("Please enter 3 sides lengths (each one on next line):\n");
	double a = get_number();
	double b = get_number();
	double c = get_number();
	if (a < b + c && b < a + c && c < a + b)
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

double get_number()
{
	double  num;
	char    end;
	int     res = scanf("%lf%c", &num, &end);
	if ((res == 2) && (end == '\n'))
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

	return get_number();
}

void get_angle(double side_1, double side_2, double side_3)
{
	double radians = acos((pow(side_2,2) + pow(side_3, 2) - pow(side_1, 2)) / (2 * side_2 * side_3));
	int seconds = round(radians * 180 * 3600 / acos(-1));
	int minutes = seconds / 60;
	int degrees = minutes / 60;
	printf("Angle opposite to %lf side is %d degrees, %d minutes, %d seconds\n", side_1, degrees, minutes % 60, seconds % 60);
}