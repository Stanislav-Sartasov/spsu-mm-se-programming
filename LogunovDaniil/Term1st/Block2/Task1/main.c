#include <stdio.h>
#include <string.h>

#define NAME "Daniil"
#define SURNAME "Logunov"
#define PARNAME "Evgenjevich"
#define NAME_PRODUCT ((int)strlen(NAME) * (int)strlen(SURNAME) * (int)strlen(PARNAME))

int lengthInBinary(int n)
{
	int len = 0;
	while (n > 0)
	{
		len++;
		n /= 2;
	}
	return len;
}

void greetingsMessage()
{
	printf("This program is designed to write binary code of a number,\n");
	printf(" which equals to [%d], the product of its creator name, surname and parname,\n", NAME_PRODUCT);
	printf(" in three different types.\n\n");
}

void intToBinary(int n, char* to, int sizeTo)
{
	int i = sizeTo - 1;
	while (n > 0 && i >= 0)
	{
		if (n % 2 == 1)
			to[i] = '1';
		i--;
		n /= 2;
	}
}

void toNegativeInt32Binary(int n, char* to)
{
	to[0] = '1'; // negative bit
	intToBinary(n - 1, &to[1], 31);
	for (int i = 1; i < 32; i++)
	{
		to[i] = to[i] == '0' ? '1' : '0'; // inverting bits
	}
}

void toFloatBinary(int n, char* to)
{
	to[0] = '0';
	int len = lengthInBinary(n);
	intToBinary(126 + len, &to[1], 8); // setting up exponent value
	intToBinary(n, &to[9], len - 1); // setting up mantissa value
}

void toNegativeDoubleBinary(int n, char* to)
{
	to[0] = '1';
	int len = lengthInBinary(n);
	intToBinary(1022 + len, &to[1], 11);
	intToBinary(n, &to[12], len - 1);
}

int main()
{
	greetingsMessage();

	int nameProduct = NAME_PRODUCT;

	char typeInt[33];
	memset(typeInt, '0', sizeof(char) * 32);
	typeInt[32] = 0;
	toNegativeInt32Binary(nameProduct, typeInt);
	printf("> negative 32bit integer: \n");
	printf("  %s\n\n", typeInt);

	char typeFloat[33];
	memset(typeFloat, '0', sizeof(char) * 32);
	typeFloat[32] = 0;
	toFloatBinary(nameProduct, typeFloat);
	printf("> positive floating-point number with single precision of IEEE754 standart:\n");
	printf("  %s\n\n", typeFloat);

	char typeDouble[65];
	memset(typeDouble, '0', sizeof(char) * 64);
	typeDouble[64] = 0;
	toNegativeDoubleBinary(nameProduct, typeDouble);
	printf("> negative floating-point number with double precision of IEEE754 standart:\n");
	printf("  %s\n\n", typeDouble);

	return 0;
}