#include <stdbool.h>

typedef struct myHashTable
{
	int arg;
	int primeArg;
	int size;
	elem* heads;
} table;

int hashFunc(table* hashTable, int value);

void add(table* hashTable, int value);

void del(table* hashTable, int value);

bool find(table* hashTable, int value);

table* createHashTable();

void deleteHashTable(table* hashTable);

void demoHashTable(table* hashTable);