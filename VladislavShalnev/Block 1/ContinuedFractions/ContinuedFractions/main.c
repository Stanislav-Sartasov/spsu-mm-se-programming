
#include <stdio.h>
#include <math.h>

long long get_int_part_of_fraction(long long number, long long int_part, long long *remainder, long long *denominator)
{
	long long new_denominator = (number - (int_part - *remainder) * (int_part - *remainder)) / *denominator;

	long long intermediate = int_part * 2 - *remainder;

	long long new_remainder = intermediate % new_denominator;
	long long int_part_of_fraction = intermediate / new_denominator;

	*remainder = new_remainder;
	*denominator = new_denominator;

	return int_part_of_fraction;
}

long long get_int_part_of_sqrt(long long number)
{
	return (long long)sqrt(number);
}

void print_continued_fraction(long long number)
{
	long long int_part = get_int_part_of_sqrt(number);
	long long remainder = 0;
	long long denominator = 1;
	
	printf("Continued fraction of square root of entered number:\n");
	printf("[%lld; %lld", int_part, get_int_part_of_fraction(number, int_part, &remainder, &denominator));

	// remainder and denominator were updated after first print

	long long start_remainder = remainder;
	long long start_denominator = denominator;

	long long period = 1;

	long long new_int_part_of_fraction;

	while (1)
	{
		new_int_part_of_fraction = get_int_part_of_fraction(number, int_part, &remainder, &denominator);

		// catching the fraction cycle
		if (remainder == start_remainder && denominator == start_denominator)
			break;

		// if not caught
		printf("; %lld", new_int_part_of_fraction);
		period++;
	}

	printf("]\n");
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
	printf("This program prints the continued fraction and it's period of square root of entered number.\n");

	long long number;
	input(&number, "Enter a positive integer number that isn't square of another integer: ");
	
	print_continued_fraction(number);

	return 0;
}