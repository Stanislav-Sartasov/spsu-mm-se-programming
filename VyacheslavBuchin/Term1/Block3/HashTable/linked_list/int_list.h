//
// Created by Вячеслав Бучин on 09.11.2021.
//

#ifndef HASHTABLE_INT_LIST_H
#define HASHTABLE_INT_LIST_H

#include <stdlib.h>

typedef struct list_item
{
	int value;
	struct list_item* next;
} list_t;

list_t* empty_list();

void free_list(list_t* node);

size_t list_size(list_t* list);

int list_empty(list_t* list);

list_t* list_insert(list_t* list, int value);

list_t* list_insert_after(list_t* list, list_t* node, int value);

list_t* list_find(list_t* list, int value);

int list_remove(list_t* list, int value);

#endif //HASHTABLE_INT_LIST_H
