#include <stdio.h>
#include <stdlib.h>
#include "hash_table.h"

unsigned int hash(int key, int table_size)
{
	int hash = ((key >> 16) ^ key) * 0x45d9f3b;
	hash = ((hash >> 16) ^ hash) * 0x45d9f3b;
	hash = (hash >> 16) ^ hash;
	return hash % table_size;
}

void delete_linked_list(lst *list)
{
	if (list == NULL)
		return;
	if (list->next != NULL)
		delete_linked_list(list->next);
	free(list);
}

int is_prime(int num)
{
	for (int i = 2; i * i <= num; i++)
	{
		if (num % i == 0)
			return 0;
	}
	return 1;
}

int find_next_prime(int key)
{
	int i;
	for (i = 2 * key + 1; !is_prime(i); i++);
	return i;
}

void initialize_hash_table(hash_table *ht)
{
	for (int i = 0; i < ht->current_size; i++)
		ht->table[i] = NULL;
}

void rebalance(hash_table *ht)
{
	int new_size = find_next_prime(ht->current_size);
	hash_table *new_ht = (hash_table*) malloc(sizeof(hash_table));
	new_ht->current_size = new_size;
	new_ht->table = (lst**) malloc(new_size * sizeof(lst*));
	initialize_hash_table(new_ht);
	for (int i = 0; i < ht->current_size; i++)
	{
		lst *current_list = ht->table[i];
		while (current_list != NULL)
		{
			add_pair(new_ht, current_list->key, current_list->value);
			current_list = current_list->next;
		}
		delete_linked_list(ht->table[i]);
	}
	free(ht->table);
	ht->current_size = new_size;
	ht->table = new_ht->table;
}

void add_pair(hash_table *ht, int key, int value)
{
	if (find(ht, key) != NULL)
	{
		printf("Error: there is already an element with such key\n");
		return;
	}
	lst *new_pair = (lst*)malloc(sizeof(lst));
	new_pair->key = key;
	new_pair->value = value;
	new_pair->next = NULL;
	int index = hash(new_pair->key, ht->current_size);
	if (ht->table[index] == NULL)
	{
		ht->table[index] = new_pair;
	}
	else
	{
		int lst_size = 1;
		lst *current = ht->table[index];
		while (current->next != NULL)
		{
			current = current->next;
			lst_size++;
		}
		current->next = new_pair;
		if (lst_size + 1 > MAX_BUCKET_SIZE)
			rebalance(ht);
	}
}

void print_table(hash_table *ht)
{
	for (int i = 0; i < ht->current_size; i++)
	{
		lst *current = ht->table[i];
		printf("%d ", i);
		while (current != NULL)
		{
			printf("[%d, %d]", current->key, current->value);
			current = current->next;
		}
		printf("\n");
	}
	printf("\n");
}

lst *find(hash_table *ht, int key)
{
	int index = hash(key, ht->current_size);
	lst *current = ht->table[index];
	lst *found = NULL;
	while (current != NULL)
	{
		if (current->key == key)
		{
			found = current;
			break;
		}
		current = current->next;
	}
	return found;
}

void delete(hash_table *ht, int key)
{
	int index = hash(key, ht->current_size);
	lst *current = ht->table[index];

	if (current == NULL)
		return;
	if (key == current->key)
	{
		ht->table[index] = current->next;
		free(current);
		return;
	}
	lst *found = NULL;
	while (current->next != NULL)
	{
		if (current->next->key == key)
		{
			found = current->next;
			break;
		}
		current = current->next;
	}
	if (found != NULL)
	{
		current->next = found->next;
		free(found);
	}
}