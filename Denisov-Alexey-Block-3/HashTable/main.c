#include <stdio.h>
#include "hashtable.h"

int main()
{
	printf("This program demonstrates a work with hash table.\n\n");

	pointer* hashTable = create();

	// Демонстрация добавления элемента и перебалансировки
	srand(time(NULL)); int a;
	for (int i = 0; i < 10; i++)
	{
		a = rand() % 1001;
		add(&hashTable, a);
	}

	print(&hashTable);

	// Демонстрация добавления, удаления и поиска элемента
	int response;
	do
	{
		printf("\nEnter 1 if you want to DELETE some element.\nEnter 2 if you want to ADD some element.\nEnter 3 if you want to SEARCH some element.\nEnter 0 if you want to EXIT.\n");
		scanf_s("%d", &response);

		if (response == 1)
		{
			printf("\nEnter an element you want to delete: ");
			int valueDelete;
			scanf_s("%d", &valueDelete);

			del(&hashTable, valueDelete);
			printf("Done.\n");
			print(&hashTable);
		}
		else if (response == 2)
		{
			printf("\nEnter an element you want to add: ");
			int valueAdd;
			scanf_s("%d", &valueAdd);

			add(&hashTable, valueAdd);
			printf("Done.\n");
			print(&hashTable);
		}
		else if (response == 3)
		{
			printf("\nEnter an element you want to search: ");
			int valueSearch;
			scanf_s("%d", &valueSearch);

			if (search(&hashTable, valueSearch))
			{
				printf("There is such element.\n");
			}
			else
			{
				printf("There is no such element.\n");
			}
		}
		else if (response != 0)
		{
			printf("Incorrect response.\n");
			response = 1;
		}
	} while (response != 0);

	freeTable(&hashTable);

	return 0;
}