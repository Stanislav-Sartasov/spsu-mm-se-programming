#define _USE_MATH_DEFINES

#include <stdio.h>
#include <math.h>

int minutes(double deg);
int seconds(double deg);

int main()
{

	printf("This program gets three numders: lengths of a triangle sides\n");
	printf("and if the triangle is non-degenerate displays it's angels\n\n");
	
	double a, b, c;
	printf("Enter sides a, b, c: ");
	while (!(scanf_s("%lf", &a) && scanf_s("%lf", &b) && scanf_s("%lf", &c)))
	{
		scanf_s("%*[^\n]");
		printf("Input error. Please, enter double variables\n\nEnter sides a, b, c: ");
	}
    printf("\n");

	if (a < b + c && b < a + c && c < a + b)
	{
		
		char angle[3][6] = { "Alpha", "Beta", "Gamma" };
		double deg[3] = { 0 };
		deg[0] = (180 * acos((b * b + c * c - a * a) / (2 * b * c))) / M_PI;
		deg[1] = (180 * acos((a * a + c * c - b * b) / (2 * a * c))) / M_PI;
		deg[2] = (180 * acos((b * b + a * a - c * c) / (2 * b * a))) / M_PI;

		for (int i = 0; i < 3; i++)
		{
			printf("%s is %d degrees %d minutes %d seconds\n", angle[i], (int)deg[i], minutes(deg[i]), seconds(deg[i]));
		}
	}
	else
		printf("Triangle is degenerate");
	
	return 0;
}

int minutes(double deg)
{
	return (int)(deg * 60) % 60;
}

int seconds(double deg)
{
	return (int)(deg * 3600) % 60;
}