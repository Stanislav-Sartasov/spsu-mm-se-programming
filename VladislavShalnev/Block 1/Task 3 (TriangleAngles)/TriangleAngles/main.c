
#define _USE_MATH_DEFINES

#include <stdio.h>
#include <math.h>

double find_angle(double x, double y, double z)
{
	return 180 * acos((x * x + y * y - z * z) / (2 * x * y)) / M_PI;
}

int is_triangle(double x, double y, double z)
{
	return (x + y > z) && (x + z > y) && (y + z > x);
}

int degrees(double angle) {
	return (int)floor(angle);
}

int minutes(double angle) {
	return (int)floor((angle - floor(angle)) * 60);
}

int seconds(double angle) {
	return (int)floor((angle - floor(angle)) * 3600) % 60;
}

void input(double* adress, char* message)
{
	while (1)
	{
		char input[256];

		printf(message);
		fgets(input, sizeof(input), stdin);

		if (!sscanf_s(input, "%lf", adress))
		{
			printf("Inputed side is not a number.\n");
			continue;
		}
		if (*adress <= 0)
		{
			printf("Inputed side can't be less than zero or equal to zero.\n");
			continue;
		}
		return;
	}

}

int main()
{
	double x, y, z, alpha, beta, gamma;
	printf("This program checks existance of triangle by entered sides and counts its angles.\n\n");
	printf("Enter triangle sides:\n");
	input(&x, "Enter first side: ");
	input(&y, "Enter second side: ");
	input(&z, "Enter third side: ");
	printf("\n");

	if (is_triangle(x, y, z))
	{
		printf("Triangle exists.\n\n");

		alpha = find_angle(x, y, z);
		beta = find_angle(z, x, y);
		gamma = find_angle(z, y, x);

		printf("Angles:\n");
		printf("Alpha: %ddeg %d' %d\"\n", degrees(alpha), minutes(alpha), seconds(alpha));
		printf("Beta: %ddeg %d' %d\"\n", degrees(beta), minutes(beta), seconds(beta));
		printf("Gamma: %ddeg %d' %d\"\n", degrees(gamma), minutes(gamma), seconds(gamma));
	}
	else
	{
		printf("Triangle does not exist.\n");
	}
	return 0;
}