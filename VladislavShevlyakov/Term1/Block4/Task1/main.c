#define BASE 1073741824 // 2^30

#include <stdio.h>
#include <stdlib.h>

struct long_number
{
	unsigned int size;
	unsigned int* digits;
};

void multiplication(struct long_number* total)
{
	unsigned int i = 0;
	unsigned int current;
	unsigned int carry = 0;
	while (i < total->size || carry)
	{
		if (i == total->size)
		{
			total->size++;
			total->digits = (unsigned int*)realloc(total->digits, sizeof(unsigned int) * total->size);
			total->digits[total->size - 1] = 0;
		}
		current = carry + total->digits[i] * 3;
		total->digits[i] = current % BASE;
		carry = current / BASE;
		i++;
	}
}

void dec_hex(unsigned int n)
{
	unsigned int x;
	if (n != 0)
	{
		x = n % 16;
		dec_hex(n /= 16);
		printf("%c", x < 10 ? x + '0' : x - 10 + 'A');
	}
}

int main()
{
	struct long_number total;
	total.size = 1;
	total.digits = (unsigned int*)malloc(sizeof(unsigned int));
	total.digits[0] = 1;

	printf("The program uses long arithmetic algorithms to calculate the value of the number 3^5000 "
		"and it is in the HEX number system.\n\n3^5000 = \n");

	for(int i = 1;i<=5000;i++)
		multiplication(&total);

	for (int i = total.size - 1; i >= 0; i--)
		dec_hex(total.digits[i]);

	free(total.digits);

	return 0;
}