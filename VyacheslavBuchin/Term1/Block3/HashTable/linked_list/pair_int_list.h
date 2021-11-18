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

/**
 * @return a list with one node which key is the size of whole list (equals 0 at start)
 */
list_t* empty_list();

/**
 * Frees the pointer returned by empty_list function
 * @param list is a pointer to list to be freed
 */
void free_list(list_t* list);

/**
 * @return a count of significant elements in the given list
 */
size_t list_size(list_t* list);

int list_empty(list_t* list);

/**
 * @param list is a pointer to list where insertion should be performed
 * @param key is a key to be inserted
 * @param value is a value to be inserted
 * @return a pointer to list where insertion is performed or NULL if insertion went wrong
 */
list_t* list_insert(list_t* list, int key, int value);

/**
 * @param list is a pointer to list where searching
 * @param key is a key to be found
 * @return a pointer to first list item with requested key or NULL if list contains no such key
 */
list_t* list_find(list_t* list, int key);

/**
 * @param list is a poiner to list where removing is performed
 * @param key is a key to be removed
 * @return 1 if removing was performed else 0
 */
int list_remove(list_t* list, int key);

#endif //HASHTABLE_PAIR_INT_LIST_H
