#include <stdio.h>
#include <stdlib.h>
#include "hash.h"

struct element
{
	int value;
	struct element* next;
};

struct line
{
	int length;
	struct element* head;
};

void rebalance(struct hash_table* my_hash_table)
{
	int new_mode = (my_hash_table->mode) * 2;
	int* values = malloc(my_hash_table->number_elements * sizeof(int));
	int index = 0;

	// collecting values

	for (int i = 0; i < my_hash_table->mode; i++)
	{
		struct element* curent_element = my_hash_table->lines[i].head;
		while (curent_element != NULL)
		{
			values[index] = curent_element->value;
			index += 1;
			curent_element = curent_element->next;
		}
	}

	delete_hash_table(my_hash_table);
	create_hash_table(my_hash_table, new_mode);

	// filling in a new hash table

	for (int j = 0; j < index; j++)
	{
		add_element(my_hash_table, values[j]);
	}

	free(values);

	printf("The hash table was rebalanced\n");
	return;
}

void check_balance(struct hash_table* my_hash_table, int hash_key)
{
	if (my_hash_table->lines[hash_key].length > ((my_hash_table->mode) / 2 + 1)) 
	{
		rebalance(my_hash_table);
	}
}

int hash_function(struct hash_table* hash_table, int value)
{
	return (value % (hash_table->mode));
}

void create_hash_table(struct hash_table* my_hash_table, int mode)
{
	struct line* lines = malloc(sizeof(struct line) * mode);
	for (int i = 0; i < mode; i++)
	{
		lines[i].head = NULL;
		lines[i].length = 0;
	}

	my_hash_table->mode = mode;
	my_hash_table->number_elements = 0;
	my_hash_table->lines = lines;
}

void add_element(struct hash_table* my_hash_table, int value)
{
	int hash_key = hash_function(my_hash_table, value);

	struct element* new_element = malloc(sizeof(struct element));
	new_element->value = value;
	new_element->next = NULL;

	struct line* lines = my_hash_table->lines;
	struct element* curent_element = lines[hash_key].head;

	if (curent_element == NULL) // there are no elements in line
	{
		lines[hash_key].head = new_element;
		lines[hash_key].length += 1;
		my_hash_table->number_elements += 1;
	}
	else if (new_element->value < curent_element->value) // new element is the smallest
	{
		new_element->next = curent_element;
		lines[hash_key].head = new_element;
		lines[hash_key].length += 1;
		my_hash_table->number_elements += 1;
	}
	else
	{
		while (new_element->value >= curent_element->value)
		{
			if (new_element->value == curent_element->value) // element is already in hash table
			{
				printf("This value(%d) is already in hash table. Its key is %d\n", new_element->value, hash_key);
				free(new_element);
				break;
			}
			else if (curent_element->next != NULL && new_element->value > curent_element->value && new_element->value < curent_element->next->value) // add new element according value order
			{
				new_element->next = curent_element->next;
				curent_element->next = new_element;
				lines[hash_key].length += 1;
				my_hash_table->number_elements += 1;
				break;
			}
			else if (curent_element->next == NULL) // add in the end of line
			{
				curent_element->next = new_element;
				lines[hash_key].length += 1;
				my_hash_table->number_elements += 1;
				break;
			}
			curent_element = curent_element->next;
		}
	}
	check_balance(my_hash_table, hash_key);
	return;
}

void find_element(struct hash_table* my_hash_table, int value)
{
	int hash_key = hash_function(my_hash_table, value);
	struct element* current_element = my_hash_table->lines[hash_key].head;

	while (current_element != NULL && current_element->value <= value)
	{
		if (current_element->value == value)
		{
			printf("The element with the value %d is in the hash table table, its key is %d\n", value, hash_key);
			return;
		}
		else
		{
			current_element = current_element->next;
		}
	}
	printf("There is no element with such value in hash table\n");
	return;
}

void delete_element(struct hash_table* my_hash_table, int value)
{
	int hash_key = hash_function(my_hash_table, value);

	struct line* lines = my_hash_table->lines;
	struct element* curent_element = lines[hash_key].head;

	if (curent_element == NULL) // there is no such element in line, we have nothing to delete
	{
		printf("There is no element with such value in hash table\n");
		return;
	}
	else if (value == curent_element->value) // find needed element at the start
	{
		lines[hash_key].head = curent_element->next;
		free(curent_element);
		lines[hash_key].length -= 1;
		my_hash_table->number_elements -= 1;
		return;
	}
	else
	{
		while (value > curent_element->value)
		{
			if (value == curent_element->next->value) // find needed element
			{
				struct element* next = curent_element->next->next;
				free(curent_element->next);
				curent_element->next = next;
				lines[hash_key].length -= 1;
				my_hash_table->number_elements -= 1;
				return;
			}
			else // continue searching for element
			{
				curent_element = curent_element->next;
			}
		}
		printf("There is no element with such value in hash table\n");
		return;
	}
}

void delete_hash_table(struct hash_table* my_hash_table)
{
	struct line* lines = my_hash_table->lines;

	for (int i = 0; i < my_hash_table->mode; i++)
	{
		while (lines[i].head != NULL)
		{
			struct element* temp_element = lines[i].head->next;
			free(lines[i].head);
			lines[i].head = temp_element;
		}
		free(lines[i].head);
	}
	free(my_hash_table->lines);
}

void show(struct hash_table* my_hash_table)
{
	printf("\n-----------Hash Table Info-----------\nNumbers of elements: %d\n", my_hash_table->number_elements);

	for (int i = 0; i < my_hash_table->mode; i++)
	{
		printf("Key = %d:", i);
		struct element* curent_element = my_hash_table->lines[i].head;

		if (curent_element == NULL)
		{
			printf("\n");
			continue;
		}
		else if (curent_element->next == NULL)
		{
			printf(" %d\n", curent_element->value);
		}
		else
		{
			while (curent_element->next != NULL)
			{
				printf(" %d,", curent_element->value);
				curent_element = curent_element->next;
			}
			printf(" %d\n", curent_element->value);
		}
	}
	printf("-------------------------------------\n\n");
}