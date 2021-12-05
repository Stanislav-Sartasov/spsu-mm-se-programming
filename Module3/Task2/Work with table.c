#include "header.h"

struct hash_table* initialize(int size, struct hash_table* table)
{
	table->size = size;
	table->array_list = malloc(sizeof(struct list) * size);

	for (int i = 0; i < size; i++)
	{
		table->array_list[i].next = NULL;
		table->array_list[i].value = 0;
		table->array_list[i].key = 0;
	}
}

int h_func(int key, struct hash_table* table)
{
	return key % table->size;
}

void h_resize(struct hash_table* table)
{
	int new_size = table->size * 2;

	struct list* new_lists = malloc(sizeof(struct list) * table->size);
	memcpy(new_lists, table->array_list, sizeof(struct list) * table->size);

	free(table->array_list);

	initialize(new_size, table);

	for (int j = 0; j < table->size / 2; j++)
	{
		struct list* l = new_lists + j;
		while (l->next != NULL)
		{
			listAdd(l->value, l->key, table->array_list + h_func(l->key, table));
			l = l->next;
		}
	}


	printf("Изменен размер таблицы, была увеличина в два раза!\n");
}

void h_add(struct hash_table* table, int key, int value)
{
	struct list* list = (table->array_list + h_func(key, table));

	int counter;

	counter = listAdd(value, key, list);

	if (counter == (table->size + 2) / 2)
	{
		h_resize(table);
	}
}

bool h_search(struct hash_table* table, int key, int* num)
{
	struct list* list = table->array_list + h_func(key, table);

	return listSearch(key, list, num);
}

void h_remove(struct hash_table* table, int key)
{
	struct list* list = table->array_list + h_func(key, table);

	while (list != NULL)
	{
		if (list->key == key)
		{
			listValueRemove(key, list);
			printf("%s\n", "Элемент был удален");
			return;
		}
		list = list->next;
	}
	printf("%s\n", "Не смог найти заданный вами элемент! ");
}


void h_print(struct hash_table* table)
{
	for (int i = 0; i < table->size; i++)
	{
		listPrint(table->array_list + i);
		printf("\n");
	}
}

void h_delete(struct hash_table* table)
{
	for (int i = 0; i < table->size; i++)
	{
		listDelete((table->array_list + i)->next);
	}
	free(table->array_list);
}


