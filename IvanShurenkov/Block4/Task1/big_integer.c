#include "big_integer.h"

#define MIN(a, b) ((a) < (b) ? (a) : (b))
#define MAX(a, b) ((a) > (b) ? (a) : (b))
#define SQ(x) (x) * (x)

const UINT32 MAX_UINT32 = 4294967295;

INTB *init_integer(INT32 num)
{
	INTB *ret = malloc(sizeof(INTB));
	ret->num = malloc(sizeof(UINT64) * 2);
	ret->cnt = 2;
	for (int i = 0; i < ret->cnt; i++)
		ret->num[i] = 0;
	ret->num[1] = num;
	return ret;
}

void free_integer(INTB *a)
{
	free(a->num);
	free(a);
}

void shift_right(INTB *a, INT32 b)
{
	a->num = realloc(a->num, sizeof(UINT64) * (a->cnt - b / 32));
	INT32 m = b % 32;
	for (int i = a->cnt - 1; i >= 0; i--)
	{
		a->num[i] = a->num[i] >> m;
		if (i > 0)
			a->num[i] += (a->num[i - 1] << (32 - m));
		a->num[i] &= MAX_UINT32;
	}
}

void shift_left(INTB *a, INT32 b)
{
	a->num = realloc(a->num, sizeof(UINT64) * (a->cnt + b / 32));
	a->cnt += b / 32;
	INT32 m = b % 32;
	for (int i = a->cnt - b / 32; i < a->cnt; i++)
	{
		a->num[i] = 0;
	}
	for (int i = 1; i < a->cnt - b / 32; i++)
	{
		a->num[i - 1] += a->num[i] >> (32 - m);
		a->num[i] = (a->num[i] << m);
		a->num[i] &= MAX_UINT32;
	}
	a->num[0] &= MAX_UINT32;
}

void normalize_integer(INTB *a)
{
	int cnt = 0;
	for (int i = 0; i < a->cnt; i++)
	{
		if (a->num[i] != 0)
			break;
		cnt++;
	}
	if (cnt < 1)
	{
		shift_left(a, 32);
		shift_right(a, 16);
		shift_right(a, 16);
	}
	else if (cnt > 1)
	{
		for (int i = 0; i < (cnt - 1); i++)
		{
			shift_left(a, 16);
			shift_left(a, 16);
		}
		a->num = realloc(a->num, sizeof(UINT64) * (a->cnt - cnt + 1));
		a->cnt -= cnt - 1;
	}
}

INTB *add(INTB *a, INTB *b)
{
	INTB *res = init_integer(0);
	res->cnt = MAX(a->cnt, b->cnt);
	res->num = realloc(res->num, res->cnt * sizeof(UINT64));
	int a_it = a->cnt - 1;
	int b_it = b->cnt - 1;
	int it = res->cnt - 1;
	INT32 rem = 0;
	for (; a_it >= 0 || b_it >= 0; a_it--, b_it--, it--)
	{
		res->num[it] = rem;
		if (a_it >= 0)
			res->num[it] += a->num[a_it];
		if (b_it >= 0)
			res->num[it] += b->num[b_it];
		rem = res->num[it] >> 32;
		res->num[it] &= MAX_UINT32;
	}
	if (it != 0 && rem != 0)
		res->num[it - 1] = rem;
	normalize_integer(res);
	return res;
}

void shift_r(INTB *a, INT32 b)
{
	shift_right(a, b);
	normalize_integer(a);
}

void shift_l(INTB *a, INT32 b)
{
	shift_left(a, b);
	normalize_integer(a);
}

void copy(INTB *from, INTB *to)
{
	to->cnt = from->cnt;
	to->num = realloc(to->num, to->cnt * sizeof(UINT64));
	for (int i = 0; i < to->cnt; i++)
		to->num[i] = from->num[i];
}

INTB *mul(INTB *a, INTB *b)
{
	INTB *res = init_integer(0);
	INTB *b_copy = init_integer(0);
	copy(b, b_copy);
	for (int i = a->cnt - 1; i >= 0; i--)
	{
		for (int j = 0; j < 32; j++)
		{
			if (a->num[i] & (1 << j))
			{
				INTB *temp = res;
				res = add(temp, b_copy);
				free_integer(temp);
			}
			shift_l(b_copy, 1);
		}
	}
	free_integer(b_copy);
	normalize_integer(res);
	return res;
}

INTB *bin_pow(INTB *a, INT32 b)
{
	INTB *res = init_integer(1);
	INTB *a_copy = init_integer(0);
	copy(a, a_copy);
	while (b)
	{
		if (b & 1)
		{
			INTB *temp = res;
			res = mul(temp, a_copy);
			free_integer(temp);
		}
		INTB *temp = a_copy;
		a_copy = mul(temp, temp);
		free_integer(temp);
		b >>= 1;
	}
	free_integer(a_copy);
	return res;
}

void print_integer(INTB *a)
{
	printf("0x");
	printf("%0X", (UINT32)a->num[1]);
	for (int i = 2; i < a->cnt; i++)
	{
		printf("%08X", (UINT32)a->num[i]);
	}
}
