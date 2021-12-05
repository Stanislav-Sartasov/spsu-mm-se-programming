#ifndef HT_H
#define HT_H

struct hash_table
{
	int mod;
	int n_elements;
	struct bucket* buckets;
};

void initialize_hash_table(struct hash_table* hash_table, int mod);

void insert(struct hash_table* hash_table, int key, int value);

int get(struct hash_table* hash_table, int key);

void delete(struct hash_table* hash_table, int key);

void display(struct hash_table* hash_table);

void destroy_hash_table(struct hash_table* hash_table);

#endif