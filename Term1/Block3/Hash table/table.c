#define _CRT_SECURE_NO_WARNINGS

#include "table.h"

#define SUCCESS 1
#define FAIL 0


int _add(list** head, int key);
int _delete(list** head, int key);
int _search(list* head, int key);
int hash(int key, int size);
int resize(int size);
void rebalance(hashTable* hasht);


int hash(int key, int size)
{
	return (key % size);
}

int resize(int size)
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

void rebalance(hashTable* hasht)
{
	hashTable new;
	createTable(&new, resize(hasht->size));

	for (int i = 0; i < hasht->size; i++)
		if (hasht->buckets[i].first != NULL)
		{
			list* current = hasht->buckets[i].first;
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

void createTable(hashTable* hasht, int size)
{
	hasht->buckets = (bucket*)malloc(sizeof(bucket) * size);
	hasht->countElements = 0;
	hasht->size = size;
	for (int i = 0; i < size; i++)
	{
		hasht->buckets[i].first = NULL;
		hasht->buckets[i].countElements = 0;
	}
}

void freeTable(hashTable* hasht)
{
	for (int i = 0; i < hasht->size; i++)
	{
		list* current = hasht->buckets[i].first;
		if (current == NULL)
			continue;

		list* next = NULL;
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

void printTable(hashTable* hasht)
{
	printf("\n");
	for (int i = 0; i < hasht->size; i++)
		if (hasht->buckets[i].first != NULL)
		{
			printf("%d: [", i);
			list* current = hasht->buckets[i].first;
			while (current->next != NULL)
			{
				printf("%d; ", current->value);
				current = current->next;
			}
			printf("%d]\n", current->value);
		}
	printf("\n");
}

int _add(list** head, int key)
{
	int value = key;

	if (*head == NULL)
	{
		*head = (list*)malloc(sizeof(list));
		(*head)->value = value;
		(*head)->next = NULL;
		return SUCCESS;
	}

	list* current = *head;
	list* prev = NULL;
	while (current->next != NULL && current->value < value)
	{
		prev = current;
		current = current->next;
	}
	if (current->value == value)
		return FAIL;

	if (prev == NULL && current->value > value)
	{
		list* first = (list*)malloc(sizeof(list));
		first->value = value;
		first->next = *head;
		*head = first;
		return SUCCESS;
	}

	if (current->next == NULL && current->value < value)
	{
		list* last = (list*)malloc(sizeof(list));
		current->next = last;
		last->value = value;
		last->next = NULL;
		return SUCCESS;
	}

	list* between = (list*)malloc(sizeof(list));
	between->value = value;
	between->next = current;
	prev->next = between;
	return SUCCESS;
}

void add(hashTable* hasht, int key)
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

int _delete(list** head, int key)
{
	int value = key;

	if (*head == NULL)
		return FAIL;

	if ((*head)->value == value)
	{
		list* toDelete = *head;
		*head = (*head)->next;
		free(toDelete);
		return SUCCESS;
	}

	list* current = *head;
	list* prev = NULL;
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

void del(hashTable* hasht, int key)
{
	int n = hash(key, hasht->size);

	if (_delete(&hasht->buckets[n].first, key))
	{
		hasht->buckets[n].countElements--;
		hasht->countElements--;
	}
}

int _search(list* head, int key)
{
	int value = key;

	if (head == NULL)
		return FAIL;

	list* current = head;
	while (current->next != NULL && current->value < value)
	{
		current = current->next;
	}
	if (current->value == value)
		return SUCCESS;

	return FAIL;
}

void search(hashTable* hasht, int key)
{
	int n = hash(key, hasht->size);
	if (_search(hasht->buckets[n].first, key))
		printf("Element %d was found\n", key);
	else
		printf("Element %d was not found\n", key);
}