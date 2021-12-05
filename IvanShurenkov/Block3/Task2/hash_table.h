#ifdef IS_MSVC
typedef __int64 INT64;
typedef __int32 INT32;
typedef __int8 INT8;
#else
typedef __int64_t INT64;
typedef __int32_t INT32;
typedef __int8_t INT8;
#endif

struct node
{
	INT32 key;
	INT32 value;
	struct node *next;
};

struct hash_table
{
	INT32 size;
	INT32 *lengths;
	struct node **linked_lists;
};
typedef struct hash_table hash_table;

struct hash_table *create_table(INT32 size);

void add_element(hash_table *table, INT32 key, INT32 value);

INT8 find_element(hash_table *table, INT32 key, INT32 *value);

void delete_element(hash_table *table, INT32 key);

void free_table(hash_table *table);
