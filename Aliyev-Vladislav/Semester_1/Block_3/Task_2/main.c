#include <stdio.h>
#include "ht.h"

int main()
{
	int num = 999;
	printf("This program shows the operation of the implemented hash table.\n\n");

	struct hash_table* new_hash_table = init();

	for (int n = 1; n < 15; n++)
	{
		insert(new_hash_table, n, num * n);
	}

	printf("The table is filled with keys[1; 15]\n");
	printf("Let's add a few more elements, let there be a total of 40\n");

	for (int n = 15; n < 30; n++)
	{
		insert(new_hash_table, n, num * n);
	}
	printf("There was a rebalance.\n\n");

	printf("Some more values removed.\n");
	for (int n = 0; n < 40; n++)
	{
		if (get(new_hash_table, n) == NULL)
			printf("\n Key: %d   value: NOTFOUND\t", n);
		else
			printf("\n Key: %d   value:  %d\t", n, get(new_hash_table, n));
	}

	printf("\n\nDeleting values with keys [5; 15].\n");
	printf("Deleting a nonexistent value under the key 39.\n\n");

	for (int n = 5; n < 16; n++)
	{
		del(new_hash_table, n);
	}

	del(new_hash_table, 39);

	for (int n = 0; n < 40; n++)
	{
		if (get(new_hash_table, n) == NULL)
			printf("\n Key: %d   value: NOTFOUND\t", n);
		else
			printf("\n Key: %d   value: %d\t", n, get(new_hash_table, n));
	}

	printf("\n\nThe values have been deleted. The nonexistent value has not been changed.\n");

	return 0;
}