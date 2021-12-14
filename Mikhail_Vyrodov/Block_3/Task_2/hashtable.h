#pragma once

struct list
{
	int value;
	struct list* next;
};

typedef struct list list;

struct hashtable
{
	int count;
	list* table;
};

typedef struct hashtable htable;

htable create_htable(int number);

int find_elem(int value, htable hashtable);

void add_elem(int value, htable* hashtable);

void delete_elem(int value, htable* hashtable);