#pragma once

struct list
{
	int value;
	struct list* next;
};

typedef struct list list;

struct hash_table
{
	int count;
	list* table;
};

typedef struct hash_table htable;

htable create_htable(int number);

int find_elem(int value, htable hash_table);

void add_elem(int value, htable* hash_table);

void delete_elem(int value, htable* hash_table);