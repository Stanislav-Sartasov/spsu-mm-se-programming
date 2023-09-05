#ifndef TABLE_ENTRAILS
#define TABLE_ENTRAILS

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

pointer* rebalance(pointer** p);

#endif // TABLE_ENTRAILS