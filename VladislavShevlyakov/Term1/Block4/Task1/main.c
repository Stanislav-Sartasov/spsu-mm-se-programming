#define BASE 16

#include <stdio.h>
#include <stdlib.h>

struct long_number
{
	int size;
	int* digits;
	int count;
}total;

void multiplication(struct long_number* total)
{
	for (int i = 0; i < total->size; i++)
		total->digits[i] = total->digits[i] * 3;

	for (int i = 0; i < total->size - 1; i++)
	{
		total->digits[i + 1] += total->digits[i] / BASE;
		total->digits[i] %= BASE;
	}

	if (total->digits[total->size - 1] >= BASE)
	{
		total->size++;
		total->digits = (int*)realloc(total->digits, sizeof(int) * total->size);
		total->digits[total->size - 1] = total->digits[total->size - 2] / BASE;
		total->digits[total->size - 2] %= BASE;
	}

	total->count--;

	if (total->count)
		multiplication(total);
}

int main()
{
	total.size = 1;
	total.digits = (int*)malloc(sizeof(int));
	total.digits[0] = 1;
	total.count = 5000;

	printf("The program uses long arithmetic algorithms to calculate the value of the number 3 ^5000 "
		"and it is in the HEX number system.\n\n3^5000 = \n");

	multiplication(&total);

	for (int i = total.size - 1; i >= 0; i--)
		printf("%X", total.digits[i]);

	free(total.digits);
	return 0;
}