#include <stdlib.h>
#include "my_hash_table.h"
#include "my_list.h"

void restructure(struct my_hash_table* table) 
{
	struct my_list_holder* new_arrays = init_list_holder();

	free(table->bucket_lengths);
	table->bucket_lengths = (int*)malloc(2 * table->current_base * sizeof(int));
	memset(table->bucket_lengths, 0, sizeof(int) * 2 * table->current_base);

	for (int i = 0; i < table->current_base * 2; i++)
	{
		lst_holder_add(new_arrays, init_list());
	}

	for (int i = 0; i < table->current_base; i++)
	{
		struct my_int_list* current_list = lst_holder_get((table->buckets), i);
		while (1)
		{
			if (current_list == NULL) break;
			if ((current_list->key - i) % (table->current_base * 2) == 0)
			{
				lst_add(lst_holder_get(new_arrays, i), current_list->key, current_list->value);
				table->bucket_lengths[i]++;
			}
			else
			{
				lst_add(lst_holder_get(new_arrays, table->current_base + i), current_list->key, current_list->value);
				table->bucket_lengths[table->current_base + i]++;
			}
			current_list = current_list->prev;
		}
	}
	// dispose old lists
	for (int i = 0; i < table->current_base; i++)
		lst_dispose(lst_holder_get(table->buckets, i));
	lst_holder_dispose(table->buckets);
	table->buckets = new_arrays;

	table->current_base *= 2;
}

int hash_func(struct my_hash_table* table, int key) 
{
	return key % table->current_base;
}

void add(struct my_hash_table* table, int key, int value) 
{
	lst_add(lst_holder_get((table->buckets), hash_func(table, key)), key, value);
	table->bucket_lengths[hash_func(table, key)]++;
	// For restructure one of bucket lengths should be grater than 16
	for (int i = 0; i < table->current_base; i++)
		if (table->bucket_lengths[i] > 16)
		{
			restructure(table);
			break;
		}
}

int remove(struct my_hash_table* table, int key) 
{
	lst_remove(lst_holder_get((table->buckets), hash_func(table, key)), key);
	table->bucket_lengths[hash_func(table, key)]--;
}

int get(struct my_hash_table* table, int key) 
{
	lst_get(lst_holder_get((table->buckets), hash_func(table, key)), key);
}

struct my_hash_table* create_hash_table()
{
	struct my_hash_table* table = (struct my_hash_table*)malloc(sizeof(struct my_hash_table));

	table->buckets = init_list_holder();
	table->current_base = 2;
	table->bucket_lengths = (int*)malloc(2 * sizeof(int));

	table->bucket_lengths[0] = 0;
	table->bucket_lengths[1] = 0;

	lst_holder_add(table->buckets, init_list());
	lst_holder_add(table->buckets, init_list());

	return table;
}