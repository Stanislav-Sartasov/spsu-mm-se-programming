#include <stdlib.h>
#include <stdio.h>
#include "hashtable.h"


struct node
{
	int key;
	int value;
	struct node* next; 
}; 



struct list 
{
	int len;
	struct node* first;
};





void rebalance(struct hashTable* hT)
{
	int newSize = hT->size * 2;
	struct hashTable* newHT = malloc(sizeof(struct hashTable));
	newHT->size = newSize;
	createTable(newHT, newSize);
	for (int i = 0; i < hT->size; i++)
	{
		struct node* current = hT->lst[i].first;
		if (current == NULL)
		{
			continue;
		}
		while (current->next != NULL)
		{
			insertElement(newHT, current->key, current->value);
			current = current->next;
		}
		insertElement(newHT, current->key, current->value);
	}
	free(hT->lst);
	hT->size = newSize;
	hT->lst = newHT->lst;
	free(newHT);
}



void checkBalancing(struct hashTable* hT, int hash_key)
{
	if (hT->lst[hash_key].len > ((hT->size) / 2 + 1))
	{
		rebalance(hT);
	}
}




void deleteHashTable(struct hashTable* hT)
{
	for (int i = 0; i < hT->size; i++)
	{
		struct node* current = hT->lst[i].first;
		struct node* next;
		if (current == NULL)
		{
			continue;
		}
		while (current->next != NULL)
		{
			next = current->next;
			free(current);
			current = next;
		}
		free(current);
	}
	free(hT->lst);
}



int hashFunction(struct hashTable* hT, int key)
{
	return key % (hT->size);
}



void createTable(struct hashTable* hT, int size)
{
	struct list* lists = malloc(sizeof(struct list) * size);
	for (int i = 0; i < size; i++)
	{
		lists[i].len = 0;
		lists[i].first = NULL;
	}
	hT->elements = 0;
	hT->size = size;
	hT->lst = lists;
}






void insertElement(struct hashTable* hT, int key, int value)
{

	struct node* newNode = malloc(sizeof(struct node));
	newNode->key = key;
	newNode->value = value;
	newNode->next = NULL;

	int hash = hashFunction(hT, key);
	struct node* current = hT->lst[hash].first;


	if (current == NULL)
	{
		hT->lst[hash].first = newNode;
		hT->lst[hash].len += 1;
		hT->elements += 1;
		return;
	}

	while (current->next != NULL)
	{
		current = current->next;
		hT->lst[hash].len += 1;
		hT->elements += 1;
	}
	current->next = newNode;
	checkBalancing(hT, hash);
	return;
}




void removeElement(struct hashTable* hT, int key)
{
	int hash = hashFunction(hT, key);
	struct node* current = hT->lst[hash].first;

	if (current == NULL)
	{
		return;
	}

	if (key == current->key)
	{
		hT->lst[hash].first = current->next;
		free(current);
		hT->lst[hash].len -= 1;
		hT->elements -= 1;
		return;
	}
	struct node* prev = NULL;

	while (current->next != NULL)
	{
		prev = current;
		current = current->next;
	}

	if (current->key == key)
	{
		prev->next = current->next;
		free(current);
		hT->lst[hash].len -= 1;
		hT->elements -= 1;
		return;
	}

}



int searchElement(struct hashTable* hT, int key)
{
	int hash = hashFunction(hT, key);
	struct node* current = hT->lst[hash].first;
	while (current != NULL)
	{
		if (current->key == key)
		{
			return current->value;
		}
		current = current->next;
	}
	return 0;
}
