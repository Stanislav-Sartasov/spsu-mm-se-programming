#include <stdio.h>
#include <stdbool.h>
#include <math.h>

int root_is_integer(n)
{
	double num;
	num = sqrt(n);
	return (num == (int)num);
}

int main()
{
	int number, period, integer_portion, correctly_read;
	printf("This program displays the period and continued fraction of the square root of the entered number. \n");
	printf("Enter a natural number: ");
	while (true)
	{
		{
			char ch;
			int correctly_read = scanf_s("%d%c", &number, &ch);
			if (correctly_read == 2 && (ch == ' ' || ch == '\n'))
			{
				break;
			}
			else
			{
				printf("The input value entered is not a natural number. Please try again: ");
				fseek(stdin, 0, 0);
			}
		}
	}
	if (root_is_integer(number))
	{
		printf("The entered number is a square of an integer.");
		return 0;
	}
	integer_portion = floor(sqrt(number));
	period = 0;
	int x, y, z;
	x = integer_portion;
	y = 0;
	z = 1;
	printf("[ %d ", integer_portion);
	do
	{
		y = x * z - y;
		z = (number - y * y) / z;
		x = (integer_portion + y) / z;
		printf("%d ", x);
		period += 1;
	} 
	while (z != 1);
	printf("] \n Period = %d", period);
	return 0;
}