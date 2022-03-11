#include <stdio.h>
#include <locale.h>
#include <string.h>

#define NAME "Олег"
#define SURNAME "Цветков"
#define PATRONYMIC "Вячеславович"


void print_bits_for_int(int number)
{
	char result[33];
	for (int i = 1; i < 32; ++i)
	{
		short bit = number & 1;
		number = number >> 1;
		result[32 - i] = bit ? '1' : '0';
	}
	result[0] = number >= 0 ? '0' : '1';
	result[32] = '\0';

	printf("%s \n", result);
}

void print_bits_for_int_as_float(int number)
{
	char result[33];
	result[0] = number >= 0 ? '0' : '1';
	result[32] = '\0';
	int real_exponent = 127; //В двоичном представлении
	int human_exponent = 0; //В человеческом понимании, без сдвига
	int temp_number = number >= 0 ? number : -number; //Используем модуль копии для вычислений
	while (temp_number > 1)
	{
		temp_number /= 2;
		++human_exponent;
	}
	real_exponent += human_exponent;
	for (int i = 1; i < 32; ++i)
	{
		result[i] = '0';
	}
	for (int i = 0; real_exponent > 0; ++i)
	{
		result[32 - (24 + i)] = (real_exponent % 2) ? '1' : '0';
		real_exponent /= 2;
	}
	temp_number = number >= 0 ? number : -number;
	for (int i = 0; temp_number > 1; ++i)
	{
		result[9 + human_exponent - i - 1] = temp_number % 2 ? '1' : '0';
		temp_number /= 2;
	}

	printf("%s \n", result);
}

void print_bits_for_int_as_double(int number)
{
	char result[65];
	result[0] = number >= 0 ? '0' : '1';
	result[64] = '\0';
	int real_exponent = 1023; //В двоичном представлении
	int human_exponent = 0; //В человеческом понимании, без сдвига
	int temp_number = number >= 0 ? number : -number; //Используем модуль копии для вычислений
	while (temp_number > 1)
	{
		temp_number /= 2;
		++human_exponent;
	}
	real_exponent += human_exponent;
	for (int i = 1; i < 64; ++i)
	{
		result[i] = '0';
	}
	for (int i = 0; real_exponent > 0; ++i)
	{
		result[64 - (53 + i)] = (real_exponent % 2) ? '1' : '0';
		real_exponent /= 2;
	}
	temp_number = number >= 0 ? number : -number;
	for (int i = 0; temp_number > 1; ++i)
	{
		result[12 + human_exponent - i - 1] = temp_number % 2 ? '1' : '0';
		temp_number /= 2;
	}

	printf("%s \n", result);
}

int main()
{
	setlocale(LC_ALL, "Russian");
	printf("Эта программа вычислит произведение длин моего имени, фамилии и отчества\n");
	printf("и выведет его двоичные представления в следующих форматах данных:\n");
	printf("1. Отрицательное 32-битное целое равное по модулю произведению\n");
	printf("2. Положительное число с плавающей запятой единичной точности по стандарту IEEE 754 равное произведению.\n");
	printf("3. Отрицательное число с плавающей запятой двойной точности по стандарту IEEE 754 равное по модулю произведению.\n");
	int length = strlen(NAME) * strlen(SURNAME) * strlen(PATRONYMIC);
	printf("Длина в десятичном формате: %d\n", length);
	printf("1: ");
	print_bits_for_int(-length);
	printf("2: ");
	print_bits_for_int_as_float(length);
	printf("3: ");
	print_bits_for_int_as_double(-length);
	return 0;
}