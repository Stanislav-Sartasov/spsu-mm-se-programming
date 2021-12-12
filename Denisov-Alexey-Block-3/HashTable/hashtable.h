#include "tableEntrails.h"

#ifndef HASH_TABLE
#define HASH_TABLE

pointer* create();

void add(pointer** p, int value);

void del(pointer** p, int value);

int search(pointer** p, int value);

void print(pointer** p);

void freeTable(pointer** p);

#endif // HASH_TABLE