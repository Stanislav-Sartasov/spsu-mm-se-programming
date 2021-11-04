#include <stdio.h>
#include "my_hash_table.h"
#include "my_list.h"

int main() 
{
	printf("This program demonstrates my realisation of hash table.\n\nCreating Table: ");
	struct my_hash_table *my_hash_table = create_hash_table();
	printf("done\n");

	printf("Filling table (keys from 1 to 100): ");
	for(int i = 1; i < 101; i++)
		add(my_hash_table, i, 100000 + i * 666 * 420);
	printf("done\n");

	printf("Getting elements (keys from 1 to 109):\n");
	for (int i = 1; i < 110; i++) 
	{
		printf("%d: ", i);
		if (get(my_hash_table, i) != NULL)
			printf("%d\t", get(my_hash_table, i));
		else
			printf("NULL\t");
	}

	printf("\nRemoving elements (keys from 1 to 89): ");
	for (int i = 1; i < 90; i++)
		remove(my_hash_table, i);
	printf("done\n");

	printf("Getting elements again (keys from 80 to 109):\n");
	for (int i = 80; i < 110; i++) 
	{
		printf("%d: ", i);
		if (get(my_hash_table, i) != NULL)
			printf("%d\t", get(my_hash_table, i));
		else
			printf("NULL\t");
	}

	printf("\n\nThe demonstration is over.\n\n");
	return 0;
}
