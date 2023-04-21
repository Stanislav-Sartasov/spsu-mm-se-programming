#include <stdlib.h>

#include "hash_table.h"

const int MAX_BUCKET_LENGTH = 8;

ht_t* htcreate()
{
	ht_t* table = (ht_t*)malloc(sizeof(ht_t));
	if (table == NULL)
		return NULL;

	table->size = 1;

	table->buckets = (ll_t**)malloc(table->size * sizeof(ll_t*));

	table->buckets[0] = llcreate();

	return table;
}

void htfree(ht_t* table)
{
	for (int i = 0; i < table->size; i++)
		llfree(table->buckets[i]);

	free(table);
}

int hash_function(ht_t* table, int key)
{
	return key % table->size;
}

void rebalance(ht_t* table)
{
	table->size *= 2;

	ll_t** buckets = (ll_t**)malloc(table->size * sizeof(ll_t*));

	for (int i = 0; i < table->size; i++)
		buckets[i] = llcreate();

	for (int i = 0; i < table->size / 2; i++)
	{
		ll_t* old_bucket = table->buckets[i];

		lli_t* elem = old_bucket->next;

		old_bucket->next = elem->next;

		while (elem != NULL)
		{
			ll_t* new_bucket = buckets[hash_function(table, elem->key)];

			lladd(new_bucket, elem->key, elem->value);

			elem = elem->next;
		}

		llfree(old_bucket);
	}

	free(table->buckets);

	table->buckets = buckets;
}

ht_t* htadd(ht_t* table, int key, int value)
{
	ll_t* bucket = table->buckets[hash_function(table, key)];

	bucket = lladd(bucket, key, value);
	if (bucket == NULL)
		return NULL;

	// rebalancing
	for (int i = 0; i < table->size; i++)
	{
		if (table->buckets[i]->length > MAX_BUCKET_LENGTH)
		{
			rebalance(table);
			break;
		}	
	}

	return table;
}

int htget(ht_t* table, int key)
{
	ll_t* bucket = table->buckets[hash_function(table, key)];

	lli_t* elem = llfind(bucket, key);
	if (elem == NULL)
		return HASH_TABLE_KEY_NOT_FOUND;

	return elem->value;
}

int htremove(ht_t* table, int key)
{
	if (htget == HASH_TABLE_KEY_NOT_FOUND)
		return 0;

	ll_t* bucket = table->buckets[hash_function(table, key)];

	return llremove(bucket, key);
}