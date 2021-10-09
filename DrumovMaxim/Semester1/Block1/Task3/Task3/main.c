#include <stdio.h>
#include <math.h>

long double calcDegree(long double angle) 
{
	return angle * (180.0 / 3.14159265358979323846);
}

double printAngle(double a, double b, double c)
{
	double angle, degrees, minutes, minutesFloor, seconds;
	angle = calcDegree(acos((b * b + c * c - a * a) / (2.0 * b * c)));
	degrees = floor(angle);
	minutes = (angle - degrees) * 60.0;
	minutesFloor = floor(minutes);
	seconds = round((minutes - minutesFloor) * 60.0);
	printf("%d degrees %d minutes %d seconds\n", (int)degrees, (int)minutesFloor, (int)seconds);
}

void cleanInput()
{
	char s;
	do
	{
		s = getchar();
	} while (s != '\n' && s != EOF);
}

int correctInput(long double* number)
{
	return (scanf("%30lf", number) && *number > 0);
}

long double getInputNum()
{
	long double number = 0;
	while (!(correctInput(&number) && getchar() == '\n'))
	{
		cleanInput();
		printf("Your input is not correct! Try again input a positive number:\n");
	}
	return number;
}

int main()
{
	printf("In this program, you need to enter 3 natural numbers. Check whether it is possible to construct \n");
	printf("a non-degenerate triangle from them. Calculate its degrees, minutes and seconds.\n\n");

	printf("Input the first positive number:\n");
	double firstNum = getInputNum();
	printf("Input the second positive number:\n");
	double secondNum = getInputNum();
	printf("Input the third positive number:\n");
	double thirdNum = getInputNum();
	if (firstNum < secondNum + thirdNum && secondNum < firstNum + thirdNum && thirdNum < firstNum + secondNum)
	{
		printf("It is possible to construct a non-degenerate triangle \n");
		printAngle(firstNum, secondNum, thirdNum); // Angle Alfa
		printAngle(secondNum, firstNum, thirdNum); // Angle Beta
		printAngle(thirdNum, firstNum, secondNum); // Angle Gamma
	}
	else
	{
		printf("It is impossible to construct a non-degenerate triangle\n");
	}
}