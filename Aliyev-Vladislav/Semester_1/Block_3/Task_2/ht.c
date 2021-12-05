#include "ht.h"
#include<stdlib.h>

struct junction
{
	int key;
	int value;
	struct junction* next;
};

struct list
{
	struct junction* head;
	int length;
};

struct list* form_list()
{
	struct list* form = (struct list*)malloc(sizeof(struct list));
	form->head = NULL;
	form->length = 0;
	return form;
}

void insert_list(struct list* lst, int key, int value)
{
	lst->length += 1;

	struct junction* new_junction = (struct junction*)malloc(sizeof(struct junction));
	new_junction->key = key;
	new_junction->value = value;
	new_junction->next = NULL;
	struct junction* present;
	present = lst->head;
	if (present == NULL)
	{
		lst->head = new_junction;
		return;
	}

	while (present->next != NULL)
	{
		present = present->next;
	}

	present->next = new_junction;
}

void del_list_value(struct list* lst, int key)
{
	if (lst->head == NULL)
	{
		return;
	}

	if (lst->head->key == key)
	{
		struct junction* next;
		next = lst->head->next;
		free(lst->head);
		lst->head = next;
		lst->length -= 1;
		return;
	}

	struct junction* present;
	struct junction* initial;
	present = lst->head;
	initial = present;
	while (present->next != NULL)
	{
		if (present->next->key == key)
		{
			struct junction* to_del;
			to_del = present->next;
			present->next = present->next->next;
			free(to_del);
			lst->length -= 1;
			return;
		}
		initial = present;
		present = present->next;
	}

	if (present->key == key)
	{
		initial->next = NULL;
		free(present);
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

	struct junction* present;
	present = lst->head;
	while (present->next != NULL)
	{
		if (present->next->key == key)
		{
			return present->next->value;
		}
		present = present->next;
	}

	if (present->key == key)
	{
		return present->value;
	}

	return NULL;
}

void del_list(struct list* lst)
{
	struct junction* present;
	struct junction* initial;
	present = lst->head;
	while (present != NULL)
	{
		initial = present;
		present = present->next;
		free(initial);
	}
	lst->head = NULL;
	free(lst);
}

struct hash_table* init()
{
	struct hash_table* new_hash_table = (struct hash_table*)malloc(sizeof(struct hash_table));
	new_hash_table->mod = 2;
	new_hash_table->cell = (struct list**)malloc(sizeof(struct list*) * 2);
	new_hash_table->cell[0] = form_list();
	new_hash_table->cell[1] = form_list();
	return new_hash_table;
}

int hash(struct hash_table* hash_table, int arg)
{
	return arg % hash_table->mod;
}

void rebalance(struct hash_table* hash_table)
{
	struct list** new_list = (struct list**)malloc(sizeof(struct list*) * 2 * hash_table->mod);
	for (int i = 0; i < 2 * hash_table->mod; i++)
	{
		new_list[i] = form_list();
	}

	for (int i = 0; i < hash_table->mod; i++)
	{
		struct junction* present;
		present = hash_table->cell[i]->head;
		while (present != NULL)
		{
			if ((present->key - i) % (2 * hash_table->mod) == hash_table->mod)
			{
				insert_list(new_list[i + hash_table->mod], present->key, present->value);
			}
			else
			{
				insert_list(new_list[i], present->key, present->value);
			}
			present = present->next;
		}
	}
	for (int i = 0; i < hash_table->mod; i++)
	{
		del_list(hash_table->cell[i]);
	}
	free(hash_table->cell);
	hash_table->cell = new_list;
	hash_table->mod *= 2;
}

void insert(struct hash_table* hash_table, int key, int value)
{
	insert_list(hash_table->cell[hash(hash_table, key)], key, value);
	for (int i = 0; i < hash_table->mod; i++)
	{
		if (hash_table->cell[i]->length > 10)
		{
			rebalance(hash_table);
			return;
		}
	}
}

void del(struct hash_table* hash_table, int key)
{
	del_list_value(hash_table->cell[hash(hash_table, key)], key);
}

int get(struct hash_table* hash_table, int key)
{
	get_list(hash_table->cell[hash(hash_table, key)], key);
}
