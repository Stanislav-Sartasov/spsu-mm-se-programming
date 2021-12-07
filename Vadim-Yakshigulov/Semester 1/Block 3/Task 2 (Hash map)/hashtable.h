#pragma once

#include <stdbool.h>
#include "list.h"

#define NOT_FOUND -1

typedef struct hashTable
{
	linkedList **table;
	int size;
	int addedElementsCount;
	double loadFactor;
} hashTable;

hashTable *newHashTable(linkedList **table, int size, int addedElementsCount, double loadFactor);

hashTable *initHashTableByDefault();

void addByKey(hashTable *self, char *key, int value);

void removeByKey(hashTable *self, char *key);

int getValueByKey(hashTable *self, char *key);

void deleteHashTable(hashTable *self);




