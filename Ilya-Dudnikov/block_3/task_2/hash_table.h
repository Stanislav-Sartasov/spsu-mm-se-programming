#define MAX_BUCKET_SIZE 20
#define INITIAL_TABLE_SIZE 17

typedef struct linked_list
{
	struct linked_list *next;
	int key;
	int value;
} lst;

typedef struct hash_table
{
	int current_size;
	lst **table;
} hash_table;

void add_pair(hash_table *ht, int key, int value);

void print_table(hash_table *ht);

lst *find(hash_table *ht, int key);

void delete(hash_table *ht, int key);

void initialize_hash_table(hash_table *ht);