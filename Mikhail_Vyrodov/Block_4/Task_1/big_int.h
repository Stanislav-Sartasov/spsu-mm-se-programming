#pragma once
#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>

#define BASE 4294967296;

struct big_int
{
    uint32_t len;
    uint32_t* digits;
};

typedef struct big_int big_int;

big_int power(int32_t number, int32_t degree);

big_int multiply(big_int a, big_int b);

big_int create_big_int(int32_t value, int32_t size);

int32_t index_without_zeros(big_int number);

void print_big_int_hex(big_int number);

