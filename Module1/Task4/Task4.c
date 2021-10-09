#include <stdlib.h> 
#include <stdio.h> 
#include <math.h>
#include <stdbool.h>
#include <limits.h>

#define bufer_size  13

bool check_error(double* value)
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

		if ((int)sqrt(*value) * (int)sqrt(*value) == *value)
		{
			printf("Ошибка: Был введен квадратом целого\n");
			return false;
		}

		if (*value < 0)
		{
			printf("Ошибка: введено отрицательное чило.\n");
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


int сalculating_a_fractions(int a, double n, int t)
{

	int subsequence, period = 0;

	double numerator = 1, denominator = 1, q = a;

	printf("Последовательность: %d ", a);

	do
	{
		numerator = (n + q);
		denominator = (t - q * q) / denominator;

		subsequence = (int)(numerator / denominator);
		q = denominator * subsequence - q;

		printf(" %d ", subsequence);

		period++;

	} while ((numerator / denominator != (n + a)));

	return period;
}

int main()
{

	double number = 0;

	system("chcp 1251");
	system("cls");

	printf("%s", "Пожалуйста, введите неквадратное целое число:\n");

	bool status = false;

	do
	{
		status = check_error(&number);

		if (!status)
		{
			printf("Пожалуйста, попробуйте еще раз.\n");
		}

	} while (!status);

	printf("\nПериод =  %d", сalculating_a_fractions((int)sqrt(number), sqrt(number), number));

	return 0;
}
