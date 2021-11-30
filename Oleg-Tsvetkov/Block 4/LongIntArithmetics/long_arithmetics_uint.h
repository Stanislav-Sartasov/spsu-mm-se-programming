#ifndef LONGARITHMETICS_LONG_ARITHMETICS_UINT_H
#define LONGARITHMETICS_LONG_ARITHMETICS_UINT_H

#define START_LONG_ARITHMETICS_UINT_LENGTH 30
#define DIGIT_SYMBOLS "0123456789abcdef"

struct long_ar_uint
{
	unsigned short int * digits_array;
	int current_length;
	int max_length;
	int base;
};

struct long_ar_uint * initialize_long_ar_uint(int base);

void set_number_for_long_ar_uint(struct long_ar_uint * my_int, unsigned int source_number);

struct long_ar_uint * addition_for_long_ar_uint(struct long_ar_uint * int1, struct long_ar_uint * int2);

struct long_ar_uint *multiply_by_lower_than_base_for_long_ar_uint(struct long_ar_uint *int1, unsigned int int2);

struct long_ar_uint *multiplication_for_long_ar_uint(struct long_ar_uint *int1, struct long_ar_uint *int2);

//Возведёт в степень число long_ar_uint, если степень больше 1.
struct long_ar_uint *pow_for_long_ar_uint(struct long_ar_uint *int1, unsigned int int2);

void free_long_ar_uint(struct long_ar_uint * my_int);

void print_long_ar_uint(struct long_ar_uint * my_int);


#endif //LONGARITHMETICS_LONG_ARITHMETICS_UINT_H
