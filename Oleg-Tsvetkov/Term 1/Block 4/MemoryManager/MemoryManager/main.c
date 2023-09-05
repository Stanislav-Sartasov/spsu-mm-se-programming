#include <stdio.h>
#include <locale.h>
#include "mmanager.h"

//Выводим описание программы
void print_program_description();

//Выделяем память, наполняем массив произвольными элементами, выводим, освобождаем
void my_malloc_demonstration();

//Выделяем память, наполняем массив произвольными элементами, выводим, c помощью realloc добавляем новые, выводим, освобождаем
void my_realloc_demonstration();

int main()
{
	setlocale(LC_ALL, "Russian");
	print_program_description();

	init();

	my_malloc_demonstration();
	my_realloc_demonstration();

	free_init_space();
	return 0;
}

void my_malloc_demonstration()
{
	printf("Демонстрация my_malloc, выделяем память под 8 элементов и выводим, после чего высвобождается.\n");
	int *arr = my_malloc(sizeof(int) * 8);
	for (int i = 0; i < 8; ++i)
	{
		arr[i] = i;
	}

	for (int i = 0; i < 8; ++i)
	{
		printf("%d ", arr[i]);
	}
	printf("\n");

	my_free(arr);
	printf("Элементы были выведены, память освобождена с помощью my_free.\n\n");
}

void my_realloc_demonstration()
{
	printf("Демонстрация my_realloc, для начала выделяем память под 8 элементов и выводим:\n");
	int *arr = my_malloc(sizeof(int) * 8);
	for (int i = 0; i < 8; ++i)
	{
		arr[i] = i;
	}

	for (int i = 0; i < 8; ++i)
	{
		printf("%d ", arr[i]);
	}
	printf("\n");

	printf("Теперь проведём my_realloc, уменьшив размер до 6 элементов:\n");

	arr = my_realloc(arr, sizeof(int) * 6);

	for (int i = 0; i < 6; ++i)
	{
		printf("%d ", arr[i]);
	}

	printf("\nТеперь проведём my_realloc, увеличив размер до 10 элементов, заодно доопределив новые:\n");

	arr = my_realloc(arr, sizeof(int) * 10);

	for (int i = 6; i < 10; ++i)
	{
		arr[i] = i;
	}

	for (int i = 0; i < 10; ++i)
	{
		printf("%d ", arr[i]);
	}

	my_free(arr);
	printf("\nЭлементы были выведены, память освобождена с помощью my_free.\n\n");
}


void print_program_description()
{
	printf("Данная программа представляет собой реализацию менеджера памяти(реализация функций\n");
	printf("my_malloc, my_free, my_realloc, init (единственное место, где можем использовать настоящий malloc))\n\n");
}