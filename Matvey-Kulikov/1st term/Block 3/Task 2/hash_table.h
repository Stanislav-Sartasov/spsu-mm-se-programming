struct pair
{
	int key;
	int value;
	struct pair* next;
};
typedef struct pair pair;

struct hash_table
{
	pair** hash_indexed_array;
	int array_length;
	int pairs_amount;
};
typedef struct hash_table hash_table;

hash_table* new_table();

void add(hash_table* table, int key, int value);

int get(hash_table* table, int key);

void del(hash_table* table, int key);