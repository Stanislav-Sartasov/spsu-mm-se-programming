#pragma once

/*Description: Hash-map.
for deleting key use <delete>;
		0 will be returned if no such key in hashTable
		1 will be returned if deleting will be competed

add(hashTable* hashTable, int inntialKey, int value) will notice You if there are same key and replace its value by new.

for getting value use <get>;
		NULL will be returned if no such key in hashTable
		[value] will be returned if value will be founded
*/

struct element
{
	int key;
	int value;
	struct element* next;
};
typedef struct element element;

struct hash_table
{
	int balance;
	struct element** keys;
};
typedef struct hash_table hash_table;


void create_table(hash_table* hash_table, int size);

void show_table(hash_table* hash_table);

void add(hash_table* hash_table, int initial_key, int value);

int get(hash_table* hash_table, int initial_key);

int delete(hash_table* hash_table, int initial_key);

void delete_table(hash_table* hash_table, int size);