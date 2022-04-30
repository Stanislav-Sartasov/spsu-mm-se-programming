#include <stdio.h>
#include <stdlib.h>
#include "hash_table.h"

int random(int l, int r)
{
	return rand() % (r - l + 1) + l;
}

void add_delete_test()
{
	hash_table ht = {.current_size = INITIAL_TABLE_SIZE, .table = (lst**) malloc(INITIAL_TABLE_SIZE * sizeof(lst*))};
	initialize_hash_table(&ht);

	int n = random(5, 10);
	int *keys = (int*)malloc(sizeof(int) * n);
	for (int i = 0; i < n; i++)
	{
		int to_insert_key = random(-1000, 1000);
		keys[i] = to_insert_key;
		int to_insert_value = random(-1000, 1000);

		add_pair(&ht, to_insert_key, to_insert_value);
		printf("Added pair {%d, %d}\n", to_insert_key, to_insert_value);
	}

	printf("Current state of the hash table:\n");
	print_table(&ht);

	for (int i = 0; i < n; i++)
	{
		printf("Removing element with key %d\n", keys[i]);
		remove_key(&ht, keys[i]);
	}

	printf("Current state of the hash table:\n");
	print_table(&ht);
	delete_hash_table(&ht);
}

void add_existing_key_test()
{
	hash_table ht = {.current_size = INITIAL_TABLE_SIZE, .table = (lst**) malloc(INITIAL_TABLE_SIZE * sizeof(lst*))};
	initialize_hash_table(&ht);

	int random_key = random(-1000, 1000);
	int random_value = random(-1000, 1000);

	add_pair(&ht, random_key, random_value);
	printf("Added pair {%d, %d}\n", random_key, random_value);
	int new_random_value = random(1001, 2000);
	printf("Trying to add another value with the same key: {%d, %d}\n", random_key, new_random_value);
	add_pair(&ht, random_key, new_random_value);

	delete_hash_table(&ht);
}

void delete_non_existing_key_test()
{
	hash_table ht = {.current_size = INITIAL_TABLE_SIZE, .table = (lst**) malloc(INITIAL_TABLE_SIZE * sizeof(lst*))};
	initialize_hash_table(&ht);

	printf("Currently the hash_table is empty:\n");
	print_table(&ht);
	int to_delete = random(-1000, 1000);
	printf("Trying to delete a key %d:\n", to_delete);
	remove_key(&ht, to_delete);
	printf("Remove executed succesfully:\n");
	print_table(&ht);

	delete_hash_table(&ht);
}

void rebalance_test()
{
	hash_table ht = {.current_size = INITIAL_TABLE_SIZE, .table = (lst**) malloc(INITIAL_TABLE_SIZE * sizeof(lst*))};
	initialize_hash_table(&ht);

	printf("Currently hash table has %d empty buckets:\n", ht.current_size);

	while (ht.current_size == INITIAL_TABLE_SIZE)
	{
		int random_key = random(-1000, 1000), random_value = random(-1000, 1000);\
		printf("Adding pair {%d, %d}\n", random_key, random_value);
		add_pair(&ht, random_key, random_value);
	}

	printf("Now rebalance happened:\n");
	print_table(&ht);

	delete_hash_table(&ht);
}