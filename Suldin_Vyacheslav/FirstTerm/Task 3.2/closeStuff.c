#include <stdlib.h>
#include <stdio.h>
#include "closeStuff.h"

void flushStdin(void)
{
	char ch;
	while (scanf_s("%c", &ch) == 1 && ch != '\n')
	{
	}
}

int getNumber(int* number, const int top)
{
	return !(scanf_s("%d", number) == 1 && *number < top);
}

int input(int min, int max)
{
	int number;
	while (getNumber(&number, 1000000000) || number < min || number > max)
	{
		fprintf(stderr, "Wrong input! \n");
		flushStdin();
	}
	return number;
}

int hashFunction(int key, int balance)
{
	if (key < 0 && key % balance != 0) return (key % balance) + balance;
	return (key % balance);
}

void createLists(int start, int end, element** keys)
{
	for (int i = start; i < end;i++)
	{
		element* first = (element*)malloc(sizeof(element));
		first->next = NULL;
		first->key = -1;
		keys[i] = first;
	}
}

void rebalancing(element** keys, int* balance)
{
	printf("rebalancing...\n");
	createLists(*balance, *balance * 2, keys);
	for (int i = 0; i < *balance;i++)
	{
		element* head = keys[i]->next;
		int tmp = keys[i]->key;
		if (tmp == -1 && head == NULL)
		{
			continue;
		}
		if (hashFunction(tmp, *balance) != hashFunction(tmp, *balance * 2))
		{
			add(&keys[hashFunction(tmp, *balance * 2)], tmp, keys[i]-> value);
			deleteAndSearch(&keys[hashFunction(tmp, *balance)], tmp, 1);
		}
		while (head->next != NULL)
		{
			element* temp = head->next;
			if (hashFunction(head->key, *balance) != hashFunction(head->key, *balance * 2))
			{
				add(&keys[hashFunction(head->key, *balance * 2)], head->key, head->value);
				deleteAndSearch(&keys[hashFunction(head->key, *balance)], head->key, 1);
			}
			head = temp;
		}
	}
	*balance *= 2;
	printf("rebalanced...\n");
}

void check(element** keys, int hashKey, int* balance)
{
	int elements = 0;
	element* head = keys[hashKey]->next;
	while (head)
	{
		elements++;
		head = head->next;
	}
	if (elements > *balance / 2)
	{
		rebalancing(keys, balance);
	}
}
