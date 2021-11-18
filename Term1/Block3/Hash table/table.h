#pragma once

typedef struct List
{
	int value;
	struct List* next;
}List;

typedef struct Bucket
{
	int countElements;
	List* first;
}Bucket;

typedef struct HashTable
{
	int size;
	int countElements;
	Bucket* buckets;
}HashTable;

void createTable(HashTable* hasht, int size);
void add(HashTable* hasht, int key);
void del(HashTable* hasht, int key);
void search(HashTable* hasht, int key);
void printTable(HashTable* hasht);
void freeTable(HashTable* hasht);