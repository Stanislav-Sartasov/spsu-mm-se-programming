#include <stdlib.h>
#include <stdio.h>
#include "ht.h"
#define START_MOD 7


void insert_n_random_values(struct hash_table* hash_table, int n, int max) {
	for (int i = 0; i < n; i++)
	{
		insert(hash_table, rand() % (max + 1), rand() % (max + 1));
	}
}


int main()
{
	srand(time(NULL));  // For random number generation

	printf("This program shows the operation of the implemented hash table.\n\n");

	struct hash_table hash_table;
	initialize_hash_table(&hash_table, START_MOD);

	printf("Hash table is created:\n");
	display(&hash_table);

	insert(&hash_table, 91919, 317);
	insert(&hash_table, 81045, 5874);

	printf("Inserted elements (91919, 317) and (81045, 5874):\n");
	display(&hash_table);

	insert_n_random_values(&hash_table, 20, 10000);

	printf("Inserted 20 random values:\n");
	display(&hash_table);

	insert_n_random_values(&hash_table, 20, 10000);

	printf("Inserted 20 more random values:\n");
	display(&hash_table);

	delete(&hash_table, 91919);

	printf("Deleted element with key 91919:\n");
	display(&hash_table);

	int value = get(&hash_table, 81045);
	printf("For key 81045 got value %d.\n\n", value);

	destroy_hash_table(&hash_table);

	printf("Hash table is destroyed.\n");
}