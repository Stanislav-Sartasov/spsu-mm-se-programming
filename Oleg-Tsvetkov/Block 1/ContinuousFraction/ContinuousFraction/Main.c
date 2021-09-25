#include <stdio.h>
#include <locale.h>
#include <math.h>

void clear_excess_chars()
{
	char temp;
	do
	{
		temp = getchar();
	} 
	while (temp != '\n' && temp != EOF);
}

long long get_number()
{
	short success = 0;
	long long result;
	char after_number;
	do
	{
		printf("Введите число: ");
		if (scanf_s("%10lld%c", &result, &after_number, 10) != 2 || after_number != '\n')
		{
			printf("Ошибка! К вводу принимаются только нат. числа. Они также не должны вызывать переполнения\n");
			clear_excess_chars(); //Если не удалось сосканировать число, пропускаем лишние символы до след. строки
		}
		else if (result <= 0)
		{
			printf("Ошибка! Введённое число должно быть больше нуля\n");
		}
		else if (sqrtl(result) - (long long)sqrtl(result) < 0.000001)
		{
			printf("Ошибка! Введённое число не должно быть квадратом целого числа\n");
		}
		else
		{
			success = 1;
		}

	} 
	while (!success);
	return result;
}

int main()
{
	setlocale(LC_ALL, "Russian");
	printf("Описание: Программа получает число от пользователя, берёт корень, и говорит\n");
	printf("период цепной дроби, а также представляет его элементы.\n");
	long long start_number;
	start_number = get_number();
	long long period[50000];
	int period_length = 1;
	period[0] = (long long)sqrtl(start_number);
	long long element, substract = period[0], divider = 1;
	do
	{
		divider = (start_number - substract * substract) / divider;
		element = (long long)((substract + period[0]) / divider);
		period[period_length] = element;
		++period_length;
		substract = element * divider - substract;
	} 
	while (divider != 1);
	printf("Период: %d ", period_length - 1);
	printf("Последовательность: [%lld; ", period[0]);
	for (int i = 1; i < period_length; ++i)
	{
		if (i != period_length - 1)
		{
			printf("%lld,", period[i]);
		}
		else
		{
			printf("%lld", period[i]);
		}
	}
	printf("]");


	return 0;
}
