//
// Created by Вячеслав Бучин on 10.11.2021.
//

#include "int_hash_map.h"

const size_t SIZES[] = { 5897,
						 10399,		21881, 		41233, 		86441,
						 150517, 	303283, 	409099, 	511177,
						 619471,	706283,		813493,		1063561 };
const size_t SIZES_COUNTER = 13;
const int MAX_BUCKET_SIZE = 10;

int size(hash_map_t* map)
{
	return (int)SIZES[map->current_size_ind];
}

hash_map_t* empty_hash_map_reserved_size(int size_ind)
{
	hash_map_t* map = (hash_map_t*) malloc(sizeof(hash_map_t));
	if (map == NULL)
		return NULL;
	map->current_size_ind = size_ind;
	map->buckets = (list_t**) malloc(sizeof(list_t*) * size(map));
	if (map->buckets == NULL)
	{
		free(map);
		return NULL;
	}

	for (int i = 0; i < size(map); i++)
	{
		map->buckets[i] = empty_list();
		if (map->buckets[i] == NULL)
		{
			for (int j = 0; j < i; j++)
				free_list(map->buckets[j]);
			free(map->buckets);
			free(map);
			return NULL;
		}
	}

	return map;
}

hash_map_t* empty_hash_map()
{
	return empty_hash_map_reserved_size(0);
}

void free_hash_map(hash_map_t* map)
{
	for (int i = 0; i < size(map); i++)
		free_list(map->buckets[i]);
	free(map->buckets);
	free(map);
}

int hash(int key, int module)
{
	return (key % module + module) % module;
}

int hash_map_contains(hash_map_t* map, int key)
{
	int hash_code = hash(key, size(map));
	return list_find(map->buckets[hash_code], key) != NULL;
}

list_t* unsafe_insert(hash_map_t* map, int key, int value)
{
	int hash_code = hash(key, (int)SIZES[map->current_size_ind]);
	return list_insert(map->buckets[hash_code], key, value);
}

int rebalance(hash_map_t** map)
{
	int new_size_ind = (*map)->current_size_ind + 1;
	if (new_size_ind == SIZES_COUNTER)
		return -1;

	hash_map_t* new_map = empty_hash_map_reserved_size(new_size_ind);
	if (new_map == NULL)
		return -1;

	for (int i = 0; i < size(*map); i++)
	{
		list_t* current_bucket = (*map)->buckets[i]->next;
		while(current_bucket)
		{
			if (unsafe_insert(new_map, current_bucket->key, current_bucket->value) == NULL)
			{
				free_hash_map(new_map);
				return -1;
			}
			current_bucket = current_bucket->next;
		}
	}

	free_hash_map(*map);
	*map = new_map;
	return 0;
}

int hash_map_insert(hash_map_t** map, int key, int value)
{
	if (key == HASH_MAP_KEY_NOT_FOUND)
		return HASH_MAP_INSERT_REJECTED_RESERVED_KEY;
	if (hash_map_contains(*map, key))
		return HASH_MAP_INSERT_REJECTED_KEY_IS_IN_MAP;

	list_t* bucket = unsafe_insert(*map, key, value);

	if (bucket == NULL)
		return HASH_MAP_INSERT_REJECTED_MALLOC_ERROR;

	if (list_size(bucket) > MAX_BUCKET_SIZE)
		rebalance(map);
	return 0;
}

int hash_map_remove(hash_map_t* map, int key)
{
	if (!hash_map_contains(map, key))
		return 1;
	int hash_code = hash(key, size(map));
	return list_remove(map->buckets[hash_code], key);
}

int hash_map_get(hash_map_t* map, int key)
{
	int hash_code = hash(key, size(map));
	list_t* result = list_find(map->buckets[hash_code], key);
	if (result == NULL)
		return HASH_MAP_KEY_NOT_FOUND;
	return result->value;
}