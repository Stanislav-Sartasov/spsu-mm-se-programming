#include "myList.h"
#include "myHashTable.h"
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>

int hashFunc(table* hashTable, int value)
{
	return ((long long int)(hashTable->arg) * ((long long int)value % (hashTable->primeArg))) % (hashTable->size);
}

void add(table* hashTable, int value)
{
	int primes[15] = { 106033, 108301, 112909, 115249, 32469,
		59656, 90825, 262419, 361275, 481899, 514229, 65537, 160001, 331777, 614657 };
	while ((hashTable->heads)[hashFunc(hashTable, value)].value >= 10)
	{
		elem* new = (elem*)malloc(2 * (hashTable->size) * sizeof(elem));
		int size = hashTable->size;
		hashTable->size = 2 * (hashTable->size);
		hashTable->primeArg = primes[rand() % 15];
		hashTable->arg = rand() % (hashTable->primeArg);
		for (int i = 0; i < hashTable->size; i++)
		{
			new[i].next = NULL;
			new[i].value = 0;
		}
		for (int i = 0; i < size; i++)
		{
			elem* tmp = (hashTable->heads)[i].next;
			while (tmp)
			{
				push(&(new[hashFunc(hashTable, tmp->value)].next), tmp->value);
				new[hashFunc(hashTable, tmp->value)].value++;
				tmp = tmp->next;
			}
		}
		free(hashTable->heads);
		hashTable->heads = new;
	}
	push(&hashTable->heads[hashFunc(hashTable, value)].next, value);
	hashTable->heads[hashFunc(hashTable, value)].value++;
}

void del(table* hashTable, int value)
{
	if (delVal(&(hashTable->heads[hashFunc(hashTable, value)].next), value))
	{
		hashTable->heads[hashFunc(hashTable, value)].value -= 1;
	}
}

bool find(table* hashTable, int value)
{
	return findVal(((hashTable->heads)[hashFunc(hashTable, value)]).next, value);
}

table* createHashTable()
{
	table* hashTable = (table*)malloc(sizeof(table));
	int primes[15] = { 106033, 108301, 112909, 115249, 32469,
		59656, 90825, 262419, 361275, 481899, 514229, 65537, 160001, 331777, 614657 };
	hashTable->size = 5;
	hashTable->primeArg = primes[rand() % 15];
	hashTable->arg = rand() % (hashTable->primeArg);
	hashTable->heads = (elem*)malloc(sizeof(elem) * (hashTable->size));
	for (int i = 0; i < hashTable->size; i++)
	{
		hashTable->heads[i].next = NULL;
		hashTable->heads[i].value = 0;
	}
	return hashTable;
}

void deleteHashTable(table* hashTable)
{
	free(hashTable->heads);
	free(hashTable);
}

void demoHashTable(table* hashTable)
{
	for (int i = 0; i < hashTable->size; i++)
	{
		printf("List %d: ", i, hashTable->heads[i].value);
		elem* tmp = hashTable->heads[i].next;
		if (tmp == NULL)
		{
			printf("empty");
		}
		while (tmp)
		{
			printf("%d ", tmp->value);
			tmp = tmp->next;
		}
		printf("\n");
	}
}