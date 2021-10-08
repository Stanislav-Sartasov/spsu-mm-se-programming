#include <stdlib.h> 
#include <stdio.h> 
#include <math.h>
#include <stdbool.h>
#include <limits.h>
#define bufer_size 12

int check_number(int a, int b)
{

	if (a > 0 || b > 0)
	{
		if (b > a)
		{
			int temp = b;
			b = a;
			a = temp;
		}

		if ((a % b) == 0)
		{
			return b;
		}
		else
		{
			return check_number(b, a % b);
		}
	}
	else
	{
		return false;
	}

}

bool check_pifagor(int a, int b, int c)
{
	if (a > 0 && b > 0 && c > 0)
	{

		if (pow(a, 2) + pow(b, 2) == pow(c, 2) || pow(c, 2) + pow(b, 2) == pow(a, 2) || pow(a, 2) + pow(c, 2) == pow(b, 2))
		{
			return true;
		}
	}
	return false;
}

bool check_error(int* value)
{
	size_t length = 0;
	char* end = NULL;
	char buf[bufer_size] = "";

	fflush(stdout);

	if (!fgets(buf, sizeof(buf), stdin))
	{
		return false;
	}


	length = strlen(buf);

	if (buf[length - 1] == '\n')
	{
		buf[--length] = '\0';

		errno = 0;

		*value = strtod(buf, &end);

		if (length == 0)
		{
			printf("Ошибка: введена пустая строка.\n");
			return false;
		}

		if (errno != 0 || *end != '\0')
		{
			printf("Ошибка: некорректный символ.\n");
			printf("\t%s\n", buf);
			printf("\t%*c\n", (int)(end - buf) + 1, '^');

			return false;
		}

		if (*value < 0)
		{
			printf("Ошибка: введено отрицательное число\n");
			return false;
		}

		if (INT_MAX < *value || INT_MIN >= *value)
		{
			printf("Ошибка: целочисленное переполнение параметра .\n");
			return false;
		}

	}
	else
	{

		scanf_s("%*[^\n]");
		scanf_s("%*c");
		printf("Ошибка: не вводите больше чем %d символа(ов).\n", bufer_size - 2);

		return false;
	}

	return true;
}

void check_enter(int* cislo)
{
	bool status = false;

	do
	{
		status = check_error(cislo);

		if (!status)
		{
			printf("Пожалуйста, попробуйте еще раз.\n");
		}

	} while (!status);
}

int main()
{
	system("chcp 1251");
	system("cls");

	int a = 0, b = 0, c = 0;

	printf("Пожалуйста, задайте тройку натуральных чисел: \n");
	printf("a = "); check_enter(&a);
	printf("b = "); check_enter(&b);
	printf("c = "); check_enter(&c);
	printf("Результат анализ введенных значений: \n");

	if (check_pifagor(a, b, c))
	{
		printf("1. ДА, заданная тройка натуральных чисел (a,b,c) удовлетворяет условию x^2 + y^2 = z^2.\n");
		printf("Результат: %d^2 + %d^2 = %d^2 \n", a, b, c);

		if (check_number(check_number(a, b), c) == 1)
		{
			printf("2. ДА, заданная тройка натуральных чисел (a,b,c) является ппростой пифагоровой тройкой\n");
		}
		else
		{
			printf("2. НЕТ, заданная тройка натуральных чисел (a,b,c) не является простой пифагоровой тройкой\n");
		}
	}
	else
	{
		printf("1. Нет, заданная тройка натуральных чисел (a,b,c) не удовлетворяет условию x^2 + y^2 = z^2.\n");
		printf("Результат: %d^2 + %d^2 <> %d^2", a, b, c);
	}

	return 0;
}

