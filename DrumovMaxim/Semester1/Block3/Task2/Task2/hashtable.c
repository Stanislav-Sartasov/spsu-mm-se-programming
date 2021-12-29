#include "hashtable.h"
#include <stdlib.h>
#include <math.h>

#define CONDITION 0.75

uint32_t hash(uint32_t key, size_t sizeTab);
table* rebalance(table* tab);
void copy(table* tab, node* elem);

table* createTab(size_t sizeTable)
{
	uint32_t newSize = pow(2, log2(sizeTable));
	if (newSize != sizeTable)
	{
		sizeTable = pow(2, (int)log2(sizeTable) + 1);
	}
	table* init = (struct table*)malloc(sizeof(struct table));
	init->elements = 0;
	init->segments = sizeTable;
	init->lists = (struct node**)malloc(sizeof(struct node*) * init->segments);
	for (size_t i = 0; i < init->segments; i++)
	{
		init->lists[i] = NULL;
	}
	return init;
}

void freeTab(table* tab)
{
	for (size_t i = 0; i < tab->segments; i++)
	{
		if (tab->lists[i] == NULL)
		{
			continue;
		}
		uint32_t del_key = tab->lists[i]->key;
		while (!delete(tab, del_key))
		{
			del_key = tab->lists[i]->key;
		}
	}
	free(tab->lists);
	free(tab);
}

table* rebalance(table* tab)
{
	table* newTab = createTab((tab)->segments * 2);
	for (size_t i = 0; i < (tab)->segments; i++)
	{
		if ((tab)->lists[i] != NULL)
		{
			copy(newTab, (tab)->lists[i]);
		}
	}
	freeTab(tab);
	return newTab;
}

void add(table** tab, uint32_t key, int32_t val)
{
	node* prev = find(*tab, key);
	if (prev != NULL)
	{
		prev->value = val;
		return;
	}
	uint32_t h = hash(key, (*tab)->segments);
	node* cur = (*tab)->lists[h];
	if (cur == NULL)
	{
		(*tab)->lists[h] = (struct node*)malloc(sizeof(struct node));
		(*tab)->lists[h]->key = key;
		(*tab)->lists[h]->value = val;
		(*tab)->lists[h]->parent = NULL;
		(*tab)->lists[h]->left = NULL;
		(*tab)->lists[h]->right = NULL;
		(*tab)->elements++;
	}
	else
	{
		prev = cur;
		while (cur != NULL)
		{
			if (cur->key > key)
			{
				prev = cur;
				cur = cur->left;
			}
			else
			{
				prev = cur;
				cur = cur->right;
			}
		}
		if (prev->key > key)
		{
			prev->left = (struct node*)malloc(sizeof(struct node));
			prev->left->parent = prev;
			prev->left->key = key;
			prev->left->value = val;
			prev->left->right = NULL;
			prev->left->left = NULL;
		}
		else
		{
			prev->right = (struct node*)malloc(sizeof(struct node));
			prev->right->parent = prev;
			prev->right->key = key;
			prev->right->value = val;
			prev->right->right = NULL;
			prev->right->left = NULL;
		}
		(*tab)->elements++;
	}
	if ((double)(*tab)->elements / (*tab)->segments >= CONDITION)
	{
		*tab = rebalance(*tab);
	}
}


uint32_t hash(uint32_t key, size_t size_tab)
{
	key ^= (key << 13);
	key ^= (key >> 17);
	key ^= (key << 5);
	return key % size_tab;
}

node* find(table* tab, uint32_t key)
{
	uint32_t h = hash(key, tab->segments);
	node* cur = tab->lists[h];
	if (cur == NULL)
	{
		return NULL;
	}
	while (cur->key != key)
	{
		if (cur->key < key && cur->right != NULL)
		{
			cur = cur->right;
		}
		else if (cur->key > key && cur->left != NULL)
		{
			cur = cur->left;
		}
		return NULL;
	}
	return cur;
}

size_t delete(table* tab, uint32_t key)
{
	node* cur = find(tab, key);
	if (cur == NULL)
	{
		return 0;
	}
	if (cur->left != NULL && cur->right != NULL)
	{
		node* newNode = cur->right;
		while (newNode->left != NULL)
		{
			newNode->left;
		}
		uint32_t saveKey = newNode->key;
		uint32_t saveValue = newNode->value;
		delete(tab, saveKey);
		cur->key = saveKey;
		cur->value = saveValue;
	}
	else if (cur->right != NULL)
	{
		uint32_t saveKey = cur->right->key;
		uint32_t saveValue = cur->right->value;
		delete(tab, saveKey);
		cur->key = saveKey;
		cur->value = saveValue;
	}
	else if (cur->left != NULL)
	{
		uint32_t saveKey = cur->left->key;
		uint32_t saveValue = cur->left->value;
		delete(tab, saveKey);
		cur->key = saveKey;
		cur->value = saveValue;
	}
	else
	{
		if (cur->parent != NULL)
		{
			if (cur->parent->left == cur)
			{
				cur->parent->left = NULL;
			}
			else
			{
				cur->parent->right = NULL;
			}
		}
		else
		{
			uint32_t h = hash(key, tab->segments);
			free(tab->lists[h]);
			tab->lists[h] = NULL;
			tab->elements--;
			return -1;
		}
		free(cur);
		cur = NULL;
	}
	tab->elements--;
	return 0;
}

void copy(table* tab, node* elem)
{
	add(&tab, elem->key, elem->value);
	if (elem->left != NULL)
	{
		copy(tab, elem->left);
	}
	if (elem->right != NULL)
	{
		copy(tab, elem->right);
	}
}