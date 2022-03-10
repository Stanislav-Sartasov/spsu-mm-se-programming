#include <stdio.h>

void integer_output(int number)
{
	unsigned int result = *(unsigned int*)&number;
	char string[33] = { 0 };
	for (int i = 0; i < 32; i++)
	{
		string[31 - i] = result % 2 ? '1' : '0';
		result /= 2;
	}
	printf("Integer (%d): %s\n", number, string);
}

void float_output(float number)
{
	unsigned int result = *(unsigned int*)&number;
	char string[33] = { 0 };
	for (int i = 0; i < 32; i++)
	{
		string[31 - i] = result % 2 ? '1' : '0';
		result /= 2;
	}
	printf("Float: (%f) %s\n", number, string);
}

void double_output(double number)
{
	unsigned long long result = *(unsigned long long*) & number;
	char string[65] = { 0 };
	for (int i = 0; i < 64; i++)
	{
		string[63 - i] = result % 2 ? '1' : '0';
		result /= 2;
	}
	printf("Double (%lf): %s\n", number, string);
}

int main()
{
	// Алексеев Артур Игоревич
	int mult = 8 * 5 * 8;

	printf("This program outputs the binary of mutiplication of my full name in some formats.\n\n");

	int a = -mult;
	float b = mult;
	double c = -mult;

	integer_output(a);
	float_output(b);
	double_output(c);
}