#include <stdio.h>
#include <stdlib.h>
#include "hash_table.h"
#include "primary_check.h"

INT32 hash(INT32 key, INT32 lenght)
{
	return key % lenght;
}

hash_table *create_table(INT32 size)
{
	hash_table *table = (hash_table *) malloc(sizeof(hash_table));
	table->size = size;
	table->lengths = (INT32 *) malloc(sizeof(INT32) * size);
	table->linked_lists = (struct node **) malloc(sizeof(struct node *) * size);
	for (INT32 i = 0; i < size; i++)
	{
		table->linked_lists[i] = NULL;
		table->lengths[i] = 0;
	}
	return table;
}

hash_table *rebalance_table(hash_table *table)
{
	int new_size = table->size * 2;
	while (!is_prime(new_size))
		new_size++;
	hash_table *new_table = create_table(new_size);
	for (int i = 0; i < table->size; i++)
	{
		while ((*table).linked_lists[i] != NULL)
		{
			int value;
			find_element(table, (*table).linked_lists[i]->key, &value);
			add_element(new_table, (*table).linked_lists[i]->key, value);
			delete_element(table, (*table).linked_lists[i]->key);
		}
	}
	free(table->linked_lists);
	free(table->lengths);
	*table = *new_table;
}

void add_element(hash_table *table, INT32 key, INT32 value)
{
	INT32 index = hash(key, table->size);
	struct node *first_node = table->linked_lists[index];
	// find copies of the element in the linked list and if key exists, change value
	INT32 find_value;
	if (find_element(table, key, &find_value))
	{
		while (first_node != NULL)
		{
			if (first_node->key == key)
			{
				first_node->value = value;
				return;
			}
		}
	}
	// create new element and add to linked list
	struct node *new_node = malloc(sizeof(struct node));
	new_node->key = key;
	new_node->value = value;
	new_node->next = NULL;
	if (table->linked_lists[index] == NULL)
		table->linked_lists[index] = new_node;
	else
	{
		new_node->next = table->linked_lists[index];
		table->linked_lists[index] = new_node;
	}
	table->lengths[index]++;
	if (table->lengths[index] > 31)
		rebalance_table(table);
}

INT8 find_element(hash_table *table, INT32 key, INT32 *value)
{
	INT32 index = hash(key, table->size);
	struct node *first_node = table->linked_lists[index];
	while (first_node != NULL)
	{
		if (first_node->key == key)
		{
			*value = (*first_node).value;
			return 1;
		}
		first_node = first_node->next;
	}
	return 0;
}

void delete_element(hash_table *table, INT32 key)
{
	INT32 index = hash(key, table->size);
	struct node *first_node = table->linked_lists[index];
	struct node *prev_node = NULL;
	while (first_node != NULL)
	{
		if (first_node->key == key)
		{
			if (prev_node != NULL)
				prev_node->next = first_node->next;
			else
				table->linked_lists[index] = table->linked_lists[index]->next;
			free(first_node);
			table->lengths[index]--;
			return;
		}
		prev_node = first_node;
		first_node = first_node->next;
	}
}

void free_table(hash_table *table)
{
	for (int i = 0; i < table->size; i++)
	{
		while ((*table).linked_lists[i] != NULL)
			delete_element(table, (*table).linked_lists[i]->key);
	}
	free(table->linked_lists);
	free(table->lengths);
	free(table);
}
