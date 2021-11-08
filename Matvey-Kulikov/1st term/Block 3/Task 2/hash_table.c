#include "hash_table.h"
#include <stdlib.h>

int get_next_prime(int number)
{
	int next_prime;
	if (number == 1)
	{
		next_prime = 2;
	}
	else
	{
		next_prime = number - 1 + number % 2;
		int flag = 1;
		while (flag)
		{
			flag = 0;
			next_prime += 2;
			for (int i = 3; i * i <= next_prime; i += 2)
			{
				if (next_prime % i == 0)
				{
					flag = 1;
					break;
				}
			}
		}
	}
	return next_prime;
}

int hash(int length, int key)
{
	return key % length;
}

pair** get_head_pointer(hash_table* table, int key)
{
	int index = hash(table->array_length, key);
	pair** head_pointer = &(table->hash_indexed_array[index]);
	return head_pointer;
}

int add_pair_to_list(pair** head_pointer, pair* added_pair, int* elements_amount)
{
	int length;
	added_pair->next = NULL;

	if (*head_pointer)
	{
		length = 0;
		int new_key = 1;
		pair* prev_head;
		pair* head = *head_pointer;

		while (head != NULL)
		{
			++length;
			prev_head = head;

			if (head->key == added_pair->key)
			{
				head->value = added_pair->value;
				new_key = 0;
			}
			prev_head = head;
			head = head->next;
		}

		if (new_key)
		{
			++length;
			prev_head->next = added_pair;
			++*elements_amount;
		}
	}
	else
	{
		*head_pointer = added_pair;
		++*elements_amount;
		length = 1;
	}

	return length;
}

void rebalance(hash_table* table)
{
	int old_array_length = table->array_length;
	pair** old_array = table->hash_indexed_array;
	table->array_length = get_next_prime(old_array_length);
	table->hash_indexed_array = (pair**)calloc(table->array_length, sizeof(pair*));
	table->pairs_amount = 0;
	for (int i = 0; i < old_array_length; i++)
	{
		pair* moved_pair = old_array[i];
		if (moved_pair)
		{
			pair* next_pair;
			while (moved_pair != NULL)
			{
				next_pair = moved_pair->next;
				pair** new_head_pointer = get_head_pointer(table, moved_pair->key);
				add_pair_to_list(new_head_pointer, moved_pair, &(table->pairs_amount));
				moved_pair = next_pair;
			}
		}
	}
	free(old_array);
}

hash_table* new_table()
{
	hash_table* new_table = (hash_table*)malloc(sizeof(hash_table));
	new_table->hash_indexed_array = (pair**)calloc(2, sizeof(pair*));
	new_table->array_length = 2;
	new_table->pairs_amount = 0;
	return new_table;
}

void add(hash_table* table, int key, int value)
{
	pair* new_pair = (pair*)malloc(sizeof(pair));
	new_pair->key = key;
	new_pair->value = value;
	pair** head_pointer = get_head_pointer(table, key);

	int list_length = add_pair_to_list(head_pointer, new_pair, &(table->pairs_amount));

	if (list_length >= ((table->array_length + table->array_length % 2) / 2))
	{
		rebalance(table);
	}
}

int get(hash_table* table, int key)
{
	pair* head = *get_head_pointer(table, key);
	if (!head)
	{
		return 0;
	}
	while (head != NULL)
	{
		if (head->key == key)
		{
			return head->value;
		}
		head = head->next;
	}
	return 0;
}

void del(hash_table* table, int key)
{
	pair* head = *get_head_pointer(table, key);
	pair* prev_head = NULL;
	while (head != NULL)
	{
		if (head->key == key)
		{
			if (prev_head)
			{
				prev_head->next = head->next;
			}
			free(head);
			break;
		}
		prev_head = head;
		head = head->next;
	}
}
