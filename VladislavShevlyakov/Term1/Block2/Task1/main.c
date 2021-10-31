#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <string.h>
#include <math.h>

void get_degree_of_two(int num, int* exp)
{
	while (num >>= 1)++* exp;
}

void int_to_bin(int num, char* line, int size)
{
	line[0] = num > 0 ? '1' : '0';
	int new_num = num > 0 ? abs(num) - 1 : abs(num);
	for (int i = size - 1; i > 0; i--)
	{
		line[i] = new_num % 2 ? '1' : '0';
		new_num /= 2;
	}
	if (num > 0)
	{
		for (int i = size - 1; i > 0; i--)
		{
			line[i] = line[i] == '0' ? '1' : '0';
		}
	}
	printf("A) %s\n", line);
}

void float_to_bin(int num, char* line, int size)
{
	line[0] = num >= 0 ? '0' : '1';
	int exp = 0;
	num = abs(num);
	get_degree_of_two(num, &exp);
	int old_exp = exp;
	exp += 127;

	for (int i = 8; i > 0; i--)
	{
		line[i] = exp % 2 ? '1' : '0';
		exp /= 2;
	}

	int new_num = num == 0 ? num : num - pow(2, old_exp);
	get_degree_of_two(new_num, &exp);
	int bit_counter = 8;
	if (old_exp - exp > 1)
	{
		bit_counter++;
		line[bit_counter + 1] = '0';
	}

	for (int i = 9 + bit_counter; i > 8; i--)
	{
		line[i] = new_num % 2 ? '1' : '0';
		new_num /= 2;
	}

	for (int i = size - 1; i > 9 + bit_counter; i--)
	{
		line[i] = '0';
	}

	printf("B) %s\n", line);
}

void double_to_bin(int num, char* line, int size)
{
	line[0] = num >= 0 ? '1' : '0';
	int exp = 0;
	num = abs(num);
	get_degree_of_two(num, &exp);
	int old_exp = exp;
	exp += 1023;

	for (int i = 11; i > 0; i--)
	{
		line[i] = exp % 2 ? '1' : '0';
		exp /= 2;
	}

	int new_num = num == 0 ? num : num - pow(2, old_exp);
	get_degree_of_two(new_num, &exp);
	int bit_counter = 12 + exp;
	if (old_exp - exp > 1)
	{
		bit_counter++;
		line[bit_counter + 1] = '0';
	}

	for (int i = bit_counter; i > 11; i--)
	{
		line[i] = new_num % 2 ? '1' : '0';
		new_num /= 2;
	}

	for (int i = size - 1; i > bit_counter; i--)
	{
		line[i] = '0';
	}
	printf("C) %s\n", line);
}

int main()
{
	printf("The program calculates the product of the lengths of my first name,"
		"last name and patronymic and displays a binary representation of the"
		"following values in the specified data formats:\n\n");

	printf("A) A negative 32-bit integer whose modulus is equal to the found product\n");

	printf("B) A positive floating - point number of unit precision according to the"
		"IEEE 754 standard, the modulus of which is equal to the found product\n");

	printf("C) A negative double - precision floating - point number according to the"
		"IEEE 754 standard, the modulus of which is equal to the product found.\n\n");

	int total_size = strlen("Vladislav") * strlen("Shevlyakov") * strlen("Yuryevich");

	char line_int[33] = { 0 };
	char line_float[33] = { 0 };
	char line_double[65] = { 0 };

	int_to_bin(total_size, line_int, 32);
	float_to_bin(total_size, line_float, 32);
	double_to_bin(total_size, line_double, 64);

	return 0;
}