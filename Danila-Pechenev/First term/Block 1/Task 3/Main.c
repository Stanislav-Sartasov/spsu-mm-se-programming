#include <stdio.h>
#include <math.h>

double PI = 3.14159265358979323846;  // Const

void count_and_print_angle(double a, double b, double c)
{
	double cosine = (b * b + c * c - a * a) / (2 * b * c);
	double arccosine = acos(cosine);
	double radians_in_degrees = arccosine * (180 / PI);
	double degrees = floor(radians_in_degrees);
	double remainder_in_minutes = (radians_in_degrees - degrees) * 60;
	double minutes = floor(remainder_in_minutes);
	double seconds = round((remainder_in_minutes - minutes) * 60);
	printf("%d degrees %d minutes %d seconds\n", (int)degrees, (int)minutes, (int)seconds);
}

void clean_input()  // Cleaning the input area before using scanf_s function
{
	char s = ' ';
	while (s != '\n' && s != EOF)
	{
		s = getchar();
	}
}

int is_correct_input(double* number)  // Returns 1 only if the input is correct
{
	return (scanf_s("%lf", number) && 0 < *number && *number <= 1000000000);
}

double get_input_number()
{
	double number;
	while (!(is_correct_input(&number) && getchar() == '\n'))
	{
		clean_input();
		printf("Your input is incorrect! Please, try again:\n");
	}
	return number;
}

int is_triangle(double a, double b, double c)  // All numbers are positive
{
	return (a + b > c && a + c > b && b + c > a);
}

int main()
{
	printf("This program determines whether, based on the three input positive numbers,\n");
	printf("it is possible to construct a triangle with the corresponding sides.\n");
	printf("And if possible, determines its angles in degrees, minutes and second.\n");
	printf("Restrictions on the entered numbers: 0 < n <= 1000000000.\n");
	printf("\n");

	printf("Please, input the first positive number:\n");
	double a = get_input_number();
	printf("Please, input the second positive number:\n");
	double b = get_input_number();
	printf("Please, input the third positive number:\n");
	double c = get_input_number();

	if (is_triangle(a, b, c))
	{
		printf("It is possible to construct a triangle with such sides. And its angels are:\n");
		count_and_print_angle(a, b, c);
		count_and_print_angle(b, a, c);
		count_and_print_angle(c, a, b);
	}
	else
	{
		printf("It is impossible to construct a triangle with such sides.\n");
	}
}