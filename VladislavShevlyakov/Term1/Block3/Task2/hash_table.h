struct list
{
	int key;
	int value;
	int total_numbers;
	int tab_size;
	int* sizes;
	int** table;
};

void insert_from_copy(struct list* func);

void rebalancing_the_table(struct list* func);

int find_following_primes(int tab_size);

int hash_function(struct list func);

void find_values(struct list* func);

void insert_value(struct list* func);

void delete_key(struct list* func);

struct list initialize_the_structure();