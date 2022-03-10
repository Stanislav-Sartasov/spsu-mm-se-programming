#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <string.h>
#include <math.h>
#define NAME "Danila"
#define PATRONYMIC "Evgenevich"
#define SURNAME "Pechenev"


int count_product()
{
	return strlen(NAME) * strlen(PATRONYMIC) * strlen(SURNAME);
}


void to_bin_negative_int32(int n, char* result)
{
	for (int i = 1; i <= 32; i++)
	{
		result[32 - i] = "01"[n % 2];
		n /= 2;
	}
	result[32] = 0;  // Null-terminated string

	result[0] = '1';  // Negative 

	int first_one = 1;
	for (int i = 1; i <= 31; i++)
	{
		result[32 - i] = first_one ? result[32 - i] : "01"[result[32 - i] ^ '1'];
		if ((result[32 - i] == '1') & first_one)
		{
			first_one = 0;
		}
	}
}


int length_of_binary_number(int n)
{
	return (int)(log10(n) / log10(2)) + 1;
}


void to_bin_positive_float(int n, char* result)
{
	result[0] = '0';  // Positive 

	int length = length_of_binary_number(n);

	int exponent = length + 126;
	for (int i = 0; i < 8; i++)
	{
		result[8 - i] = "01"[exponent % 2];
		exponent /= 2;
	}

	for (int i = 1; i <= length - 1; i++)
	{
		result[length - i + 8] = "01"[n % 2];
		n /= 2;
	}

	for (int i = length + 8; i < 32; i++)
	{
		result[i] = '0';
	}

	result[32] = 0;  // Null-terminated string
}


void to_bin_negative_double(int n, char* result)
{
	result[0] = '1';  // Negative

	int length = length_of_binary_number(n);

	int exponent = length + 1022;
	for (int i = 0; i < 11; i++)
	{
		result[11 - i] = "01"[exponent % 2];
		exponent /= 2;
	}

	for (int i = 1; i <= length - 1; i++)
	{
		result[length - i + 11] = "01"[n % 2];
		n /= 2;
	}

	for (int i = length + 11; i < 64; i++)
	{
		result[i] = '0';
	}

	result[64] = 0;  // Null-terminated string
}


int main()
{
	printf("This program calculates the product of the lengths of the name, surname and patronymic of its creator\n");
	printf("and displays a binary representation of the following values in the specified data formats:\n");
	printf("A) a negative 32 - bit integer which module is equal to the found product;\n");
	printf("B) a positive floating - point number of unit precision according to the IEEE 754 standard\n");
	printf("which module is equal to the found product;\n");
	printf("C) a negative floating - point number of double precision according to the IEEE 754 standard\n");
	printf("which module is equal to the found product.\n");
	printf("\n");

	int product = count_product();

	char result_a[32 + 1];
	to_bin_negative_int32(product, &result_a);
	printf("A) %s\n", result_a);

	char result_b[32 + 1];
	to_bin_positive_float(product, &result_b);
	printf("B) %s\n", result_b);

	char result_c[64 + 1];
	to_bin_negative_double(product, &result_c);
	printf("C) %s\n", result_c);
}