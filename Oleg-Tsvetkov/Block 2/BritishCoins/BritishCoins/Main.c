#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <locale.h>
#include <malloc.h>

void clear_excess_chars()
{
	char temp;
	do
	{
		temp = getchar();
	} while (temp != '\n' && temp != EOF);
}

long long get_number()
{
	short success = 0;
	long long result;
	char after_number;
	do
	{
		printf("Введите число: ");
		if (scanf("%10lld%c", &result, &after_number) != 2 || after_number != '\n')
		{
			printf("Ошибка! К вводу принимаются только нат. числа.\n");
			clear_excess_chars(); //Если не удалось сосканировать число, пропускаем лишние символы до след. строки
		}
		else if (result <= 0 || result > 30000)
		{
			printf("Ошибка! Введённое число должно быть больше нуля и не больше 30000.\n");
		}
		else
		{
			success = 1;
		}

	} while (!success);
	return result;
}

int main()
{
	setlocale(LC_ALL, "Russian");
	printf("Описание: В Англии в обращении находятся монеты следующего достоинства: 1 пенс, 2 пенса, 5 пенсов, 10 пенсов, 20 пенсов,\n");
	printf("50 пенсов, 1 фунт (100 пенсов) и 2 фунта (200 пенсов). Программа принимает количество пенсов N до 30000,\n");
	printf("и выводит количество способов, которым можно набрать представленную сумму денег.\n");
	long long money_amount;
	money_amount = get_number();
	const int money_options[8] = { 1, 2, 5, 10, 20, 50, 100, 200 };
	/*
	Решим задачу методом динамического программирования. Создадим таблицу table размером 8x[сумма денег] ([сумма денег] столбцов и 8 строк),
	Пусть в ячейке table[line][column] номер строки отвечает за макс. возможную стоимость единицы денег для расчета(к примеру, 5 пенсов),
	столбец - количество денег, которое нужно набрать. Тогда значение ячейки будет хранить количество способов, которым
	можно получить сумму при заданных условиях.
	*/
	long long* table[8];
	for (int i = 0; i < 8; ++i)
	{
		table[i] = (long long*)malloc((money_amount + 1) * sizeof(long long));
		table[i][0] = 1;
	}
	for (long long i = 1; i < money_amount + 1; ++i)
	{
		table[0][i] = 1;
	}
	for (int i = 1; i < 8; ++i)
	{
		for (long long j = 1; j < money_amount + 1; ++j)
		{
			if (money_options[i] > j)
			{
				table[i][j] = table[i - 1][j];
			}
			else
			{
				table[i][j] = table[i - 1][j] + table[i][j - money_options[i]];
			}
		}
	}
	printf("Ответ: %lld", table[7][money_amount]);

	for (int i = 0; i < 8; ++i)
	{
		free(table[i]);
	}
	return 0;
}