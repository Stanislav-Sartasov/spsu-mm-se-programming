//
// Created by Вячеслав Бучин on 26.11.2021.
//

#ifndef LONG_ARITHMETIC_BIG_INT_H
#define LONG_ARITHMETIC_BIG_INT_H

#include <inttypes.h>

static const uint32_t BIG_INTEGER_BASE = 16 * 16 * 16 * 16 * 16 * 16 * 16;

typedef struct big_int {
	uint32_t* digits;
	uint32_t size;
} big_int_t;

big_int_t* big_int_by_value(uint64_t value);

char* big_int_to_string(big_int_t* number);

void big_int_free(big_int_t* number);

/**
 * Adds right number to the left one
 * @return pointer to left
 */
big_int_t* big_int_add(big_int_t* left, big_int_t* right);

#endif //LONG_ARITHMETIC_BIG_INT_H
