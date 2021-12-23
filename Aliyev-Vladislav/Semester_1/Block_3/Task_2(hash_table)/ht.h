#ifndef MY_HASH_TABLE
#define MY_HASH_TABLE

typedef void(tb_free)(void*, uintptr_t);
typedef int(tb_cmp)(const void*, const void*);
typedef(tb_hash)(const void*);

typedef struct node
{
	struct node* next;
	void* key;
	uintptr_t val;
} slist;

typedef struct
{
	slist* p;
	size_t i;
} iter_t;

typedef struct
{
	tb_free* pfn_free; //functor to free memory
	tb_cmp* pfn_cmp;  //comparator x == y
	tb_hash* pfn_hash; //hash function
	slist** tabs;
	size_t cnt;
	size_t size;
} table_t;


int cmp_str(const void* a, const void* b);

int iter_each(iter_t* it, table_t* tb, void** key, uintptr_t* val);

int slist_remove(slist** ls, const void* key, table_t* tb);

void slist_clear(slist* ls, table_t* tb);

void iter_reset(iter_t* it);

void free_str(void* p, uintptr_t v);

void table_rebuild(table_t* tb, size_t cnt);

void table_init(table_t* tb, tb_cmp* cmp, tb_hash* hash, tb_free* pfr);

void table_clear(table_t* tb);

void table_remove(table_t* tb, const void* key);

slist* slist_add(slist** ls, void* key, uintptr_t val);

slist* slist_find(slist* ls, const void* key, table_t* tb);

slist* table_find(table_t* tb, const void* key);

slist* table_insert(table_t* tb, void* key, uintptr_t val);

size_t hash_str(const void* p);

#endif