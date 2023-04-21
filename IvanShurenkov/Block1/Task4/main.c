#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>
#include <math.h>

#ifdef IS_MSVC
#define INT64 __int64
#else
#define INT64 __int64_t
#endif
#define SQ(x) (x) * (x)


INT64 cnt_next_fraction(INT64 *integer_part, INT64 *denominator, INT64 square)
{
	// denominator / (sqrt(square) + integer_part) == (sqrt(square) - integer_part) / ((square - integer_part ^ 2) / denominator)
	// (integer_part + sqrt(square)) / denominator
	*integer_part = (INT64) sqrt(square) - (*integer_part);
	*denominator = (square - SQ(*integer_part)) / (*denominator);
	INT64 ret = (INT64)(((*integer_part) + (INT64) sqrt(square)) / (*denominator));
	*integer_part = ((*integer_part) + (int) sqrt(square)) % (*denominator);
	return ret;
}


int main()
{
	INT64 n = 0;
	printf("Continued fraction\n"
		   "This programm calculates the continue fraction square root of the positive integer, which is not square.\n"
		   "Enter positive integer, which is not square:\n");
	while (1)
	{
		int correctly_scan;
		correctly_scan = scanf("%lld", &n);
		if (correctly_scan == 1 && n > 0)
		{
			if (SQ((INT64) sqrt(n)) != n)
				break;
			else
			{
				printf("Your number is square!\nTry again:\n");
			}
		}
		else
		{
			while (fgetc(stdin) != '\n')
				;
			printf("It's not positive number\nTry again:\n");
		}
	}

	printf("Continued fraction of square root given number: [%lld", (INT64) sqrt(n));
	INT64 integer_part = 0, denominator = 1;
	INT64 out = cnt_next_fraction(&integer_part, &denominator, n);
	INT64 first_int_part = integer_part, first_denominator = denominator;
	int circle_size = 0;
	do
	{
		printf(", %lld", out);
		circle_size++;
		out = cnt_next_fraction(&integer_part, &denominator, n);
	}
	while (first_int_part != integer_part || first_denominator != denominator);
	printf("]\nThe circle size of this fraction is %lld", circle_size);
	return 0;
}