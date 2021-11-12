//
// Created by Вячеслав Бучин on 10.11.2021.
//

#ifndef HASHTABLE_INT_HASH_MAP_H
#define HASHTABLE_INT_HASH_MAP_H

#include <stdlib.h>

#include "../linked_list/pair_int_list.h"

#define HASH_MAP_KEY_NOT_FOUND -(1 << 30) + 228 - 1337
#define HASH_MAP_INSERT_REJECTED_MALLOC_ERROR -2
#define HASH_MAP_INSERT_REJECTED_RESERVED_KEY -3
#define HASH_MAP_INSERT_REJECTED_KEY_IS_IN_MAP -4

typedef struct hash_map
{
	list_t** buckets;
	int current_size_ind;
} hash_map_t;

hash_map_t* empty_hash_map();

void free_hash_map(hash_map_t* map);

int hash_map_insert(hash_map_t** map, int key, int value);

int hash_map_remove(hash_map_t* map, int key);

int hash_map_contains(hash_map_t* map, int key);

int hash_map_get(hash_map_t* map, int key);

#endif //HASHTABLE_INT_HASH_MAP_H
