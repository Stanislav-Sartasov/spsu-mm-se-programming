#ifndef MY_HASH_TABLE
#define MY_HASH_TABLE
#include "my_list.h"

struct my_hash_table 
{
	struct my_list_holder *buckets;
	int* bucket_lengths;
	int current_base;
};

void add(struct my_hash_table* table, int key, int value);
int remove(struct my_hash_table* table, int key);
int get(struct my_hash_table* table, int key);
struct my_hash_table * create_hash_table();

#endif