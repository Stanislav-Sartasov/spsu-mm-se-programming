#include "memorymanager.h"
#include <locale.h>

void intDemonstration(int* pointer);

int main()
{
	setlocale(LC_ALL, "Rus");
	printf("Программа демонстирует работу менеджера памяти memorymanager.h\n\n");
	
	init();

	printf("Создадим массив целых чисел на 7 элементов. Менеджер памяти хранит информацию\n"
		"о занятости блока и о размере блока - первые две цифры.\n");
	
	int* arr = (int*)myMalloc(7 * sizeof(int));
	for (int i = 0; i < 7; i++)
	{
		arr[i] = 7;
	}
	intDemonstration(arr);
	printf("\n\n");

	printf("Создадим ещё по два массива на 5 элеметов. Память выглядит следующим образом.\n");

	int* arr1 = (int*)myMalloc(5 * sizeof(int));
	for (int i = 0; i < 5; i++)
	{
		arr1[i] = 5;
	}

	int* arr2 = (int*)myMalloc(5 * sizeof(int));
	for (int i = 0; i < 5; i++)
	{
		arr2[i] = 5;
	}
	
	intDemonstration(arr); intDemonstration(arr1); intDemonstration(arr2);
	printf("\n\n");

	printf("Удалим второй массив и добавим другой - на 4 элемента.\n"
		"Как видно, он удачно разместился на месте второго.\n");
	
	myFree(arr1);

	int* arr3 = (int*)myMalloc(4 * sizeof(int));
	for (int i = 0; i < 4; i++)
	{
		arr3[i] = 4;
	}

	intDemonstration(arr); intDemonstration(arr3); intDemonstration(arr2);
	printf("\n\n");

	printf("А теперь с помощью realloc расширим массив 4-ок на один элемент.\n");

	arr3 = myRealloc(arr3, 5 * sizeof(int));

	arr3[4] = 4;

	intDemonstration(arr); intDemonstration(arr3); intDemonstration(arr2);
	printf("\n\n");

	printf("Удалим массив 4-ок и попросим менеджер памяти увеличить первый массив.\n");

	myFree(arr3);
	
	arr = myRealloc(arr, 14 * sizeof(int));
	for (int i = 7; i < 14; i++)
	{
		arr[i] = 7;
	}

	intDemonstration(arr); intDemonstration(arr2);
	printf("\n\n");

	deinit();

	return 0;
}

void intDemonstration(int* pointer)
{
	block* blockInfo = (block*)((int)pointer - sizeof(block));
	printf("%d %d ", blockInfo->isOccupied, blockInfo->size);

	for (int i = 0; i < (int)((blockInfo->size - sizeof(block)) / sizeof(int)); i++)
	{
		printf("%d ", pointer[i]);
	}
}