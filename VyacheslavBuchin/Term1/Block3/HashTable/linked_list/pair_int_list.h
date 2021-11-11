//
// Created by Вячеслав Бучин on 09.11.2021.
//

#ifndef HASHTABLE_PAIR_INT_LIST_H
#define HASHTABLE_PAIR_INT_LIST_H

#include <stdlib.h>

typedef struct list_item
{
	struct list_item* next;
	int key;
	int value;
} list_t;

list_t* empty_list();

void free_list(list_t* node);

size_t list_size(list_t* list);

int list_empty(list_t* list);

list_t* list_insert(list_t* list, int key, int value);

list_t* list_find(list_t* list, int key);

int list_remove(list_t* list, int key);

#endif //HASHTABLE_PAIR_INT_LIST_H
