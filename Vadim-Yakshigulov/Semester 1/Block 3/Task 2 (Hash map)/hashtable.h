#pragma once

#include <stdbool.h>
#include "list.h"

#define NOT_FOUND -1

typedef struct HashTable
{
	LinkedList **table;
	int size;
	int addedElementsCount;
	double loadFactor;
} HashTable;

HashTable *newHashTable(LinkedList **table, int size, int addedElementsCount, double loadFactor);

HashTable *initHashTableByDefault();

void addByKey(HashTable *self, char *key, int value);

void removeByKey(HashTable *self, char *key);

int getValueByKey(HashTable *self, char *key);

void deleteHashTable(HashTable *self);




