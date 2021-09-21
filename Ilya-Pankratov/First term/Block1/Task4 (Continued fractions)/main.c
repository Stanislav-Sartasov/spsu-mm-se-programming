#include <stdio.h>
#include <math.h>

int main()
{
	unsigned int number;
	char check;
	float checkNatural;
	printf("This program represents the square root of an integer that is not the square of another integer as an infinite continued fraction\n\n");
	printf("Please enter a number that is not a square of an integer and higher then 0: ");

	if ((scanf_s("%f%c", &checkNatural, &check) != 2) || (check != '\n'))
	{
		printf("\nError: you enter letters or symbols, you should enter narutal number\nPlease, try again\n");
		return 0;
	}

	if ((checkNatural != (int)checkNatural) || (checkNatural <= 0) || (pow((int)pow(checkNatural, 0.5), 2) == checkNatural))
	{
		printf("\nError: your number does not match the conditions\nPlease try again\n");
		return 0;
	}

	number = (int)checkNatural;
	
	/* Algorithm for constructing an infinite continued fraction from an irrational number (square root):
	1. Allocation of the integer part from under the root and representation of the remainder of the number 
	in the form of the root minus the integer part
	2. Turn the fraction of the form m/n to the form 1/(n/m)
	3. Multiply the numerator and denominator of the fraction by a number of the form sqrt (number) + dif (the difference is calculated as
	the whole part of the root - the remainder from the previous step) to make the denominator rational.
	4. Count the denominator
	5. Reduce the current denominator by the past denominator (This will be an integer, you can prove it mathematically)
	6. Remember the current denominator for the next step
	7. Count the integer part of the numerator sqrt (number) + difference, extracting the integer part from the root
	8. Take out the whole part from the fraction and save it in the array
	9. Ñalculate the remainder (the remainder is stored in i + 1 elements of the array)
	10. Repeat steps 2-10 again (When the last denominator is 1, the period closes and starts again) */

	int period[64] = { 0 }, intDenominator, intNumenator, difference, lastDenominator = 1;
	period[0] = (int)(pow(number, 0.5)); // step 1

	for (int i = 1; i < 64; i++)
	{
		difference = period[0] - period[i]; 
		intDenominator = number - pow(difference, 2); // step  4
		intDenominator = (int)(intDenominator / lastDenominator); // step 5
		lastDenominator = intDenominator; // step 6
		intNumenator = period[0] + difference; // step 7
		period[i] = (int)(intNumenator / intDenominator); // step 8
		period[i + 1] = intNumenator - period[i] * intDenominator; // step 9

		if (lastDenominator == 1)
		{
			period[i + 1] = -1;
			break;
		}

		if (i == 62)
		{
			period[i + 1] = -2;
			break;
		}
	}

	printf("\nInfinite continued fraction representation of the square root of %d: [%d, (", number, period[0]);
	int i = 1;

	while(period[i] != -1 || period[i] != -2)
	{
		printf("%d", period[i]);

		if (period[i+1] == -1)
		{
			printf(")]");
			break;
		}

		if (period[i + 1] == -2)
		{
			printf("...)]");
			break;
		}

		printf(", ");
		i++;
	}

	printf("\nThe '()' indicates period of the continued fraction, which repeats over and over again\n");
	return 0;
}