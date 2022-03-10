#pragma once

#include <stdio.h>
#include <stdlib.h>

typedef struct list
{
	int value;
	struct list* next;
}list;

typedef struct bucket
{
	int countElements;
	list* first;
}bucket;

typedef struct hashTable
{
	int size;
	int countElements;
	bucket* buckets;
}hashTable;

void createTable(hashTable* hasht, int size);
void add(hashTable* hasht, int key);
void del(hashTable* hasht, int key);
void search(hashTable* hasht, int key);
void printTable(hashTable* hasht);
void freeTable(hashTable* hasht);