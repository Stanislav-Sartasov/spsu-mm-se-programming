#include <stdio.h>
#include <math.h>

#define SURNAME "Likhosherstova"
#define NAME "Ksenia"
#define PATRONYMIC "Andreevna"

void negBin(int length)
{
	int bin[32] = { 0 };

	for (int i = 31; i >= 0; i--)
	{
		bin[i] = length % 2;
		length /= 2;
	}

	for (int i = 0; i <= 31; i++)
	{
		if (bin[i] == 0)
		{
			bin[i] = 1;
		}
		else
		{
			bin[i] = 0;
		}

	}

	for (int i = 0; i <= 31; i++)
	{
		printf("%d", bin[i]);
	}

}

void floatBin(int lenght)
{
	int bits[32] = { 0 };

	int border = (int)floor(log2(lenght));
	int exp = 127 + border;
	int man = (int)(lenght - pow(2, border)) / pow(2, border) * pow(2, 23);
	for (int i = 31; i > 9; i--)
	{
		bits[i] = man % 2;
		man /= 2;
	}
	for (int i = 8; i > 0; i--)
	{
		bits[i] = exp % 2;
		exp /= 2;
	}
	for (int i = 0; i <= 31; i++)
	{
		printf("%d", bits[i]);
	}

}


void negDoubleBin(int lenght)
{
	int bits[64] = { 0 };
	bits[0] = 1;
	int border = (int)floor(log2(lenght));
	int exp = 1023 + border;
	long long man = (long long)(lenght - pow(2, border)) / pow(2, border) * pow(2, 52);

	for (int i = 63; i > 12; i--)
	{
		bits[i] = man % 2;
		man /= 2;
	}
	for (int i = 11; i > 0; i--)
	{
		bits[i] = exp % 2;
		exp /= 2;
	}

	for (int i = 0; i < 64; i++)
	{
		printf("%d", bits[i]);
	}

}


int main()
{
	int product = strlen(NAME) * strlen(SURNAME) * strlen(PATRONYMIC);
	printf("This program calculates the product of the lengths of the first name, last nameand patronymic\n");
	printf("and displays a binary representation of the values in the following formats:\n\n");
	printf("A) a negative 32-bit integer, the modulus of which is equal to the found product:\n");
	negBin(product - 1);
	printf("\nB) a positive single-precision floating-point number  according to the IEEE 754 standard: \n");
	floatBin(product);
	printf("\nC) a negative double-precision floating-point number according to the IEEE 754 standard: \n");
	negDoubleBin(product);

	return 0;
}