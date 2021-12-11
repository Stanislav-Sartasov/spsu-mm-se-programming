#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define SUBNUM (16)
#define NUM_MAX (SUBNUM * SUBNUM * SUBNUM)

struct integer
{
	int size;
	int* numerals;
};
typedef struct integer integer;

char lastDigit(int x)
{
	x %= SUBNUM;
	if (x < 10)
		return '0' + x;
	return 'a' + x - 10;
}

void numeralToString(int n, char* str)
{
	str[0] = lastDigit(n / SUBNUM / SUBNUM);
	str[1] = lastDigit(n / SUBNUM);
	str[2] = lastDigit(n);
}

void integerToString(integer* num, char* str)
{
	for (int i = num->size - 1; i > -1; i--)
	{
		numeralToString(num->numerals[i], str + (num->size - i - 1) * 3);
	}
}

void integerPrint(integer* num)
{
	char* print = calloc(num->size * 3 + 1, sizeof(char));
	if (print == NULL)
	{
		printf("error");
		return;
	}
	integerToString(num, print);
	int nonZero = 0;
	while (nonZero < num->size * 3 - 1 && print[nonZero] == '0')
		nonZero++;
	printf("%s", print + nonZero);
	free(print);
}

integer* integerCreate(int size)
{
	size = max(0, size);
	integer* new = malloc(sizeof(integer));
	if (new == NULL)
		return NULL;
	int* nums = calloc(size, sizeof(int));
	if (nums == NULL && size > 0)
	{
		free(new);
		return NULL;
	}
	new->size = size;
	new->numerals = nums;
	return new;
}

void integerDestroy(integer* toDestroy)
{
	free(toDestroy->numerals);
	free(toDestroy);
}

int countLength(int value)
{
	int res = 0;
	value = abs(value);
	while (value > 0)
	{
		value /= NUM_MAX;
		res++;
	}
	return res;
}

integer* assignInteger(int value)
{
	value = max(value, 0);
	int size = countLength(value);
	integer* new = integerCreate(size);
	if (new == NULL)
		return NULL;
	for (int i = 0; i < size; i++)
	{
		new->numerals[i] = value % NUM_MAX;
		value /= NUM_MAX;
	}
	return new;
}

int dropHeadZeros(integer* num)
{
	int nonZero = num->size - 1;
	while (nonZero > 0 && num->numerals[nonZero] == 0)
		nonZero--;
	int* drop = malloc((nonZero + 1) * sizeof(int));
	if (drop == NULL)
		return 1;
	for (int i = 0; i <= nonZero; i++)
		drop[i] = num->numerals[i];
	free(num->numerals);
	num->numerals = drop;
	num->size = nonZero + 1;
	return 0;
}

int integerGetNumeral(integer* num, int id)
{
	if (id >= num->size || id < 0)
		return 0;
	return num->numerals[id];
}

integer* integerMul(integer* a, integer* b)
{
	integer* new = integerCreate(a->size + b->size + 1);
	if (new == NULL)
		return NULL;
	for (int i = 0; i < a->size; i++)
	{
		int carry = 0;
		for (int j = 0; j <= b->size; j++)
		{
			int mul = a->numerals[i] * integerGetNumeral(b, j) + carry + new->numerals[i + j];
			new->numerals[i + j] = mul % NUM_MAX;
			carry = mul / NUM_MAX;
		}
	}
	dropHeadZeros(new);
	return new;
}

integer* integerPow(integer* base, int deg)
{
	if (deg <= 0)
		return assignInteger(1);
	integer* half = integerPow(base, deg / 2);
	integer* full = integerMul(half, half);
	integer* rem = integerMul(full, base);
	integer* res;
	if (deg % 2 == 0)
	{
		res = full;
		integerDestroy(rem);
	}
	else
	{
		res = rem;
		integerDestroy(full);
	}
	integerDestroy(half);
	return res;
}

void greetingsMessage()
{
	printf("This program calculates the value of 3 in the power of 5000\n");
	printf(" using long arithmetics and prints the result in hex:\n\n");
}

int main()
{
	greetingsMessage();
	integer* three = assignInteger(3);
	integer* res = integerPow(three, 5000);
	integerPrint(res);
	integerDestroy(three);
	integerDestroy(res);

	return 0;
}