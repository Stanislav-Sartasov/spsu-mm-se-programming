#pragma once

struct l
{
	struct l* next;
	int value;
	int key;
};
typedef struct l list;

struct t
{
	struct list** first_elements;
	int* lengths;
	int max_length;
	int size;
};
typedef struct t table;

struct table* create_table(int size);

void drop_table(table* keys);

void add_element(table* keys, int key, int value);

void del_element(table* keys, int key);

int find_element(table* keys, int key);