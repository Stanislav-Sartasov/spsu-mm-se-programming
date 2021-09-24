#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>

#define _USE_MATH_DEFINES

#include <math.h>

double double_input(char message[])
{
	bool check_correction = true, point_found = false;
	int sign = 1, end = 0, check_overflow = 0;
	double number = 0;
	do
	{
		bool first_mark_input = true;
		char symbol_input = '\0';
		double divisor = 10;
		sign = 1, number = 0, end = 0, check_overflow = 0;
		check_correction = true, point_found = false;
		printf_s("%s", message);
		while (check_correction && check_overflow < 16)
		{
			end = scanf_s("%c", &symbol_input);
			if (first_mark_input)
			{
				first_mark_input = false;
				if (symbol_input == '-')
				{
					sign = -1;
					first_mark_input = true;
					continue;
				}
				else if (symbol_input >= '0' && symbol_input <= '9')
				{
					number = symbol_input - '0';
					check_overflow++;
					continue;
				}
				check_correction = false;
			}
			else
			{
				if (check_overflow == 15 && (!end == EOF || !symbol_input == '\n'))
				{
					check_overflow++;
					break;
				}
				else if (symbol_input >= '0' && symbol_input <= '9')
				{
					if (!point_found)
					{
						number = number * 10 + symbol_input - '0';
						check_overflow++;
						continue;
					}
					else
					{
						number += ((double)(symbol_input - '0') / divisor);
						divisor *= 10;
						check_overflow++;
						continue;
					}
				}
				else if (symbol_input == '.' && !point_found)
				{
					point_found = true;
					continue;
				}
				else if (end == EOF || symbol_input == '\n')
				{
					break;
				}

				check_correction = false;
			}
		}
		if (!check_correction)
		{
			printf("incorrect input, there are string type, try again.\n");
			if (end == EOF || symbol_input == '\n')		// if the user immediately pressed Enter
			{
				continue;
			}
			symbol_input = '\0';
			while (symbol_input != '\n')	// clearing the buffer of unnecessary user input characters
			{
				scanf_s("%c", &symbol_input);
			}
		}
		else if (check_overflow > 15)
		{
			printf("overflow, the number of characters exceeds the number of characters of type double, please try again.\n");
			symbol_input = '\0';
			while (symbol_input != '\n')	// clearing the buffer of unnecessary user input characters
			{
				scanf_s("%c", &symbol_input);
			}
		}
	} while (!check_correction || check_overflow > 15);
	return number * sign;
}


void printAngles(double a, double b, double c)
{
	double p = (a + b + c) / 2;
	double s = sqrt(p * (p - a) * (p - b) * (p - c));
	double R = (a * b * c) / (4 * s);
	double angle = asin(a / (2 * R)) * 180 / M_PI;
	printf("Opposite side %d angle at degrees: %d, minutes: %d, seconds: %d. \n", (int)a, (int)trunc(angle), minutes(angle), seconds(angle));
	angle = asin(b / (2 * R)) * 180 / M_PI;
	printf("Opposite side %d angle at degrees: %d, minutes: %d, seconds: %d. \n", (int)b, (int)trunc(angle), minutes(angle), seconds(angle));
	angle = asin(c / (2 * R)) * 180 / M_PI;
	printf("Opposite side %d angle at degrees: %d, minutes: %d, seconds: %d. \n", (int)c, (int)trunc(angle), minutes(angle), seconds(angle));
}

int seconds(double angle)
{
	return (int)trunc((angle * 60 - trunc(angle * 60)) * 60);
}

int minutes(double angle)
{
	return (int)trunc((angle - trunc(angle)) * 60);
}

int main()
{
	double a, b, c;
	printf("This program determines whether it is possible to construct a non-degenerate triangle based on three numbers \nentered by the user. \n");
	printf("If possible, displays its angles in degrees, minutes and seconds with precision to the second. \n");
	a = double_input("Enter the first number: ");
	b = double_input("Enter the second number: ");
	c = double_input("Enter the third number: ");
	double middle_elem, minim_elem, maxim_elem;
	maxim_elem = max(max(a, b), c);
	minim_elem = min(min(a, b), c);
	middle_elem = a + b + c - minim_elem - maxim_elem;
	if ((a <= 0 || b <= 0 || c <= 0) || (maxim_elem >= (middle_elem + minim_elem)))
	{
		printf("it is impossible to construct a non-degenerate triangle with corresponding sides.");
		return 0;
	}
	printf("it is possible to construct a non-degenerate triangle with corresponding sides. \n");
	printAngles(a, b, c);
	return 0;
}