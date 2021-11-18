#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <stdlib.h>
#include "table.h"

#define SUCCESS 1
#define FAIL 0


static int _add(List** head, int key);
static int _delete(List** head, int key);
static int _search(List* head, int key);
static int hash(int key, int size);
static int resize(int size);
static void rebalance(HashTable* hasht);


static int hash(int key, int size)
{
	return (key % size);
}

static int resize(int size)
{
	size = size * 3 + 1;
	int flag = 1;
	while (flag)
	{
		flag = 0;
		for (int div = 2; div * div <= size; div++)
			if (size % div == 0)
			{
				flag = 1;
				size++;
				break;
			}
	}
	return size;
}

static void rebalance(HashTable* hasht)
{
	HashTable new;
	createTable(&new, resize(hasht->size));

	for (int i = 0; i < hasht->size; i++)
		if (hasht->buckets[i].first != NULL)
		{
			List* current = hasht->buckets[i].first;
			while (current->next != NULL)
			{
				add(&new, current->value);
				current = current->next;
			}
			add(&new, current->value);
		}

	freeTable(hasht);
	*hasht = new;
}

void createTable(HashTable* hasht, int size)
{
	hasht->buckets = (Bucket*)malloc(sizeof(Bucket) * size);
	hasht->countElements = 0;
	hasht->size = size;
	for (int i = 0; i < size; i++)
	{
		hasht->buckets[i].first = NULL;
		hasht->buckets[i].countElements = 0;
	}
}

void freeTable(HashTable* hasht)
{
	for (int i = 0; i < hasht->size; i++)
	{
		List* current = hasht->buckets[i].first;
		if (current == NULL)
			continue;

		List* next = NULL;
		while (current->next != NULL)
		{
			next = current->next;
			free(current);
			current = next;
		}
		free(current);
	}
	free(hasht->buckets);
}

void printTable(HashTable* hasht)
{
	printf("\n");
	for (int i = 0; i < hasht->size; i++)
		if (hasht->buckets[i].first != NULL)
		{
			printf("%d: [", i);
			List* current = hasht->buckets[i].first;
			while (current->next != NULL)
			{
				printf("%d; ", current->value);
				current = current->next;
			}
			printf("%d]\n", current->value);
		}
	printf("\n");
}

static int _add(List** head, int key)
{
	int value = key;

	if (*head == NULL)
	{
		*head = (List*)malloc(sizeof(List));
		(*head)->value = value;
		(*head)->next = NULL;
		return SUCCESS;
	}

	List* current = *head;
	List* prev = NULL;
	while (current->next != NULL && current->value < value)
	{
		prev = current;
		current = current->next;
	}
	if (current->value == value)
		return FAIL;

	if (prev == NULL && current->value > value)
	{
		List* first = (List*)malloc(sizeof(List));
		first->value = value;
		first->next = *head;
		*head = first;
		return SUCCESS;
	}

	if (current->next == NULL && current->value < value)
	{
		List* last = (List*)malloc(sizeof(List));
		current->next = last;
		last->value = value;
		last->next = NULL;
		return SUCCESS;
	}

	List* between = (List*)malloc(sizeof(List));
	between->value = value;
	between->next = current;
	prev->next = between;
	return SUCCESS;
}

void add(HashTable* hasht, int key)
{
	int n = hash(key, hasht->size);

	if (_add(&hasht->buckets[n].first, key))
	{
		hasht->buckets[n].countElements++;
		hasht->countElements++;
	}

	if ((hasht->buckets[n].countElements > 2 * hasht->countElements / hasht->size + 2) ||
		(hasht->countElements / hasht->size >= 3))
		rebalance(hasht);
}

static int _delete(List** head, int key)
{
	int value = key;

	if (*head == NULL)
		return FAIL;

	if ((*head)->value == value)
	{
		List* toDelete = *head;
		*head = (*head)->next;
		free(toDelete);
		return SUCCESS;
	}

	List* current = *head;
	List* prev = NULL;
	while (current->next != NULL && current->value < value)
	{
		prev = current;
		current = current->next;
	}

	if (current->value == value)
	{
		prev->next = current->next;
		free(current);
		return SUCCESS;
	}

	return FAIL;
}

void del(HashTable* hasht, int key)
{
	int n = hash(key, hasht->size);

	if (_delete(&hasht->buckets[n].first, key))
	{
		hasht->buckets[n].countElements--;
		hasht->countElements--;
	}
}

static int _search(List* head, int key)
{
	int value = key;

	if (head == NULL)
		return FAIL;

	List* current = head;
	while (current->next != NULL && current->value < value)
	{
		current = current->next;
	}
	if (current->value == value)
		return SUCCESS;

	return FAIL;
}

void search(HashTable* hasht, int key)
{
	int n = hash(key, hasht->size);
	if (_search(hasht->buckets[n].first, key))
		printf("Element %d was found\n", key);
	else
		printf("Element %d was not found\n", key);
}