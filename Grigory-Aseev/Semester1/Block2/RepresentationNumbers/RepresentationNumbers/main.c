#include <stdio.h>
#include <math.h>

int get_length()
{
	return (sizeof("Grigory") - 1) * (sizeof("Aseev") - 1) * (sizeof("Aleksandrovich") - 1);
}

void bin_negative_int_32(char* string, int length);

void mantissa_float(int n, char* result);

void mantissa_double(int n, char* result);

void order_float(int n, char* result);

void order_double(int n, char* result);

void bin_positive_float(char* string, int length);

void bin_negative_double(char* string, int length);

int main()
{
	printf("The program displays a binary representation of the product of the lengths of my first name, last name and patronymic.\n");
	printf("Output format of numbers equal in absolute value to the desired product: \nA) a negative 32-bit integer.\n");
	printf("B) a positive floating point number of single precision according to the IEEE 754 standard.\n");
	printf("C) a negative double-precision floating-point number according to the IEEE 754 standard.\n");
	int multiplication = get_length();
	char result_a[33];
	bin_negative_int_32(&result_a, multiplication);
	printf("A:\t%s \n", result_a);
	char result_b[33];
	bin_positive_float(&result_b, multiplication);
	printf("B:\t%s \n", result_b);
	char result_c[65];
	bin_negative_double(&result_c, multiplication);
	printf("C:\t%s \n", result_c);
	return 0;
}

void bin_negative_int_32(char* string, int length)
{
	string[32] = '\0'; // The end of string
	for (int i = 31; i >= 0; i--) // The positive binary conversion
	{
		string[i] = (length % 2) + '0';
		length /= 2;
	}
	for (int i = 0; i < 32; i++) // The bit inversion
	{
		string[i] = (string[i] - '0') ^ '1';
	}
	if (string[31] == '0') // Adding a one if the last bit equals zero
	{
		string[31] = '1';
		return;
	}
	for (int i = 31; i >= 0; i--) // Adding a one if the last bit equals one
	{
		if (string[i] == '1')
		{
			string[i] = '0';
		}
		else
		{
			string[i] = '1';
			break;
		}
	}
}

void mantissa_float(int n, char* result)
{
	int length = sizeof(char) * ((int)round(log2(n)) + 1);
	char* binary = (char*)malloc(length);
	binary[length - 1] = '\0';
	for (int i = length - 2; i >= 0; i--)
	{
		binary[i] = (n % 2) + '0';
		n /= 2;
	}
	result[23] = '\0';
	for (int i = 0; i < 23; i++)
	{
		if (i < length - 2)
		{
			result[i] = binary[i + 1];
		}
		else
		{
			result[i] = '0';
		}
	}
	free(binary);
}

void mantissa_double(int n, char* result)
{
	int length = sizeof(char) * ((int)round(log2(n)) + 1);
	char* binary = (char*)malloc(length);
	binary[length - 1] = '\0';
	for (int i = length - 2; i >= 0; i--)
	{
		binary[i] = (n % 2) + '0';
		n /= 2;
	}
	result[52] = '\0';
	for (int i = 0; i < 52; i++)
	{
		if (i < length - 2)
		{
			result[i] = binary[i + 1];
		}
		else
		{
			result[i] = '0';
		}
	}
	free(binary);
}

void order_float(int n, char* result)
{
	int order = 127 + (int)round(log2(n)) - 1;
	result[8] = '\0';
	for (int i = 7; i >= 0; i--)
	{
		result[i] = (order % 2) + '0';
		order /= 2;
	}
}

void order_double(int n, char* result)
{
	int order = 1023 + (int)round(log2(n)) - 1;
	result[11] = '\0';
	for (int i = 10; i >= 0; i--)
	{
		result[i] = (order % 2) + '0';
		order /= 2;
	}
}

void bin_positive_float(char* string, int length)
{
	string[32] = '\0';
	string[0] = '0';
	char mantissa[24], order[9];
	mantissa_float(length, &mantissa);
	order_float(length, &order);
	for (int i = 1; i < 9; i++)
	{
		string[i] = order[i - 1];
	}
	for (int i = 9; i < 32; i++)
	{
		string[i] = mantissa[i - 9];
	}
}

void bin_negative_double(char* string, int length)
{
	string[64] = '\0';
	string[0] = '1';
	char mantissa[53], order[12];
	mantissa_double(length, &mantissa);
	order_double(length, &order);
	for (int i = 1; i < 12; i++)
	{
		string[i] = order[i - 1];
	}
	for (int i = 12; i < 64; i++)
	{
		string[i] = mantissa[i - 12];
	}
}