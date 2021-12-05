#ifndef header
#define header

struct hash_table
{
	struct list** buckets;
	int module;
};

struct hash_table* init();

void insert(struct hash_table* hash_table, int key, int value);

int get(struct hash_table* hash_table, int key);

void del(struct hash_table* hash_table, int key);

#endif

