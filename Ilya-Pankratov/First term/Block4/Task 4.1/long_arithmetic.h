#pragma once

typedef struct long_number
{
	unsigned int size;
	unsigned int base;
	unsigned int sign;
	int* digits;
} long_number;

long_number* create_number(int size, int value, int base);

long_number* copy_long_number(long_number* number);

void cut_empety_digits(long_number* number);

long_number* normalize(long_number* number);

void free_number(long_number* num);

int long_comparator(long_number* first, long_number* second);

long_number* long_max(long_number* first, long_number* second);

long_number* long_sum(long_number* first, long_number* second);

long_number* column_product(long_number* first, long_number* second, int flag_memory_free);

long_number* long_power(long_number* number, int power);

void print_long_number(long_number* number);
