#include <stdlib.h>
#include <stdint.h>
#include <stdio.h>

#define LONG_ARITHMETIC_BASE (16 * 16 * 16 * 16 * 16 * 16)

typedef struct
{
	uint64_t *digits;
	size_t size;
} big_int_t;

void big_int_delete_unused_zeros(big_int_t *number);
void big_int_free(big_int_t *number);
void big_int_print_hex(big_int_t number);
big_int_t *big_int_by_value(uint64_t value);
big_int_t *big_int_mul(big_int_t *left, big_int_t *right);
