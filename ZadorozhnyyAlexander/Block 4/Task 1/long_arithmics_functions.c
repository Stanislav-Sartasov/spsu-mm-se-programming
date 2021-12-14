#pragma once
#include "long_arithmitics_functions.h"

void init_new_int(long_aritcmetic_int* ar_int, long long input_int, int lim_length)
{
	ar_int->lim = lim_length;
	ar_int->elements = (unsigned char*)calloc(lim_length, sizeof(unsigned char));
	int index = 0;

	if (input_int != 0)
	{
		do
		{
			ar_int->elements[index] = input_int % BASE;
			input_int /= BASE;
			index++;
		} while (input_int != 0);
	}
	ar_int->lenght = index;
}

long_aritcmetic_int* addition_long_numbers(long_aritcmetic_int* ar_int_f, long_aritcmetic_int* ar_int_s)
{
	long_aritcmetic_int* result = (long_aritcmetic_int*)malloc(sizeof(long_aritcmetic_int));
	int max_lenght = (ar_int_f->lenght > ar_int_s->lenght ? ar_int_f->lenght : ar_int_s->lenght) + 1;
	init_new_int(result, 0, max_lenght > 20 ? max_lenght : START_LIM_LENGHT);

	for (int i = 0; i < max_lenght - 1; i++)
	{
		int sum = result->elements[i] + (ar_int_f->lenght >= i ? ar_int_f->elements[i] : 0) + (ar_int_s->lenght >= i ? ar_int_s->elements[i] : 0);
		result->elements[i] = sum % BASE;
		result->elements[i + 1] = sum >= BASE ? 1 : 0;
		result->lenght++;
	}

	if (result->elements[max_lenght - 1] == 1)
		result->lenght++;

	return result;
}

long_aritcmetic_int* multiplication_long_numbers(long_aritcmetic_int* ar_int_f, long_aritcmetic_int* ar_int_s)
{
	long_aritcmetic_int* composition = (long_aritcmetic_int*)malloc(sizeof(long_aritcmetic_int));
	int max_lenght = ar_int_f->lenght + ar_int_s->lenght;
	int max_lim = ar_int_f->lim > ar_int_s->lim ? ar_int_f->lim : ar_int_s->lim;
	init_new_int(composition, 0, max_lenght > max_lim ? max_lenght : max_lim);

	for (int i = 0; i < ar_int_f->lenght; i++)
	{
		for (int j = 0; j < ar_int_s->lenght; j++)
		{
			int temp = composition->elements[i + j] + (ar_int_f->elements[i] * ar_int_s->elements[j]);
			composition->elements[i + j] = temp % BASE;
			composition->elements[i + j + 1] += temp / BASE;
		}
	}
	composition->lenght = composition->elements[max_lenght - 1] != 0 ? max_lenght : max_lenght - 1;
	return composition;
}

long_aritcmetic_int* pow_long_numbers(long_aritcmetic_int* ar_int_f, unsigned int stepen)
{
	if (stepen < 2)
		return 0;

	long_aritcmetic_int* res = (long_aritcmetic_int*)malloc(sizeof(long_aritcmetic_int));
	res = ar_int_f;

	for (int i = 1; i < stepen; i++)
	{
		res = multiplication_long_numbers(res, ar_int_f);
	}

	return res;
}

void print_hexadecimal_long_numbers(long_aritcmetic_int* ar_int)
{
	char alphabet[6] = { 'A', 'B', 'C', 'D', 'E', 'F' };

	for (int j = 1; j <= ar_int->lenght; j++)
	{
		int digit = ar_int->elements[ar_int->lenght - j];
		if (digit > 9)
			printf("%c", alphabet[digit - 10]);
		else
			printf("%d", digit);
	}
}

void free_long_numbers(long_aritcmetic_int* ar_int)
{
	free(ar_int->elements);
	free(ar_int);
}
