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

int find_list_element(int value, list* head);

int find_elem(int value, htable hashtable);

void add_list_element(list* head, int value);

void delete_list_element(list* head, int value);

void add_first_list_element(list* head, int value);

void add_elem(int value, htable* hashtable, int number);

void delete_elem(int value, htable* hashtable);

void rebalance(htable* hashtable, int number);

void check_balance(htable* hashtable, int number);