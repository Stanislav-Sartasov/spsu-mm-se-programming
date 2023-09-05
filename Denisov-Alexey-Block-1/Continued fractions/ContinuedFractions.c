#include <stdio.h>
#include <math.h>

int main()
{
	
	printf("This programm displays a continued fraction period of square root from integer.\n\n");

	int x;
	printf("Enter a integer that is not a square of a number: ");

	while (!scanf_s("%d", &x) || x<0 || floor(sqrt(x)) * floor(sqrt(x)) ==  x)
	{
		scanf_s("%*[^\n]");
		printf("Input error.\n\nEnter a integer that is not a square of a number: ");
	}
	printf("\n");

	int intx = sqrt(x);
	printf("Continued fraction: [ %d ", intx);

	int p = 0; int q = 1; int a = intx;
	int period = 0;

	do
	{

		p = a * q - p;
		q = (x - p * p) / q;

		a = (intx + p) / q;
		printf("%d ", a);

		period++;

	} while (q != 1);

	printf("]\nPeriod: %d\n", period);

	return 0;

}