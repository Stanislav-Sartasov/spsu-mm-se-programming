#include <stdio.h>
#include "hash_table.h"

int main()
{
	printf("This program shows the power of hash tables.\n\n");

	struct hash_table* new_hash_table = init();

	for (int i = 1; i < 40; i++)
	{
		insert(new_hash_table, i, i * 1337);
	}

	printf("The table is filled with keys from 1 to 40.\n");
	printf("How many linked lists are in the hash table? The answer is %d\n\n", new_hash_table->module);
	printf("Then add a few more elements, let the total be 50\n");

	for (int i = 40; i < 50; i++)
	{
		insert(new_hash_table, i, i * 1337);
	}

	printf("How many linked lists are in the hash table? The answer is %d\n", new_hash_table->module);
	printf("Some of the baskets received more than ten values and there was a rebalance\n\n");

	printf("Let's delete some values.\n");
	printf("First, let's look at the initial table and some values that we did not fill.\n\n");
	for (int i = 0; i < 60; i++)
	{
		if (get(new_hash_table, i) == NULL)
			printf("Key: %d, value: NOTFOUND\t", i);
		else
			printf("Key: %d, value: %d\t", i, get(new_hash_table, i));
	}

	printf("\nDeleting values with keys 30 to 40...\n");
	printf("Deleting a nonexistent value under the key 59...\n\n");

	for (int i = 30; i < 41; i++)
	{
		del(new_hash_table, i);
	}

	del(new_hash_table, 59);

	for (int i = 0; i < 60; i++)
	{
		if (get(new_hash_table, i) == NULL)
			printf("Key: %d, value: NOTFOUND\t", i);
		else
			printf("Key: %d, value: %d\t", i, get(new_hash_table, i));
	}

	printf("\n\nThe values have been deleted. The nonexistent value has not been changed.\n");
	printf("That's the power of hash-tables!\n");

	return 0;
}