#include <stdio.h>
#include <stdlib.h>
#include <windows.h>
#include "hashtable.h"

void testingHashTable(struct hashTable* hT)
{
	printf("Filling hashtable with keys from 1 to 70...\n");
	for (int i = 1; i <= 70; i++)
	{
		insertElement(hT, i, i * 50);
	}
	printf("Done!\n\n");

	printf("Getting values with keys from 1 to 70:\n");
	for (int i = 1; i <= 70; i++)
	{
		if (searchElement(hT, i) == 0)
		{
			printf("%d. NULL\t", i);
		}

		else
		{
			printf("%d. %d\t", i, searchElement(hT, i));
		}
	}

	printf("Removing values with keys from 35 to 70...\n");
	for (int i = 35; i <= 70; i++)
	{
		removeElement(hT, i);
	}


	printf("Trying to get values with keys from 30 to 60:\n");
	for (int i = 30; i <= 60; i++)
	{
		if (searchElement(hT, i) == 0)
		{
			printf("%d. NULL\t", i);
		}

		else
		{
			printf("%d. %d\t", i, searchElement(hT, i));
		}

	}
	printf("Testing is over!\n");

}

int main()
{
	printf("The program creates a hash table and demonstrates its functions\n\n");

	printf("Creating the hash table...\n");

	const int size = 70;
	struct hashTable* hT = malloc(size * sizeof(struct hashTable));

	createTable(hT, size);

	printf("Done!\n\n");

	testingHashTable(hT);

	deleteHashTable(hT);

	free(hT);

	system("pause");

	return 0;
}