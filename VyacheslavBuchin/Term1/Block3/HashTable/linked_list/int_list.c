//
// Created by Вячеслав Бучин on 09.11.2021.
//

#include "int_list.h"

list_t* list_create_node(int value)
{
	list_t* result = (list_t*) malloc(sizeof(list_t));
	result->value = value;
	result->next = NULL;
	return result;
}

/**
 * @return a list with one node that contains count of significant nodes in list
 */
list_t* empty_list()
{
	return list_create_node(0);
}

void free_list(list_t* node)
{
	if (node->next)
		free_list(node->next);
	free(node);
}

size_t list_size(list_t* list)
{
	return list->value;
}

int list_empty(list_t* list)
{
	return list_size(list) == 0;
}

/**
 * @return a pointer to inserted value
 */
list_t* list_insert(list_t* list, int value)
{
	list->value++;
	while (list->next)
		list = list->next;
	list->next = list_create_node(value);

	return list->next;
}

/**
 * @return a pointer to inserted value
 */
list_t* list_insert_after(list_t* list, list_t* node, int value)
{
	list->value++;
	list_t* right = node->next;
	node->next = list_create_node(value);
	node->next->next = right;
	return node->next;
}

int list_remove(list_t* list, int value)
{
	if (!list || list_empty(list))
		return 0;
	list_t* head = list;
	while (list->next)
	{
		if (list->next->value == value)
		{
			list_t* temp = list->next;
			list->next = temp->next;
			free(temp);
			head->value--;
			return 1;
		}
		list = list->next;
	}
	return 0;
}

list_t* list_find(list_t* list, int value)
{
	list = list->next;
	while (list)
	{
		if (list->value == value)
		{
			return list;
		}
		list = list->next;
	}
	return NULL;
}
