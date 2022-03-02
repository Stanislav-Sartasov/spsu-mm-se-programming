#include <stdio.h>
#include <string.h>

#define NAME "ALEXANDER"
#define SECOND_HAME "ZADOROZHNYY"
#define PATRONYMIC "SERGEEVICH"
#define FLOAT_ACCURACY 32
#define DOUBLE_ACCURACY 64

int pow_names()
{
	return strlen(NAME) * strlen(SECOND_HAME) * strlen(PATRONYMIC);
}

void digit_to_binary(int digit, char* binary_string, int length)
{
	for (int i = 1; i <= length; i++)
	{
		binary_string[length - i] = digit % 2 ? '1' : '0';
		digit /= 2;
	}
}

void try_to_right_binary(char* exp, int lenght_exp)
{
	while (exp[0] == '0')
	{
		for (int i = 0; i < lenght_exp - 1; i++)
		{
			exp[i] = exp[i + 1];
		}
		exp[lenght_exp - 1] = '0';
	}

}

void print_int_to_negative_binary_system(int digit)
{
	char binary_stroke[33];
	int first_digit = 1;
	digit_to_binary(digit, binary_stroke, 32);

	for (int i = 1; i < 32; i++)
	{
		if (!first_digit)
			binary_stroke[32 - i] = binary_stroke[32 - i] ^ '1' ? '1' : '0';
		if (first_digit & (binary_stroke[32 - i] == '1'))
			first_digit = 0;
	}

	binary_stroke[0] = '1';
	binary_stroke[32] = '\0';
	printf("A) %s\n", binary_stroke);
}

void print_int_to_float_32(int digit)
{
	char binary_stroke[FLOAT_ACCURACY + 1], exp[8], mantissa[FLOAT_ACCURACY + 1];
	int count_digit_binary_system = FLOAT_ACCURACY - 1;
	digit_to_binary(digit, mantissa, FLOAT_ACCURACY);

	for (int i = 1; i < FLOAT_ACCURACY; i++)
	{
		if (mantissa[i] == '1')
		{
			count_digit_binary_system -= i;
			mantissa[i] = '0';
			break;
		}
	}

	digit_to_binary(127 + count_digit_binary_system, exp, 8);
	try_to_right_binary(exp, 8);

	for (int i = 0; i < 8; i++)
	{
		binary_stroke[i + 1] = exp[i];
	}

	for (int i = 0; i < count_digit_binary_system + 2; i++)
	{
		binary_stroke[i + 9] = mantissa[FLOAT_ACCURACY - count_digit_binary_system + i];
	}

	for (int i = count_digit_binary_system + 9; i < FLOAT_ACCURACY; i++)
	{
		binary_stroke[i] = '0';
	}

	binary_stroke[0] = digit > 0 ? '0' : '1';
	binary_stroke[FLOAT_ACCURACY] = '\0';
	printf("B) %s\n", binary_stroke);
}

void print_int_to_double_64(int digit)
{
	char binary_stroke[DOUBLE_ACCURACY + 1], exp[11], mantissa[DOUBLE_ACCURACY + 1];
	int count_digit_binary_system = DOUBLE_ACCURACY - 1;
	digit_to_binary(digit, mantissa, DOUBLE_ACCURACY);

	for (int i = 1; i < DOUBLE_ACCURACY; i++)
	{
		if (mantissa[i] == '1')
		{
			count_digit_binary_system -= i;
			mantissa[i] = '0';
			break;
		}
	}

	digit_to_binary(1023 + count_digit_binary_system, exp, 11);
	try_to_right_binary(exp, 11);

	for (int i = 0; i < 11; i++)
	{
		binary_stroke[i + 1] = exp[i];
	}

	for (int i = 0; i < count_digit_binary_system + 2; i++)
	{
		binary_stroke[i + 12] = mantissa[DOUBLE_ACCURACY - count_digit_binary_system + i];
	}

	for (int i = count_digit_binary_system + 12; i < DOUBLE_ACCURACY; i++)
	{
		binary_stroke[i] = '0';
	}

	binary_stroke[0] = digit > 0 ? '0' : '1';
	binary_stroke[DOUBLE_ACCURACY] = '\0';
	printf("C) %s\n", binary_stroke);
}

int main()
{
	printf("This program calculates the product of the lengths of the my name, surname and patronymic\n"
		"and after show a binary representation of this values in the specified formats:\n"
		"A) a negative 32 - bit integer which module is equal to the found product;\n"
		"B) a positive floating - point number of unit precision according to the IEEE 754 standard\n"
		"which module is equal to the found product;\n"
		"C) a negative floating - point number of double precision according to the IEEE 754 standard\n"
		"which module is equal to the found product.\n\n");
	print_int_to_negative_binary_system(pow_names());
	print_int_to_float_32(pow_names());
	print_int_to_double_64(-pow_names());
}
