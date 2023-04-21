#include "hash_table.h"
#include<stdlib.h>

struct node
{
	int key;
	int value;
	struct node* next;
};

struct list
{
	struct node* head;
	int length;
};

struct list* create_list()
{
	struct list* create = (struct list*)malloc(sizeof(struct list));
	create->head = NULL;
	create->length = 0;
	return create;
}

void insert_list(struct list* lst, int key, int value)
{
	lst->length += 1;

	struct node* new_node = (struct node*)malloc(sizeof(struct node));
	new_node->key = key;
	new_node->value = value;
	new_node->next = NULL;
	struct node* current;
	current = lst->head;
	if (current == NULL)
	{
		lst->head = new_node;
		return;
	}

	while (current->next != NULL)
	{
		current = current->next;
	}

	current->next = new_node;
}

void delete_list_value(struct list* lst, int key)
{
	if (lst->head == NULL)
	{
		return;
	}

	if (lst->head->key == key)
	{
		struct node* next;
		next = lst->head->next;
		free(lst->head);
		lst->head = next;
		lst->length -= 1;
		return;
	}

	struct node* current;
	struct node* prev;
	current = lst->head;
	prev = current;
	while (current->next != NULL)
	{
		if (current->next->key == key)
		{
			struct node* to_remove;
			to_remove = current->next;
			current->next = current->next->next;
			free(to_remove);
			lst->length -= 1;
			return;
		}
		prev = current;
		current = current->next;
	}

	if (current->key == key)
	{
		prev->next = NULL;
		free(current);
	}
}

int get_list(struct list* lst, int key)
{
	if (lst->head == NULL)
	{
		return NULL;
	}

	if (lst->head->key == key)
	{
		return lst->head->value;
	}

	struct node* current;
	current = lst->head;
	while (current->next != NULL)
	{
		if (current->next->key == key)
		{
			return current->next->value;
		}
		current = current->next;
	}

	if (current->key == key)
	{
		return current->value;
	}

	return NULL;
}

void delete_list(struct list* lst)
{
	struct node* current;
	struct node* prev;
	current = lst->head;
	while (current != NULL)
	{
		prev = current;
		current = current->next;
		free(prev);
	}
	lst->head = NULL;
	free(lst);
}

struct hash_table* init()
{
	struct hash_table* new_hash_table = (struct hash_table*)malloc(sizeof(struct hash_table));
	new_hash_table->module = 2;
	new_hash_table->buckets = (struct list**)malloc(sizeof(struct list*) * 2);
	new_hash_table->buckets[0] = create_list();
	new_hash_table->buckets[1] = create_list();
	return new_hash_table;
}

int hash(struct hash_table* hash_table, int arg)
{
	return arg % hash_table->module;
}

void rebalance(struct hash_table* hash_table)
{
	struct list** new_list = (struct list**)malloc(sizeof(struct list*) * 2 * hash_table->module);
	for (int i = 0; i < 2 * hash_table->module; i++)
	{
		new_list[i] = create_list();
	}

	for (int i = 0; i < hash_table->module; i++)
	{
		struct node* current;
		current = hash_table->buckets[i]->head;
		while (current != NULL)
		{
			if ((current->key - i) % (2 * hash_table->module) == hash_table->module)
			{
				insert_list(new_list[i + hash_table->module], current->key, current->value);
			}
			else
			{
				insert_list(new_list[i], current->key, current->value);
			}
			current = current->next;
		}
	}
	for (int i = 0; i < hash_table->module; i++)
	{
		delete_list(hash_table->buckets[i]);
	}
	free(hash_table->buckets);
	hash_table->buckets = new_list;
	hash_table->module *= 2;
}

void insert(struct hash_table* hash_table, int key, int value)
{
	insert_list(hash_table->buckets[hash(hash_table, key)], key, value);
	for (int i = 0; i < hash_table->module; i++)
	{
		if (hash_table->buckets[i]->length > 10)
		{
			rebalance(hash_table);
			return;
		}
	}
}

void del(struct hash_table* hash_table, int key)
{
	delete_list_value(hash_table->buckets[hash(hash_table, key)], key);
}

int get(struct hash_table* hash_table, int key)
{
	get_list(hash_table->buckets[hash(hash_table, key)], key);
}
