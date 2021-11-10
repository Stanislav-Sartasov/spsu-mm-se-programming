//
// Created by Вячеслав Бучин on 09.11.2021.
//

#include "pair_int_list.h"

list_t* list_create_node(int key, int value)
{
	list_t* result = (list_t*) malloc(sizeof(list_t));
	if (result == NULL)
		return NULL;
	result->key = key;
	result->value = value;
	result->next = NULL;
	return result;
}

/**
 * @return a list with one node that contains count of significant nodes in list
 */
list_t* empty_list()
{
	return list_create_node(0, 0);
}

void free_list(list_t* node)
{
	if (node->next)
		free_list(node->next);
	free(node);
}

size_t list_size(list_t* list)
{
	return list->key;
}

int list_empty(list_t* list)
{
	return list_size(list) == 0;
}

/**
 * @return a pointer to inserted key
 */
list_t* list_insert(list_t* list, int key, int value)
{
	list->key++;
	while (list->next)
		list = list->next;
	list->next = list_create_node(key, value);

	return list->next;
}

/**
 * @return a pointer to inserted key
 */
list_t* list_insert_after(list_t* list, list_t* node, int key, int value)
{
	list->key++;
	list_t* right = node->next;
	node->next = list_create_node(key, value);
	node->next->next = right;
	return node->next;
}

int list_remove(list_t* list, int key)
{
	if (!list || list_empty(list))
		return 0;
	list_t* head = list;
	while (list->next)
	{
		if (list->next->key == key)
		{
			list_t* temp = list->next;
			list->next = temp->next;
			free(temp);
			head->key--;
			return 1;
		}
		list = list->next;
	}
	return 0;
}

list_t* list_find(list_t* list, int key)
{
	list = list->next;
	while (list)
	{
		if (list->key == key)
		{
			return list;
		}
		list = list->next;
	}
	return NULL;
}
