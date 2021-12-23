#pragma once
#ifndef HASH_TABLE_H
#define HASH_TABLE_H



struct hashTable
{
	int size;
	int elements;
	struct list* lst;
};




void createTable(struct hashTable* hT, int size);

void insertElement(struct hashTable* hT, int key, int value);

int searchElement(struct hashTable* hT, int key);

void removeElement(struct hashTable* hT, int key);

void deleteHashTable(struct hashTable* hT);





#endif 