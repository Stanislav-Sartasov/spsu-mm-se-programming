#include <stdlib.h>
#include <stdio.h>
#include "hash_table.h"

int hash_function(int key, int balance)
{
	if (key < 0 && key % balance != 0) return (key % balance) + balance;
	return (key % balance);
}

void create_lists(int start, int end, element** keys)
{
	for (int i = start; i < end;i++)
	{
		element* first = (element*)malloc(sizeof(element));
		first->next = NULL;
		first->key = -1;
		keys[i] = first;
	}
}

void create_table(hash_table* hash_table, int size)
{
	hash_table->balance = 4;
	hash_table->keys = (element**)malloc(size * sizeof(int*));
	create_lists(0, hash_table->balance, hash_table->keys);
	printf("\nHash Table created!");
}

int delete(hash_table* hash_table, int initial_key)
{
	int hash_key = hash_function(initial_key, hash_table->balance);

	if ((hash_table->keys[hash_key])->key == -1 && (hash_table->keys[hash_key])->next == NULL)
		return 0;

	if ((hash_table->keys[hash_key])->key == initial_key)
	{
		element* to_delete = hash_table->keys[hash_key];
		hash_table->keys[hash_key] = (hash_table->keys[hash_key])->next;
		free(to_delete);
		return 1;
	}
	element* prev = NULL;
	element* cur = hash_table->keys[hash_key];

	while (cur->key != initial_key && cur->next != NULL)
	{
		prev = cur;
		cur = cur->next;
	}

	if (cur->key == initial_key && cur->next != NULL)
	{
		prev->next = cur->next;
		free(cur);
		return 1;
	}
	else
		return 0;
}

int get(hash_table* hash_table, int initial_key)
{
	int hash_key = hash_function(initial_key, hash_table->balance);

	if ((hash_table->keys[hash_key])->key == -1 && (hash_table->keys[hash_key])->next == NULL)
		return 0;

	element* prev = NULL;
	element* cur = hash_table->keys[hash_key];

	while (cur->key != initial_key && cur->next != NULL)
	{
		prev = cur;
		cur = cur->next;
	}

	if (cur->key == initial_key && cur->next != NULL)
		return cur->value;
	else
		return NULL;
}

void rebalancing(hash_table* hash_table)
{
	create_lists(hash_table->balance, hash_table->balance * 2, hash_table->keys);
	for (int i = 0; i < hash_table->balance;i++)
	{
		element* head = hash_table->keys[i];

		if (hash_table->keys[i]->key == -1 && head == NULL)
		{
			continue;
		}

		while (head->next != NULL)
		{
			element* temp = head->next;
			if (hash_function(head->key, hash_table->balance) != hash_function(head->key, hash_table->balance * 2))
			{
				hash_table->balance *= 2;
				add(hash_table, head->key, head->value);
				hash_table->balance = (int)(hash_table->balance / 2);
				delete(hash_table, head->key);
			}
			head = temp;
		}
	}
	hash_table->balance *= 2;
}

void check(hash_table* hash_table, int initial_key)
{
	int hash_key = hash_function(initial_key, hash_table->balance);
	int elements = 0;
	element* head = hash_table->keys[hash_key]->next;

	while (head)
	{
		elements++;
		head = head->next;
	}
	if (elements > hash_table->balance / 2)
		rebalancing(hash_table);
}

void add(hash_table* hash_table, int initial_key, int value)
{
	if (delete(hash_table, initial_key))
		printf("\n %d was already in table and its value has been replaced by value (%d)", initial_key, value);

	int hash_key = hash_function(initial_key, hash_table->balance);
	element* first = (element*)malloc(sizeof(element));

	first->key = initial_key;
	first->value = value;
	first->next = hash_table->keys[hash_key];
	hash_table->keys[hash_key] = first;

	check(hash_table, initial_key);
}

void show_table(hash_table* hash_table)
{
	printf("\n-------------------\n");
	for (int i = 0; i < hash_table->balance;i++)
	{
		printf("%d- ", i);
		element* head = hash_table->keys[i];
		while (head)
		{
			if (head->next) printf("-> [%d,%d] ", head->key, head->value);
			head = head->next;
		}
		printf("\n");
	}
	printf("-------------------\n");
}

void delete_table(hash_table* hash_table, int size)
{
	for (int i = 0; i < hash_table->balance;i++)
	{
		element* head = hash_table->keys[i];

		if (hash_table->keys[i]->key == -1 && head == NULL)
		{
			free(hash_table->keys[i]);
			continue;
		}
		while (head->next != NULL)
		{
			element* temp = head->next;
			delete(hash_table, head->key);
			head = temp;
		}
	}
	free(hash_table->keys);
	printf("\nHash Table cleared!");
}