#pragma once
#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <string.h>
#include <stdlib.h>

typedef enum {
	DECIMAL_ELEM,
	REAL_ELEM,
	STRING_ELEM,
} value_type_t;

typedef union {
	int64_t decimal;
	double real;
	uint8_t* string;
} value_t;

typedef struct list
{
	value_t key;
	value_t value;
	struct list* next;
} list;

typedef struct table
{
	list** lists;
	struct
	{
		value_type_t key;
		value_type_t value;
	} type;
	size_t segments, entries;
} table;

void create_table(struct table* hash_table, value_type_t key, value_type_t value);
void free_table(struct table* hash_table);
void add_elem(struct table* hash_table, void* key, void* value);
static uint32_t hash_function_int(uint32_t key, size_t size);
static uint32_t hash_function_str(uint8_t* s, size_t size);
static uint32_t hashing(void* key, size_t size, value_type_t type);
void del_elem(struct table* hash_table, void* key);
struct list* find_elem(struct table* hash_table, void* key);
static void rebalance(struct table* hash_table);
static uint16_t cmpr_keys(void* key, value_t tab_key, value_type_t type);
static void* real(double x);
void print_hash_table(struct table* hash_table);