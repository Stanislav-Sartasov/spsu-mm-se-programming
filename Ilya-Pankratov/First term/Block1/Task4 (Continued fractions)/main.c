#include <stdio.h>
#include <math.h>

int main()
{
	unsigned int number;
	char check;
	float checkNatural;

	printf("This program represents the square root of an integer that is not the square of another integer as an infinite continued fraction.\n\n");
	printf("Please enter a number that is not a square of an integer and higher then 0: ");

	while ((scanf_s("%f%c", &checkNatural, &check) != 2) || (check != '\n') || checkNatural <= 0 || checkNatural != (int)checkNatural || (int)checkNatural == pow((int)(pow(checkNatural, 0.5)), 2))
	{
		printf("\nError: you enter incorrect data. You have number must be higher than zero.\nPlease, try again\n");
		if (check != '\n')
			check = '\0';
		while (check != '\n')
			scanf_s("%c", &check);
		check = '\0';
		printf("\nPlease enter a number that is not a square of an integer and higher then 0: ");
	}

	number = (int)checkNatural;
	int remainder = 0, lastDenominator = 1, i = 0, intDenominator, intNumenator, difference;
	int intPartOfNumber = (int)(pow(number, 0.5)); 

	printf("\nInfinite continued fraction representation of the square root of %d: [%d; ", number, intPartOfNumber);

	for ( ; ; )
	{
		difference = intPartOfNumber - remainder;
		intDenominator = number - difference * difference;
		intDenominator = (int)(intDenominator / lastDenominator); 
		lastDenominator = intDenominator; 
		intNumenator = intPartOfNumber + difference;
		remainder = (int)(intNumenator / intDenominator); 

		if (i == 0)
			printf("%d", remainder);
		else
			printf(", %d", remainder);

		remainder = intNumenator - remainder * intDenominator; 
		i++;

		if (lastDenominator == 1)
		{
			printf("].");
			break;
		}
	}
	printf("\nPeriod of the continued fraction is %d.\n", i);
	return 0;
}