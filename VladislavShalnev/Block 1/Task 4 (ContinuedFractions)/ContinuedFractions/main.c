
#include <stdio.h>
#define MAX_RIGHT_BORDER 3037000500

void update_remainder_and_denominator(long long intermediate, long long number, long long int_part, long long* remainder, long long* denominator)
{
	*denominator = (number - (int_part - *remainder) * (int_part - *remainder)) / *denominator;

	// updating remainder using new denominator
	*remainder = intermediate % *denominator;
}

long long get_int_part_of_fraction(long long number, long long int_part, long long *remainder, long long *denominator)
{
	long long intermediate = int_part * 2 - *remainder;
	long long int_part_of_fraction = intermediate / *denominator;

	update_remainder_and_denominator(intermediate, number, int_part, remainder, denominator);

	return int_part_of_fraction;
}

long long get_int_part_of_sqrt(long long number)
{
	long long left_border = 1;

	/*
		max long long = 2 ^ 64 / 2 - 1
		3037000499 < sqrt(max long long) < 3037000500
	*/

	long long right_border = (number < MAX_RIGHT_BORDER) ? number : MAX_RIGHT_BORDER;

	while (right_border - left_border > 1)
	{
		long long middle = (left_border + right_border) / 2;
		if (middle * middle <= number)
			left_border = middle;
		else
			right_border = middle;
	}

	return left_border;
}

void print_continued_fraction(long long number)
{
	long long int_part = get_int_part_of_sqrt(number);
	long long remainder = 0, denominator = 1;
	
	printf("\nContinued fraction of square root of entered number:\n");
	printf("[%lld", get_int_part_of_fraction(number, int_part, &remainder, &denominator) / 2);

	// remainder and denominator were updated after first print
	long long start_remainder = remainder, start_denominator = denominator;

	long long period = 0;

	do
	{
		printf(", %lld", get_int_part_of_fraction(number, int_part, &remainder, &denominator));
		period++;
	} while (remainder != start_remainder || denominator != start_denominator);

	printf("]\n\n");
	printf("The period of this continued fraction is %lld.\n", period);
}

int is_square(long long number)
{
	long long int_part_of_sqrt = get_int_part_of_sqrt(number);

	return number == int_part_of_sqrt * int_part_of_sqrt;
}

void input(long long* adress, char* message)
{
	while (1)
	{
		char input[256];

		printf(message);
		fgets(input, sizeof(input), stdin);

		if (!sscanf_s(input, "%lld", adress))
		{
			printf("Inputed value is not a positive integer.\n");
			continue;
		}
		if (*adress <= 0)
		{
			printf("Inputed number can't be less than zero or equal to zero.\n");
			continue;
		}
		if (is_square(*adress))
		{
			printf("Inputed number can't be square of another integer.\n");
			continue;
		}
		return;
	}
}

int main()
{
	printf("This program prints the continued fraction and it's period of square root of entered number.\n\n");

	long long number;
	input(&number, "Enter a positive integer number that isn't square of another integer: ");
	
	print_continued_fraction(number);

	return 0;
}