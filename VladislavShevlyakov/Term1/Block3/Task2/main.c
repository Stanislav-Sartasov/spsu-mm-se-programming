#include <stdio.h>
#include "hash_table.h"

int main()
{
	struct list hash_table = initialize_the_structure();

	printf("The program is a hash table that has functions such as inserting a value,"
		"deleting a key, outputting values by key and rebalancing the table.\n\n");

	printf("The principle of operation of a hash table is that when it reaches 80 percent"
		"fullness, the hash function(mod (prime number, where the original prime number is 13))"
		"changes to a new hash function (mod (fifth largest prime number)).\n\n");

	random_filling(&hash_table);
	actions_on_the_table(&hash_table);

	for (int i = 0; i < hash_table.tab_size; i++)
		free(hash_table.table[i]);
	free(hash_table.table);
	free(hash_table.sizes);

	return 0;
}