#ifndef HASH_TABLE
#define HASH_TABLE

#define START_DIVISOR 3
#define REBALANCE_CRITERION 3
#define REBALANCE_MULTIPLIER 2

struct stElement
{
	struct stElement* next;
	int value;
};
typedef struct stElement element;

struct hashTable
{
	element* chainsFirst;
	int divisor;
};
typedef struct hashTable table;

struct tablePointer
{
	table* arrChains;
};
typedef struct tablePointer pointer;

pointer* create();

void add(pointer** p, int value);

pointer* rebalance(pointer** p);

void del(pointer** p, int value);

int search(pointer** p, int value);

void print(pointer** p);

void freeTable(pointer** p);

#endif // HASH_TABLE