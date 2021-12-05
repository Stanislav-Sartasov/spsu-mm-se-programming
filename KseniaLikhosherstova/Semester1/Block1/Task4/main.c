#include <stdio.h>
#include <math.h>


int is_square(int n)
{
	int i;
	for (i = 1; i * i <= n; i++)
	{
		if ((int)pow(i, 2) == n)
		{
			return 1;
		}
	}
	return 0;
}



int main()
{
	double input;
	printf("The program outputs a period and the sequence [a0; a1...ai] of continued fraction\n");
	printf("for the entered number that is not the square of another number.\n");
	printf("Please enter the number:\n");
	scanf("%lf", &input);

	int number = (int)floor(input);
	while ((input < 0) || (is_square(number)))
	{
		printf("Invalid input. Enter another number:\n");
		char clean_input = 0;
		while ((clean_input != '\n') && (clean_input != EOF))
			clean_input = getchar();
		scanf("%lf", &input);
		number = (int)floor(input);
	}

	int element, denominator, remains, period, outcome;
	printf("The sequence is:\n[%d", (int)floor(sqrt(number)));
	remains = 0;
	denominator = 1;
	element = (int)floor(sqrt(number));
	outcome = element;
	period = 0;
	do
	{
		remains = outcome * denominator - remains;
		denominator = (number - (int)pow(remains, 2)) / denominator;
		outcome = (element + remains) / denominator;
		printf(", %d", outcome);
		period += 1;
	} while (denominator != 1);
	printf("]\nThe length of the period is %d", period);
	return 0;
}