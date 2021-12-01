#ifndef HASH_TABLE_H
#define HASH_TABLE_H

#include "../linked_list/linked_list.h"

#define HASH_TABLE_KEY_NOT_FOUND NULL

// hash table type
typedef struct hash_table
{
	ll_t** buckets;
	int size;
} ht_t;

ht_t* htcreate();

void htfree(ht_t* table);

ht_t* htadd(ht_t* table, int key, int value);

int htget(ht_t* table, int key);

int htremove(ht_t* table, int key);

#endif // HASH_TABLE_H