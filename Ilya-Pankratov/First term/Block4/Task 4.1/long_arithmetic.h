#pragma once

#define MAX_BASE 65536 // max base can't be greater than squareroot(2^(sizeof(unsigned int * 8)))

typedef struct long_number
{
	unsigned int size;
	unsigned int base;
	unsigned int sign;
	unsigned int* digits;
} long_number;

long_number* create_number(int size, int value, int base);

void free_number(long_number* num);

long_number* long_max(long_number* first, long_number* second);

long_number* long_sum(long_number* first, long_number* second);

long_number* column_product(long_number* first, long_number* second, int flag_memory_free);

long_number* long_power(long_number* number, int power);

void print_long_number(long_number* number);
