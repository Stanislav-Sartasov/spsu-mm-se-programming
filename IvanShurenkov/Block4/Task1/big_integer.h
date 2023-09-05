#include <stdio.h>
#include <stdlib.h>

#ifdef IS_MSVC
typedef __int8 INT8;
typedef __int16 INT16;
typedef __int32 INT32;
typedef __int64 INT64;
typedef unsigned __int8 UINT8;
typedef unsigned __int16 UINT16;
typedef unsigned __int32 UINT32;
typedef unsigned __int64 UINT64;
#else
typedef __int8_t INT8;
typedef __int16_t INT16;
typedef __int32_t INT32;
typedef __int64_t INT64;
typedef __uint8_t UINT8;
typedef __uint16_t UINT16;
typedef __uint32_t UINT32;
typedef __uint64_t UINT64;
#endif


struct big_integer
{
	UINT64 *num;
	int cnt;
};
typedef struct big_integer INTB;

INTB *init_integer(INT32 num);

INTB *add(INTB *a, INTB *b);

void shift_r(INTB *a, INT32 b);

void shift_l(INTB *num, INT32 b);

INTB *mul(INTB *a, INTB *b);

void free_integer(INTB *a);

INTB *bin_pow(INTB* a, INT32 b);

void print_integer(INTB *a);
