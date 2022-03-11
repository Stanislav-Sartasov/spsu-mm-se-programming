#include <stdio.h>
#include "hash_table.h"

int hash_function(int key, int length)
{
	return key % length;
}

int is_rebalance(table* keys)
{
	return keys->max_length > keys->size / 2;
}

struct table* create_table(int size)
{
	table* keys = (table*)malloc(sizeof(table));
	keys->first_elements = (list**)malloc(size * sizeof(list*));
	keys->lengths = (int*)malloc(size * sizeof(int));
	keys->size = size;
	keys->max_length = 0;
	for (int i = 0; i < size; i++)
	{
		keys->first_elements[i] = NULL;
		keys->lengths[i] = 0;
	}

	return keys;
}

void rebalance(table* keys)
{
	table* new_keys = create_table(keys->size * 2);
	for (int i = 0; i < keys->size; i++)
	{
		int length = keys->lengths[i];
		for (int j = 0; j < length; j++)
		{
			list* curr = keys->first_elements[i];
			add_element(new_keys, curr->key, curr->value);
			del_element(keys, curr->key);
		}
	}
	free(keys->lengths);
	free(keys->first_elements);
	*keys = *new_keys;
}

void add_element(table* keys, int key, int value)
{
	int h = hash_function(key, keys->size);

	list* first = (list*)malloc(sizeof(list));
	first->key = key;
	first->value = value;

	if (++keys->lengths[h] > keys->max_length)
	{
		keys->max_length = keys->lengths[h];
	}

	if (!(keys->first_elements[h]))
	{
		first->next = NULL;
		keys->first_elements[h] = first;
	}
	else
	{
		first->next = keys->first_elements[h];
		keys->first_elements[h] = first;
	}


	if (is_rebalance(keys))
	{
		rebalance(keys);
	}
}

int find_element(table* keys, int key)
{
	list* curr = keys->first_elements[hash_function(key, keys->size)];

	if (!(curr))
	{
		return 0;
	}

	while (curr->key != key && curr->next)
	{
		curr = curr->next;
	}

	if (curr->key == key)
	{
		return curr->value;
	}

	return 0;
}

void del_element(table* keys, int key)
{
	int h = hash_function(key, keys->size);
	list* curr = keys->first_elements[h];

	if (!(curr))
	{
		return;
	}

	if (curr->key == key)
	{
		keys->first_elements[h] = curr->next;
		keys->lengths[h]--;
		free(curr);
		return;
	}

	list* prev = NULL;

	while (curr->key != key && curr->next)
	{
		prev = curr;
		curr = curr->next;
	}

	if (curr->key == key)
	{
		prev->next = curr->next;
		keys->lengths[h]--;
		free(curr);
	}
}

void drop_table(table* keys)
{
	for (int i = 0; i < keys->size; i++)
	{
		for (int j = 0; j < keys->lengths[i]; j++)
		{
			list* curr = keys->first_elements[i];
			del_element(keys, curr->key);
		}
	}
	free(keys->lengths);
	free(keys->first_elements);
	free(keys);
}
