#include "memorymanager.h"
#include <locale.h>

void intDemonstration(int* pointer);

int main()
{
	setlocale(LC_ALL, "Rus");
	printf("��������� ������������ ������ ��������� ������ memorymanager.h\n\n");
	
	init();

	printf("�������� ������ ����� ����� �� 7 ���������. �������� ������ ������ ����������\n"
		"� ��������� ����� � � ������� ����� - ������ ��� �����.\n");
	
	int* arr = (int*)myMalloc(7 * sizeof(int));
	for (int i = 0; i < 7; i++)
	{
		arr[i] = 7;
	}
	intDemonstration(arr);
	printf("\n\n");

	printf("�������� ��� �� ��� ������� �� 5 ��������. ������ �������� ��������� �������.\n");

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

	printf("������ ������ ������ � ������� ������ - �� 4 ��������.\n"
		"��� �����, �� ������ ����������� �� ����� �������.\n");
	
	myFree(arr1);

	int* arr3 = (int*)myMalloc(4 * sizeof(int));
	for (int i = 0; i < 4; i++)
	{
		arr3[i] = 4;
	}

	intDemonstration(arr); intDemonstration(arr3); intDemonstration(arr2);
	printf("\n\n");

	printf("� ������ � ������� realloc �������� ������ 4-�� �� ���� �������.\n");

	arr3 = myRealloc(arr3, 5 * sizeof(int));

	arr3[4] = 4;

	intDemonstration(arr); intDemonstration(arr3); intDemonstration(arr2);
	printf("\n\n");

	printf("������ ������ 4-�� � �������� �������� ������ ��������� ������ ������.\n");

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