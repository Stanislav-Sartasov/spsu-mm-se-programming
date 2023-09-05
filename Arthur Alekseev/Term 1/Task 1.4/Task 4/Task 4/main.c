#include <math.h>
#include <stdio.h>

int get_int_part(int number)
{
	return (int)sqrt(number);
}

int recursion(int period, int integ_part, int divisor, int number, int int_part, int rem, int start_rem, int start_divisor) {
	divisor = (number - (int_part - rem) * (int_part - rem)) / divisor;

	rem = (int_part * 2 - rem) % divisor;
	integ_part = (int_part * 2 - rem) / divisor;

	// returns final period if the fraction is counted for a cycle
	if (divisor == start_divisor && rem == start_rem)
		return period;

	// prints a following number of continued fraction
	printf("; %d", integ_part);
	// it will eventually return period, and every iteration it is increased by one
	return recursion(period + 1, integ_part, divisor, number, int_part, rem, start_rem, start_divisor);
}

// This function prepares all data to start the recursion
void start_counting_continued_fraction(int number)
{
	// integer part of whole root
	int int_part = get_int_part(number);

	int divisor = 1;
	int rem = 0;

	// first iteration is counted to determine the conditions to end the recursion
	divisor = (number - (int_part - rem) * (int_part - rem)) / divisor;
	rem = (int_part * 2 - rem) % divisor;
	int int_part_of_fraction = (int_part * 2 - rem) / divisor;

	printf("%d + [%d", int_part, int_part_of_fraction);

	// variables to determine end of the recursion
	int start_rem = rem;
	int start_divisor = divisor;

	// the recursion itself will return number of iterations, while it is executed, coeffitients will be printed
	printf("]\nIt's period is: %d.\n", recursion(1, int_part, divisor, number, int_part, rem, start_rem, start_divisor));
}

// checks if the given number is suitable
int check_for_suitalbe(int number)
{
	int is_negative = number <= 0;
	int is_square = (int)floor(sqrt(number)) == number;
	return !is_negative && !is_square;
}

int my_scanf_decimal(const char* message)
{
	// Output message
	printf(message);
	int result;
	int scanf_result;
	// Endless loop awaiting user input
	while (1) {
		// Check if scanf was a success
		if (!scanf_s("%d", &result))
		{
			// Skip entire string until new line
			while (getchar() != '\n') {}
		}
		// Check if number is greater than zero
		if (!check_for_suitalbe(result)) {
			printf("Number should be greater than zero, not be an square of an integer and be a number\nInput again:");
			continue;
		}
		// End the loop
		break;
	}
	return result;
}

int main()
{
	printf("This program outputs the coefficients of continued fraction for the given number\n");
	int input = my_scanf_decimal("Please, input number (not a square of integer):");
	start_counting_continued_fraction(input);

	return 0;
}