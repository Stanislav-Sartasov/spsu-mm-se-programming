#pragma once
#include <stdint.h>

typedef struct node
{
    int32_t key;
    int32_t value;
    struct node* left;
    struct node* right;
    struct node* parent;
} node;

typedef struct table
{
    node** lists;
    int32_t elements;
    int32_t segments;
} table;


table* createTab(size_t sizeTable);
void freeTab(table* tab);
void add(table** tab, uint32_t key, int32_t val);
node* find(table* tab, uint32_t key);
size_t delete(table* tab, uint32_t key);