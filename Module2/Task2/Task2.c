#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>

#define bufer_size  13


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
void zapolenie_vozmozniv_varions(int* a)
{
	a[0] = 1;
	a[1] = 2;
	a[2] = 5;
	a[3] = 10;
	a[4] = 20;
	a[5] = 50;
	a[6] = 100;
	a[7] = 200;
}

int main()
{
	system("chcp 1251");
	system("cls");

	int enter_znach;
	bool status = false;
	do
	{
		status = check_error(&enter_znach);

		if (!status)
		{
			printf("Пожалуйста, повторите попытку: \n");
		}
	} while (!status);

	if (enter_znach == 0)
	{
		printf("К сожелению, ноль не возможно разменять!");
	}
	else if (enter_znach == 1)
	{
		printf("Количество возможных вариантов %d для размена %d пенсов", 1, enter_znach);
	}
	else if (enter_znach == 2)
	{
		printf("Количество возможных вариантов %d для размена %d пенсов", 1, enter_znach);
	}
	else
	{
		int* varians_monet = malloc(8 * sizeof(int));
		zapolenie_vozmozniv_varions(varians_monet);

		int** variants = malloc(enter_znach * sizeof(int));
		for (int i = 0; i < enter_znach; i++)
		{
			variants[i] = malloc(8 * sizeof(int));
		}

		for (int i = 0; i < enter_znach; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				if (j == 0)
				{
					variants[i][j] = 1;
				}
				else
				{
					variants[i][j] = 0;
				}
			}
		}


		for (int j = 1; j < 8; j++)
		{
			for (int i = 0; i < enter_znach; i++)
			{
				for (int k = 0; k <= i; k += varians_monet[j])
				{
					variants[i][j] += variants[i - k][j - 1];
				}
			}
		}
		
		printf("Количество возможных вариантов %d для размена %d пенсов", variants[enter_znach - 1][7], enter_znach);

		free(varians_monet);
		for (int i = 0; i < enter_znach; i++)
		{
			free(variants[i]);
		}
	}
}