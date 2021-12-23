#ifndef header
#define header

struct hash_table
{
	struct list** cell;
	int mod;
};
struct hash_table* init();
int get(struct hash_table* hash_table, int key);
void del(struct hash_table* hash_table, int key);
void insert(struct hash_table* hash_table, int key, int value);

#endif
