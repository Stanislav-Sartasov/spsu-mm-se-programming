#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <math.h>

#define NS 16
#define ZEROS 6
#define BASE 16777216  // 16^6

typedef unsigned long long very_long;


struct long_number
{
	int n_digits;
	struct digit* start_digit;
};


struct digit
{
	very_long value;
	struct digit* next_digit;
};


int count_digits(very_long n)
{
	return floor(log(n) / log(NS)) + 1;
}


void initialize_long_number(struct long_number* n)
{
	n->n_digits = 1;
	struct digit* start_digit = malloc(sizeof(struct digit));
	start_digit->value = 0;
	start_digit->next_digit = NULL;
	n->start_digit = start_digit;
}


void destroy_long_number(struct long_number* n)
{
	struct digit* current_digit = n->start_digit;
	struct digit* next_digit = NULL;
	for (int i = 0; i < n->n_digits; i++)
	{
		if (current_digit->next_digit != NULL)
		{
			next_digit = current_digit->next_digit;
		}
		free(current_digit);
		current_digit = next_digit;
	}
}


// short_n < BASE
void multiplication_long_short(struct long_number* long_n, very_long short_n)
{
	struct digit* current_digit = long_n->start_digit;
	very_long current_result = 0;
	very_long remainder = 0;
	for (int i = 0; i < long_n->n_digits; i++)
	{
		current_result = current_digit->value * short_n;
		current_result += remainder;
		remainder = 0;
		if (current_result < BASE)
		{
			current_digit->value = current_result;
		}
		else
		{
			current_digit->value = current_result % BASE;
			remainder = current_result / BASE;
		}
		if (current_digit->next_digit != NULL)
		{
			current_digit = current_digit->next_digit;
		}
	}
	if (remainder > 0)
	{
		struct digit* new_digit = malloc(sizeof(struct digit));
		new_digit->value = remainder;
		new_digit->next_digit = NULL;
		current_digit->next_digit = new_digit;
		long_n->n_digits += 1;
	}
}


// short_n < BASE
void sum_long_short(struct long_number* long_n, very_long short_n)
{
	struct digit* current_digit = long_n->start_digit;
	very_long current_result = 0;
	very_long remainder = 0;
	for (int i = 0; i < long_n->n_digits; i++)
	{
		current_result = current_digit->value + short_n;
		current_result += remainder;
		remainder = 0;
		if (current_result < BASE)
		{
			current_digit->value = current_result;
		}
		else
		{
			current_digit->value = current_result % BASE;
			remainder = current_result / BASE;
		}
		if (current_digit->next_digit != NULL)
		{
			current_digit = current_digit->next_digit;
		}
	}
	if (remainder > 0)
	{
		struct digit* new_digit = malloc(sizeof(struct digit));
		new_digit->value = remainder;
		new_digit->next_digit = NULL;
		current_digit->next_digit = new_digit;
		long_n->n_digits += 1;
	}
}


void print_long_number(struct long_number* n)
{
	very_long* digits = malloc(sizeof(very_long) * n->n_digits);
	struct digit* current_digit = n->start_digit;
	for (int i = 0; i < n->n_digits; i++)
	{
		digits[i] = current_digit->value;
		if (current_digit->next_digit != NULL)
		{
			current_digit = current_digit->next_digit;
		}
	}
	printf("%X", digits[n->n_digits - 1]);
	for (int i = n->n_digits - 2; i >= 0; i--)
	{
		int diff = ZEROS - count_digits(digits[i]);
		for (int j = 0; j < diff; j++)
		{
			printf("0");
		}
		printf("%X", digits[i]);
	}
	free(digits);
}


int main()
{
	printf("This program uses long arithmetic to calculate the value of the expression 3^5000\n");
	printf("and outputs it in hexadecimal notation.\n");
	struct long_number n;
	initialize_long_number(&n);
	sum_long_short(&n, 1);
	for (int i = 0; i < 5000; i++)
	{
		multiplication_long_short(&n, 3);
	}
	printf("0x");
	print_long_number(&n);
	printf("\n");
	destroy_long_number(&n);
}