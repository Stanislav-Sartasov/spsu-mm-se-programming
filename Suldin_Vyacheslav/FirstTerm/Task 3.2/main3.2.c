#include <stdlib.h>
#include <stdio.h>
#include "closeStuff.h"


int deleteAndSearch(element** head, int key, int type)
{
	if ((*head)->key == -1 && (*head)->next == NULL) return -1;

	if ((*head)->key == key && type == 1)
	{
		element* toDelete = *head;
		*head = (*head)->next;
		free(toDelete);
		return 0;
	}
	element* prev = NULL;
	element* cur = *head;
	while (cur->key != key && cur->next != NULL)
	{
		prev = cur;
		cur = cur->next;
	}

	if (cur->key == key && cur->next != NULL)
	{
		if (type == 0) return 123;
		else if (type == 1)
		{
			prev->next = cur->next;
			free(cur);
		}
		else printf("%d\n", cur->value);
	}
	else return -1;
	return 0;
}

void add(element** head, int key, int value)
{
	element* first = (element*)malloc(sizeof(element));
	first->key = key;
	first->value = value;
	first->next = *head;
	*head = first;
}

int main()
{
	printf("Description: Hash-map.\n>Operations: '0' - new key, '1' - delete key, '2' searh for value, '-1' - end\n");
	printf(">|Key|: <= 1000000000 \n>|Value|: <= 1000000000 \n>Revalue: '0' - yes, '1' - no\n");
	int balance = 4;
	element** keys;
	keys = (element**)malloc(balance * sizeof(int*) * 2048);
	createLists(0,balance,keys);
	while (1)
	{
		int key, operation, value;
		printf("\nENTER OPERATION>>>");
		operation = input(0, 2);
		if (operation == -1) break;
		printf("\nENTER KEY>>>");
		key = input(-1000000000, 1000000000);
		int hashKey = hashFunction(key,balance);
		if (operation == 0)
		{
			if (deleteAndSearch(&keys[hashKey], key, 0) == 123)
			{
				printf("\nKey already registered, wanna revalue?>>>");
				int rev;
				rev = input(0, 1);
				if (rev == 1)
				{
					printf("\nENTER NEW VALUE>>>");
					value = input(-1000000000, 1000000000);
					deleteAndSearch(&keys[hashKey], key, 1);
					add(&keys[hashKey], key, value);
				}
			}
			else
			{
				printf("\nENTER VALUE>>>");
				value = input(-1000000000, 1000000000);
				add(&keys[hashKey], key, value);
				printf("Successfully Added %d\n", key);
				check(keys,hashKey,&balance);
			}
				
		}
		else if (operation == 1 || operation == 2)
		{
			if (deleteAndSearch(&keys[hashKey], key, operation) == -1) printf("No such value. ");
			if ( operation == 1 ) printf("Deleting %d is ended\n", key);
		}
		/*
		printf("\n-------------------\n");
		for (int i = 0; i < balance;i++)
		{
			printf("%d- ", i);
			element* head = keys[i]->next;
			if ( head ) printf("-> [%d,%d] ", keys[i]->key, keys[i]->value);
			while (head)
			{
				if ( head->next ) printf("-> [%d,%d] ", head->key, head->value);
				head = head->next;
			}
			printf("\n");
		}
		printf("-------------------\n");
		*/
	}
	free(keys);
}