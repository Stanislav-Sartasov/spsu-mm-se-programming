#include <stdio.h>
#include <stdbool.h>
#include <math.h>

bool isTriangle(float* sides);
void calculateAngles(float* arr);

int main()
{
	printf("The program determines whether it is possible, based on the three entered numbers, to build a non-degenerate triangle with the corresponding sides and shows the angles between the sides\n\n");
	float sides[3];
	char check;

	for (int i = 0; i < 3; i++)
	{
		printf("Input the leght of the %d side(It must be greater than zero): ", i + 1);
		if ((scanf_s("%f%c", &sides[i], &check) != 2) || (check != '\n'))
		{
			printf("\nError: you enter incorrect data(Letters or symbols)\nPlease, try again\n");
			return 0;
		}

		if (sides[i] <= 0)
		{
			printf("\nError: your number is less than or equal to zero\nPlease, try again\n");
			return 0;
		}
	}

	printf("\n");

	if (isTriangle(&sides))
	{
		calculateAngles(&sides);
	}
	else
	{
		printf("Can't create a nondegenerate tringle\n");
	}

	return 0;
}

bool isTriangle(float* sides)
{
	if ((sides[0] + sides[1] > sides[2]) && (sides[0] + sides[1] > sides[2]) && (sides[0] + sides[1] > sides[2]))
	{
		return true;
    } 
	else
	{
		return false;
	}

}
void calculateAngles(float* sides)
{
	float firstAngle, secondAngle, thirdAngle;
	float const PI = 3.1415926535;

	firstAngle = (acos((pow(sides[0], 2) + pow(sides[1], 2) - pow(sides[2], 2)) / (2 * sides[0] * sides[1])) * 180) / PI;
	secondAngle = (acos((pow(sides[0], 2) + pow(sides[2], 2) - pow(sides[1], 2)) / (2 * sides[0] * sides[2])) * 180) / PI;
	thirdAngle = (acos((pow(sides[2], 2) + pow(sides[1], 2) - pow(sides[0], 2)) / (2 * sides[2] * sides[1])) * 180) / PI;

	float degrees[] = { firstAngle, secondAngle, thirdAngle }, minutes[3], seconds[3];

	for (int i = 0; i < 3; i++)
	{
		minutes[i] = (degrees[i] - (int)degrees[i]) * 60;
		seconds[i] = (minutes[i] - (int)minutes[i]) * 60;
		printf("The %d angle = %.0f degrees %.0f minutes %.0f seconds \n", i + 1, degrees[i], minutes[i], seconds[i]);
	}
}