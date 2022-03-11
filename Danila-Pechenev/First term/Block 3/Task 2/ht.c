#include <stdbool.h>
#include <stdlib.h>
#include <stdio.h>
#include <math.h>
#define POLICY_CONST 0.5
#define NOT_FOUND -800


struct data_cell
{
	int key;
	int value;
	struct data_cell* next;
};


struct bucket
{
	int length;
	struct data_cell* first_data_cell;
};


struct hash_table
{
	int mod;
	int n_elements;
	struct bucket* buckets;
};


int update_policy_mod(struct hash_table* hash_table)
{
	int last_mod = hash_table->mod;
	for (int n = last_mod + 2; ; n += 2)
	{
		bool is_prime = true;
		int sq_root = sqrt(n);
		for (int d = 3; d <= sq_root; d += 2)
		{
			if (n % d == 0)
			{
				is_prime = false;
				break;
			}
		}
		if (is_prime)
		{
			return n;
		}
	}
}


int hash_function(struct hash_table* hash_table, int key)
{
	return (key % (hash_table->mod));
}


void initialize_hash_table(struct hash_table* hash_table, int mod)
{
	struct bucket* array = malloc(sizeof(struct bucket) * mod);
	for (int i = 0; i < mod; i++)
	{
		array[i].length = 0;
		array[i].first_data_cell = 0;
	}
	hash_table->mod = mod;
	hash_table->n_elements = 0;
	hash_table->buckets = array;
}


void insert_cell_in_bucket(struct bucket* buckets, struct data_cell* data_cell, int hash_value)
{
	struct data_cell* current_cell = buckets[hash_value].first_data_cell;

	if (current_cell == 0)
	{
		buckets[hash_value].first_data_cell = data_cell;
		data_cell->next = 0;
	}
	else if (data_cell->key < current_cell->key)
	{
		buckets[hash_value].first_data_cell = data_cell;
		data_cell->next = current_cell;
	}
	else
	{
		while (current_cell->next != 0)
		{
			if (current_cell->key < data_cell->key && data_cell->key < current_cell->next->key)
			{
				data_cell->next = current_cell->next;
				current_cell->next = data_cell;
				buckets[hash_value].length += 1;
				return;
			}
			else if (current_cell->key == data_cell->key || data_cell->key == current_cell->next->key)
			{
				printf("Error. There is already an element with the same key.\n");
				return;
			}
			else
			{
				current_cell = current_cell->next;
			}
		}
		current_cell->next = data_cell;
		data_cell->next = 0;
	}
	buckets[hash_value].length += 1;
}


void rebalance(struct hash_table* hash_table)
{
	int new_mod = update_policy_mod(hash_table);

	struct bucket* new_buckets = malloc(sizeof(struct bucket) * new_mod);
	for (int i = 0; i < new_mod; i++)
	{
		new_buckets[i].length = 0;
		new_buckets[i].first_data_cell = 0;
	}

	for (int i = 0; i < hash_table->mod; i++)
	{
		struct data_cell* current_cell = hash_table->buckets[i].first_data_cell;
		if (current_cell == 0)
		{
			continue;
		}
		struct data_cell* next_cell = current_cell->next;
		while (next_cell != 0)
		{

			insert_cell_in_bucket(new_buckets, current_cell, (current_cell->key) % new_mod);
			current_cell = next_cell;
			next_cell = current_cell->next;
		}
		insert_cell_in_bucket(new_buckets, current_cell, (current_cell->key) % new_mod);
	}

	free(hash_table->buckets);

	hash_table->mod = new_mod;
	hash_table->buckets = new_buckets;
}


void insert(struct hash_table* hash_table, int key, int value)
{
	struct data_cell* new_cell = malloc(sizeof(struct data_cell));
	new_cell->key = key;
	new_cell->value = value;

	int hash_value = hash_function(hash_table, key);

	insert_cell_in_bucket(hash_table->buckets, new_cell, hash_value);
	hash_table->n_elements += 1;

	if (hash_table->buckets[hash_value].length > (POLICY_CONST * hash_table->mod))  // Rebalancing policy
	{
		rebalance(hash_table);
	}
}


int get(struct hash_table* hash_table, int key)
{
	int hash_value = hash_function(hash_table, key);

	struct data_cell* current_cell = hash_table->buckets[hash_value].first_data_cell;
	if (current_cell == 0)
	{
		return NOT_FOUND;
	}
	else
	{
		if (current_cell->key == key)
		{
			return current_cell->value;
		}
		else if (current_cell->next == 0)
		{
			return NOT_FOUND;
		}
		else
		{
			current_cell = current_cell->next;
			while (current_cell->next != 0 && current_cell->key < key)
			{
				current_cell = current_cell->next;
			}
			if (current_cell->key == key)
			{
				return current_cell->value;
			}
			else
			{
				return NOT_FOUND;
			}
		}
	}
}


void delete(struct hash_table* hash_table, int key)
{
	int hash_value = hash_function(hash_table, key);

	struct data_cell* current_cell = hash_table->buckets[hash_value].first_data_cell;
	if (current_cell == 0)
	{
		return;
	}
	if (current_cell->key == key)
	{
		hash_table->buckets[hash_value].first_data_cell = current_cell->next;
		free(current_cell);
		hash_table->buckets[hash_value].length -= 1;
		hash_table->n_elements -= 1;
	}
	else if (current_cell->next == 0)
	{
		return;
	}
	else
	{
		struct data_cell* previous_cell = current_cell;
		current_cell = current_cell->next;
		while (current_cell->next != 0 && current_cell->key < key)
		{
			previous_cell = current_cell;
			current_cell = current_cell->next;
		}
		if (current_cell->key == key)
		{
			previous_cell->next = current_cell->next;
			free(current_cell);
			hash_table->buckets[hash_value].length -= 1;
			hash_table->n_elements -= 1;
		}
	}
}


void destroy_hash_table(struct hash_table* hash_table)
{
	for (int i = 0; i < hash_table->mod; i++)
	{
		struct data_cell* current_cell = hash_table->buckets[i].first_data_cell;
		struct data_cell* next_cell;
		if (current_cell == 0)
		{
			continue;
		}
		while (current_cell->next != 0)
		{
			next_cell = current_cell->next;
			free(current_cell);
			current_cell = next_cell;
		}
		free(current_cell);
	}
	free(hash_table->buckets);
}


void display(struct hash_table* hash_table)
{
	printf("--------------------------------------------------\n");
	for (int i = 0; i < hash_table->mod; i++)
	{
		printf("[%d] (%d): ", i, hash_table->buckets[i].length);
		struct data_cell* current_cell = hash_table->buckets[i].first_data_cell;
		if (current_cell == 0)
		{
			printf("\n");
			continue;
		}
		while (current_cell->next != 0)
		{
			printf("(%d, %d), ", current_cell->key, current_cell->value);
			current_cell = current_cell->next;
		}
		printf("(%d, %d)\n", current_cell->key, current_cell->value);
	}
	printf("--------------------------------------------------\n");
}
