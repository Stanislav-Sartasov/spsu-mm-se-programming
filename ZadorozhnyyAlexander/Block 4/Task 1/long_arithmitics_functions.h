#pragma once
#include <stdio.h>
#include <stdlib.h>

#define BASE 16
#define START_LIM_LENGHT 20

typedef struct {
	int* elements;
	int lenght;
	int lim;
} long_aritcmetic_int;

void init_new_int(long_aritcmetic_int* ar_int, long long input_int, int lim_length);

void print_hexadecimal_long_numbers(long_aritcmetic_int* ar_int);

void free_long_numbers(long_aritcmetic_int* ar_int);

long_aritcmetic_int* addition_long_numbers(long_aritcmetic_int* ar_int_f, long_aritcmetic_int* ar_int_s);

long_aritcmetic_int* multiplication_long_numbers(long_aritcmetic_int* ar_int_f, long_aritcmetic_int* ar_int_s);

long_aritcmetic_int* pow_long_numbers(long_aritcmetic_int* ar_int_f, unsigned int stepen);