#ifndef BIGINT_H
#define BIGINT_H

#define BASE 4294967296 // 2^32

typedef struct
{
	unsigned int* digits;
	unsigned int length;
} bigint_t;

bigint_t* new_bigint(unsigned int initial);

void free_bigint(bigint_t* bigint);

bigint_t* multiply_bigint(bigint_t* bigint, unsigned int multiplier);

void print_hex_bigint(bigint_t* bigint);

#endif // BIGINT_H