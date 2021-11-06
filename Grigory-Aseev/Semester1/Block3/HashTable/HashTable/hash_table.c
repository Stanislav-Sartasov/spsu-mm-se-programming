#include "hash_table.h"
#define LOAD_FACTOR 0.75
#define INITIAL_SEGMENTS 16

void* real(double x)
{
	double* f = (double*)malloc(sizeof(double));
	*f = x;
	return (void*)f;
}

void create_table(struct table* hash_table, value_type_t key, value_type_t value)
{
	hash_table->lists = (struct list**)(malloc(sizeof(struct list*) * INITIAL_SEGMENTS));
	hash_table->segments = INITIAL_SEGMENTS;
	hash_table->entries = 0;
	hash_table->type.value = value;
	hash_table->type.key = key;
	for (size_t i = 0; i < INITIAL_SEGMENTS; i++)
	{
		hash_table->lists[i] = NULL;
	}
}

void rebalance(struct table* hash_table)
{
	struct table* new_table = (struct table*)malloc(sizeof(struct table));
	create_table(new_table, hash_table->type.key, hash_table->type.value);
	free(new_table->lists);
	new_table->segments = 2 * hash_table->segments;
	new_table->lists = (struct list**)(malloc(sizeof(struct list*) * new_table->segments));
	new_table->entries = 0;
	for (size_t i = 0; i < new_table->segments; i++)
	{
		new_table->lists[i] = NULL;
	}

	for (size_t i = 0; i < hash_table->segments; i++)
	{
		while (hash_table->lists[i] != NULL)
		{
			if (hash_table->type.key == DECIMAL_ELEM)
			{
				if (hash_table->type.value == DECIMAL_ELEM)
				{
					add_elem(new_table, hash_table->lists[i]->key.decimal, hash_table->lists[i]->value.decimal);
					del_elem(hash_table, hash_table->lists[i]->key.decimal);
				}
				else if (hash_table->type.value == STRING_ELEM)
				{
					add_elem(new_table, hash_table->lists[i]->key.decimal, hash_table->lists[i]->value.string);
					del_elem(hash_table, hash_table->lists[i]->key.decimal);
				}
				else if (hash_table->type.value == REAL_ELEM)
				{
					add_elem(new_table, hash_table->lists[i]->key.decimal, real(hash_table->lists[i]->value.real));
					del_elem(hash_table, hash_table->lists[i]->key.decimal);
				}
				else
				{
					exit(1);
				}
			}
			else if (hash_table->type.key == STRING_ELEM)
			{
				if (hash_table->type.value == DECIMAL_ELEM)
				{
					add_elem(new_table, hash_table->lists[i]->key.string, hash_table->lists[i]->value.decimal);
					del_elem(hash_table, hash_table->lists[i]->key.string);
				}
				else if (hash_table->type.value == STRING_ELEM)
				{
					add_elem(new_table, hash_table->lists[i]->key.string, hash_table->lists[i]->value.string);
					del_elem(hash_table, hash_table->lists[i]->key.string);
				}
				else if (hash_table->type.value == REAL_ELEM)
				{
					add_elem(new_table, hash_table->lists[i]->key.string, real(hash_table->lists[i]->value.real));
					del_elem(hash_table, hash_table->lists[i]->key.string);
				}
				else
				{
					exit(1);
				}
			}
			else
			{
				exit(1);
			}
		}
	}
	free(hash_table->lists);
	hash_table->segments = new_table->segments;
	hash_table->entries = new_table->entries;
	hash_table->lists = new_table->lists;
}

void free_table(struct table* hash_table)
{
	for (size_t i = 0; i < hash_table->segments; i++)
	{
		while (hash_table->lists[i] != NULL)
		{
			if (hash_table->type.key == DECIMAL_ELEM)
				del_elem(hash_table, hash_table->lists[i]->key.decimal);
			else if (hash_table->type.key == STRING_ELEM)
				del_elem(hash_table, hash_table->lists[i]->key.string);
			else
			{
				exit(1);
			}
		}
	}
	free(hash_table->lists);
	hash_table->entries = 0;
	hash_table->segments = 0;
	free(hash_table);
}

void add_elem(struct table* hash_table, void* key, void* value)
{


	uint32_t res_hash = hashing(key, hash_table->segments, hash_table->type.key);
	struct list* current;
	struct list* find_el = find_elem(hash_table, key);
	if (find_el != NULL)
	{
		del_elem(hash_table, key);
	}
	current = hash_table->lists[res_hash];
	if (current != NULL)
	{
		while (current->next != NULL)
		{
			current = current->next;
		}
		current->next = (struct list*)(malloc(sizeof(struct list)));
		current = current->next;
	}
	else
	{
		hash_table->lists[res_hash] = (struct list*)(malloc(sizeof(struct list)));
		current = hash_table->lists[res_hash];
	}
	current->next = NULL;
	hash_table->entries++;
	if (hash_table->type.key == DECIMAL_ELEM)
		current->key.decimal = (int64_t)key;
	else if (hash_table->type.key == STRING_ELEM)
		current->key.string = (uint8_t*)key;
	else
	{
		exit(1);
	}

	if (hash_table->type.value == DECIMAL_ELEM)
	{
		current->value.decimal = (int64_t)value;
	}
	else if (hash_table->type.value == STRING_ELEM)
		current->value.string = (uint8_t*)value;
	else if (hash_table->type.value == REAL_ELEM)
	{
		current->value.real = *(double*)value;
		free((double*)value);
	}
	else
	{
		exit(1);
	}

	if (LOAD_FACTOR < (double)hash_table->entries / hash_table->segments)
	{
		rebalance(hash_table);
	}
}

uint16_t cmpr_keys(void* key, value_t tab_key, value_type_t type)
{
	if (type == DECIMAL_ELEM && tab_key.decimal == (int64_t)key)
		return 1;
	else if (type == STRING_ELEM && tab_key.string == (uint8_t*)key)
		return 1;
	else if (type != DECIMAL_ELEM && type != STRING_ELEM)
		exit(1);
	return 0;
}

uint32_t hash_function_int(uint32_t key, size_t size)
{
	key = ((key >> 16) ^ key) * 0x119de1f3;
	key = ((key >> 16) ^ key) * 0x119de1f3;
	key = (key >> 16) ^ key;
	return key % size;
}
uint32_t hash_function_str(uint8_t* s, size_t size)
{
	uint32_t hash = 5381;
	for (size_t i = 0; i < strlen(s); i++)
	{
		hash = ((hash << 5) + hash) + s[i];
	}
	return hash % size;
}

uint32_t hashing(void* key, size_t size, value_type_t type)
{
	uint32_t res_hash;
	if (type == STRING_ELEM)
	{
		res_hash = hash_function_str((uint8_t*)key, size);

	}
	else if (type == DECIMAL_ELEM)
	{
		res_hash = hash_function_int((uint32_t)key, size);
	}
	else
	{
		exit(1);
	}
	return res_hash;
}


void del_elem(struct table* hash_table, void* key)
{
	struct list* current = find_elem(hash_table, key);
	if (current == NULL)
		return;
	uint32_t res_hash = hashing(key, hash_table->segments, hash_table->type.key);
	struct list* new_current = hash_table->lists[res_hash];
	if (new_current == current)
	{
		hash_table->lists[res_hash] = current->next;
	}
	else
	{
		while (new_current->next != current && !cmpr_keys(key, new_current->key, hash_table->type.key))
		{
			new_current = new_current->next;
		}
		new_current->next = current->next;
	}
	hash_table->entries--;
	free(current);
}

struct list* find_elem(struct table* hash_table, void* key)
{
	uint32_t res_hash = hashing(key, hash_table->segments, hash_table->type.key);
	struct list* current;

	current = hash_table->lists[res_hash];
	if (current == NULL)
		return NULL;
	while (current->next != NULL && !cmpr_keys(key, current->key, hash_table->type.key))
	{
		current = current->next;
	}
	if (cmpr_keys(key, current->key, hash_table->type.key))
	{
		return current;
	}
	else
	{
		return NULL;
	}
}