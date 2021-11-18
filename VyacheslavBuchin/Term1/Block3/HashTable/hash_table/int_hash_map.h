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

/**
 * @return a pointer to hash map with no elements stored or NULL if memory allocation went wrong
 */
hash_map_t* empty_hash_map();

/**
 * Frees the pointer returned by empty_hash_map function
 * @param map is pointer to the map to be freed
 */
void free_hash_map(hash_map_t* map);

/**
 * Map the given value to given key
 * @param map is a pointer to the pointer to hash map where mapping is performed
 * @return HASH_MAP_INSERT_REJECTED_MALLOC_ERROR if memory allocation for new hash map item went wrong
 * @return HASH_MAP_INSERT_REJECTED_RESERVED_KEY if key is equal to HASH_MAP_KEY_NOT_FOUND
 * @return HASH_MAP_INSERT_REJECTED_KEY_IS_IN_MAP is given key is already in hash map
 * @return 0 otherwise
 */
int hash_map_insert(hash_map_t** map, int key, int value);

/**
 * Removes key and associated value from hash map
 * @param map is a pointer to the map where removing is performed
 * @param key is key to be removed
 * @return 1 if there is no such key in the hash map else 0
 */
int hash_map_remove(hash_map_t* map, int key);

/**
 * @param map is a pointer to the map where searching is performed
 * @param key is a key to be found
 * @return 1 if key is found, 0 otherwise
 */
int hash_map_contains(hash_map_t* map, int key);

/**
 * Searches the value associated with given key
 * @param map is a pointer to the map where searching is performed
 * @param key is a key, value associated with is to be found
 * @return the value associated with given key or HASH_MAP_KEY_NOT_FOUND if there is no such
 */
int hash_map_get(hash_map_t* map, int key);

#endif //HASHTABLE_INT_HASH_MAP_H
