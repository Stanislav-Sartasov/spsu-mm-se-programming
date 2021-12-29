#include <stdio.h>
#include "hashtable.h"

void printfTab(table* tab);
void printfNodes(node* node);

int main()
{
	printf("the program tests the functionality of the hash table functions (add, delete, find)\n");
	table* tab = createTab(16);
	for (size_t i = 1; i < 13; i++)
	{
		add(&tab, i, i * i);
		printf("added key = %d value = %d pair\n", i, i * i);
	}
	printfTab(tab);
	for (size_t i = 1; i < 5; i++)
	{
		node* findElem = find(tab, i);
		if (findElem != NULL)
		{
			printf("element have found >> [key >> %d; value >> %d]\n", findElem->key, findElem->value);
		}
		else
		{
			printf("element with key = %d have not found\n", i);
		}
	}
	for (size_t i = 1; i < 5; i++)
	{
		delete(tab, i);
		printf("deleting an element by key = %d\n", i);
	}
	for (size_t i = 1; i < 5; i++)
	{
		node* findElem = find(tab, i);
		if (findElem != NULL)
		{
			printf("element have found >> [key >> %d; value >> %d]\n", findElem->key, findElem->value);
		}
		else
		{
			printf("element with key = %d have not found\n", i);
		}
	}
	printfTab(tab);
	freeTab(tab);
	return 0;
}

void printfTab(table* tab)
{
	printf("interface of hash-table:\n");
	for (size_t i = 0; i < tab->segments; i++)
	{
		if (tab->lists[i] != NULL)
		{
			printfNodes(tab->lists[i]);
		}
	}
}
void printfNodes(node* node)
{
	printf("[key >> %d; value >> %d]\n", node->key, node->value);
	if (node->left != NULL)
	{
		printfTab(node->left);
	}
	if (node->right != NULL)
	{
		printfTab(node->right);
	}
}