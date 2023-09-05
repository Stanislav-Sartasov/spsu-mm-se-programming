#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include "hash.h"

#define START_VALUE 7
#define DELETE_VALUE 6756
#define MAX_VALUE 10000

void add_random_elements(struct hash_table* my_hash_table, int amount, int max_element)
{
	for (int i = 0; i < amount; i++)
	{
		add_element(my_hash_table, rand() % (max_element + 1));
	}
}

int main(int argc, char* argv[])
{
	srand(time(NULL));
	struct hash_table remainders;

	printf("This program shows my hash table with keys expressed in numbers\n");
	printf("-Creare hash table for key = [0, ..., %d]:\n", START_VALUE - 1);
	create_hash_table(&remainders, START_VALUE);
	show(&remainders);

	printf("-Add 10 random elements\n");
	add_random_elements(&remainders, 10, MAX_VALUE);
	show(&remainders);

	printf("-Add %d\n", DELETE_VALUE);
	add_element(&remainders, DELETE_VALUE);
	show(&remainders);

	printf("-Try to add %d again\n", DELETE_VALUE);
	add_element(&remainders, DELETE_VALUE);
	show(&remainders);

	printf("-try to find element with value %d\n", DELETE_VALUE);
	find_element(&remainders, DELETE_VALUE);
	show(&remainders);

	int random = rand() % (MAX_VALUE + 1);
	printf("-try to find element with random value %d\n", random);
	find_element(&remainders, random);
	show(&remainders);

	printf("-Delete %d\n", DELETE_VALUE);
	delete_element(&remainders, DELETE_VALUE);
	show(&remainders);

	printf("-Add 30 random elements again\n");
	add_random_elements(&remainders, 30, MAX_VALUE);
	show(&remainders);

	delete_hash_table(&remainders);

	printf("That's over. Thank you for testing my hash table!\n");
	return 0;
}