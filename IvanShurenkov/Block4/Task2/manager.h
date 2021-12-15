struct block_data
{
	int size;
	char is_use;
	struct block *next;
};

typedef struct block_data block;

void init_memory();

void *my_malloc(int size);

void *my_calloc(int size);

void *my_realloc(void *ptr, int size);

void my_free(void *ptr);

void free_memory();