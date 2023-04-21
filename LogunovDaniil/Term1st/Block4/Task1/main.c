#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define SUBNUM (16)
#define NUM_MAX (8)

unsigned long long numeralBase()
{
	unsigned long long base = 1;
	for (int i = 0; i < NUM_MAX; i++)
		base *= SUBNUM;
	return base;
}

struct integer
{
	int size;
	unsigned int* numerals;
};
typedef struct integer integer;

char lastDigit(unsigned int x)
{
	x %= SUBNUM;
	if (x < 10)
		return '0' + x;
	return 'a' + x - 10;
}

void numeralToString(unsigned int n, char* str)
{
	for (int i = NUM_MAX - 1; i > -1; i--)
	{
		str[i] = lastDigit(n);
		n /= SUBNUM;
	}
}

void integerToString(integer* num, char* str)
{
	for (int i = num->size - 1; i > -1; i--)
	{
		numeralToString(num->numerals[i], str + (num->size - i - 1) * NUM_MAX);
	}
}

void integerPrint(integer* num)
{
	char* print = calloc(num->size * NUM_MAX + 1, sizeof(char));
	if (print == NULL)
	{
		printf("error");
		return;
	}
	integerToString(num, print);
	int nonZero = 0;
	while (nonZero < num->size * NUM_MAX - 1 && print[nonZero] == '0')
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
	unsigned int* nums = calloc(size, sizeof(unsigned int));
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

integer* assignInteger(unsigned int value)
{
	integer* new = integerCreate(1);
	if (new == NULL)
		return NULL;
	new->numerals[0] = value;
	return new;
}

int dropHeadZeros(integer* num)
{
	int nonZero = num->size - 1;
	while (nonZero > 0 && num->numerals[nonZero] == 0)
		nonZero--;
	unsigned int* drop = malloc((nonZero + 1) * sizeof(unsigned int));
	if (drop == NULL)
		return 1;
	for (int i = 0; i <= nonZero; i++)
		drop[i] = num->numerals[i];
	free(num->numerals);
	num->numerals = drop;
	num->size = nonZero + 1;
	return 0;
}

unsigned int integerGetNumeral(integer* num, int id)
{
	if (id >= num->size || id < 0)
		return 0;
	return num->numerals[id];
}

integer* integerMul(integer* a, integer* b)
{
	integer* new = integerCreate(a->size + b->size + 1);
	unsigned long long base = numeralBase();
	if (new == NULL)
		return NULL;
	for (int i = 0; i < a->size; i++)
	{
		unsigned long long carry = 0;
		for (int j = 0; j <= b->size; j++)
		{
			unsigned long long mul = (unsigned long long)a->numerals[i] * integerGetNumeral(b, j)
				+ carry + new->numerals[i + j];
			new->numerals[i + j] = mul % base;
			carry = mul / base;
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