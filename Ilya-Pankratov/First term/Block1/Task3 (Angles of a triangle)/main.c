#include <stdio.h>
#include <math.h>

#define PI 3.14159265358979323

void calculateAngles(float* sides);

int main()
{
	printf("The program determines whether it is possible, based on the three entered numbers, to build a non-degenerate triangle with the corresponding sides and shows the angles between the sides\n\n");
	float sides[3] = { 0 };
	char check;

	for (int i = 0; i < 3; i++)
	{
		printf("Input the leght of the %d side(It must be greater than zero): ", i + 1);
		while ((scanf_s("%f%c", &sides[i], &check) != 2) || (check != '\n') || sides[i] <= 0)
		{
			printf("\nError: you enter incorrect data. You have number must be higher than zero.\nPlease, try again\n\n");

			while (check != '\n') 
				scanf_s("%c", &check);

			check = '\0';
			printf("Input the leght of the %d side(It must be greater than zero): ", i + 1);
		}
		check = '\0';
	}
	
	printf("\n");

	if ((sides[0] + sides[1] > sides[2]) && (sides[0] + sides[2] > sides[1]) && (sides[2] + sides[1] > sides[0]))
		calculateAngles(&sides);
	else
		printf("Can't create a non-degenerate tringle\n");

	return 0;
}

void calculateAngles(float* sides)
{
	float firstAngle, secondAngle, thirdAngle;
	firstAngle = (acos((sides[0] * sides[0] + sides[1] * sides[1] - sides[2] * sides[2]) / (2 * sides[0] * sides[1])) * 180) / PI;
	secondAngle = (acos((sides[0] * sides[0] + sides[2] * sides[2] - sides[1] * sides[1]) / (2 * sides[0] * sides[2])) * 180) / PI;
	thirdAngle = (acos((sides[2] * sides[2] + sides[1] * sides[1] - sides[2] * sides[2]) / (2 * sides[2] * sides[1])) * 180) / PI;

	float degrees[] = { firstAngle, secondAngle, thirdAngle }, minutes[3], seconds[3];

	for (int i = 0; i < 3; i++)
	{
		minutes[i] = (degrees[i] - (int)degrees[i]) * 60;
		seconds[i] = (minutes[i] - (int)minutes[i]) * 60;
		printf("The %d angle = %.0f degrees %.0f minutes %.0f seconds \n", i + 1, degrees[i], minutes[i], seconds[i]);
	}
}