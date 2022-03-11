#ifndef EX7_HEADER_H
#define EX7_HEADER_H

#include <stdbool.h>
#include <stdlib.h>
#include <stdio.h>
#include <string.h>

struct list
{
	int value;
	int key;
	struct list* next;
};

struct hash_table
{
	struct list* array_list;
	int size;
};

struct hash_table* initialize(int size, struct hash_table*);

int listAdd(int, int, struct list*);

void listDelete(struct list*);

void listValueRemove(int, struct list*);

bool listSearch(int, struct list*, int*);

void listPrint(struct list*);






int h_func(int, struct hash_table*);

void h_resize(struct hash_table*);

bool h_search(struct hash_table*, int, int*);

void h_remove(struct hash_table*, int);

void h_print(struct hash_table*);

void h_delete(struct hash_table*);

void h_add(struct hash_table*, int, int);

#endif //EX7_HEADER_H
