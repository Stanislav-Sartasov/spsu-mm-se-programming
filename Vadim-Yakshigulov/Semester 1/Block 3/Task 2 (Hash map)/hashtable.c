#include "hashtable.h"
#include <stdlib.h>
#include <math.h>
#include <string.h>

#define DEFAULT_HASHTABLE_SIZE 17
#define DEFAULT_HASHTABLE_LOAD_FACTOR 0.75

bool isSimple(int n)
{
	for (int d = 2; d < (int) sqrt(n) + 1; ++d)
		if (n % d == 0) return false;
	return (n != 1);
}

int nextSimple(int n)
{
	n = (n % 2 == 0) ? n + 1 : n;
	while (!isSimple(n))
		n += 2;
	return n;
}

int hash(char *key, int size)
{
	// size argument must be simple for optimal hash results
	int result = 0;

	for (int i = 0; i < strlen(key); ++i)
		result = ((size + 1) * result + key[i]) % size;

	result = (result * 2 + 1) % size;
	return result;
}

bool isEqualElementByKey(elementOfList *self, elementOfList *element)
{
	return (self->key == element->key);
}

bool isTimeToResize(hashTable *self)
{
	return self->addedElementsCount > self->size * self->loadFactor;
}

void resize(hashTable *self, int size)
{
	linkedList **newTable = malloc(size * sizeof(linkedList *));
	for (int i = 0; i < size; ++i)
		newTable[i] = initLinkedListByDefault();

	for (int i = 0; i < self->size; ++i)
	{
		elementOfList *currentElement = self->table[i]->head;
		while (currentElement->next != NULL)
		{
			currentElement = currentElement->next;
			appendElement(newTable[hash(currentElement->key, size)], currentElement);
		}
		deleteLinkedList(self->table[i]);
	}
	free(self->table);
	self->table = newTable;
	self->size = size;
}

hashTable *newHashTable(linkedList **table, int size, int addedElementsCount, double loadFactor)
{
	hashTable *self = malloc(sizeof(hashTable));
	self->size = size;
	self->addedElementsCount = addedElementsCount;
	self->loadFactor = loadFactor;
	if (table != NULL)
	{
		self->table = table;
		return self;
	}

	self->table = malloc(self->size * sizeof(linkedList *));
	for (int i = 0; i < self->size; ++i)
		self->table[i] = initLinkedListByDefault();

	return self;
}

hashTable *initHashTableByDefault()
{
	return newHashTable(NULL, DEFAULT_HASHTABLE_SIZE, 0, DEFAULT_HASHTABLE_LOAD_FACTOR);
}

void addByKey(hashTable *self, char *key, int value)
{
	elementOfList *elementToAdd = newElement(key, value, NULL);
	linkedList *listWhereElementLocated = self->table[hash(key, self->size)];
	int index = getElementIndex(listWhereElementLocated, elementToAdd, isEqualElementByKey);
	if (index == NOT_FOUND)
		appendElement(listWhereElementLocated, elementToAdd);
	else
		replaceElementByIndex(listWhereElementLocated, elementToAdd, index);
	self->addedElementsCount++;

	if (isTimeToResize(self))
		resize(self, nextSimple(2 * self->size));
}

void removeByKey(hashTable *self, char *key)
{
	elementOfList *searchedElement = initElementByDefault();
	searchedElement->key = key;
	removeElement(self->table[hash(key, self->size)], searchedElement, isEqualElementByKey);
	self->addedElementsCount--;
}


int getValueByKey(hashTable *self, char *key)
{
	linkedList *list = self->table[hash(key, self->size)];
	elementOfList *searchedElement = initElementByDefault();
	searchedElement->key = key;
	int index = getElementIndex(list, searchedElement, isEqualElementByKey);
	return (index == NOT_FOUND) ? NOT_FOUND : getElementByIndex(list, index)->value;
}

void deleteHashTable(hashTable *self)
{
	for (int i = 0; i < self->size; ++i)
		deleteLinkedList(self->table[i]);
	free(self->table);
	free(self);
}
