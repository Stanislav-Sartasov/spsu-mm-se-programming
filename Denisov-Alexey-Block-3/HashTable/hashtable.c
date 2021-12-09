#include <stdio.h>
#include <stdlib.h>
#include "hashtable.h"

pointer* create()
{
	pointer* p = (pointer*)malloc(sizeof(pointer));
	p->arrChains = (table*)malloc(START_DIVISOR * sizeof(table));

	for (int i = 0; i < START_DIVISOR; i++)
	{
		p->arrChains[i].divisor = START_DIVISOR;
		p->arrChains[i].chainsFirst = NULL;
	}

	return p;
}

void add(pointer** p, int value)
{
	if ((*p)->arrChains[value % (*p)->arrChains->divisor].chainsFirst == NULL)
	{
		(*p)->arrChains[value % (*p)->arrChains->divisor].chainsFirst = (element*)malloc(sizeof(element));
		(*p)->arrChains[value % (*p)->arrChains->divisor].chainsFirst->next = NULL;
		(*p)->arrChains[value % (*p)->arrChains->divisor].chainsFirst->value = value;
		return;
	}

	element* head = (*p)->arrChains[value % (*p)->arrChains->divisor].chainsFirst;

	int k = 0;
	while (head->next != NULL)
	{
		head = head->next;
		k++;
		if (head->value == value)
			return;
	}

	if (k >= REBALANCE_CRITERION - 1)
	{
		*p = rebalance(p);
		add(p, value);
		return;
	}

	element* last = (element*)malloc(sizeof(element));
	last->next = NULL;
	last->value = value;
	head->next = last;
}

pointer* rebalance(pointer** p)
{
	pointer* copy = (pointer*)malloc(sizeof(pointer));
	copy->arrChains = (table*)malloc(REBALANCE_MULTIPLIER * (*p)->arrChains->divisor * sizeof(table));
	copy->arrChains->divisor = REBALANCE_MULTIPLIER * (*p)->arrChains->divisor;

	for (int i = 0; i < copy->arrChains->divisor; i++)
	{
		copy->arrChains[i].divisor = copy->arrChains->divisor;
		copy->arrChains[i].chainsFirst = NULL;
	}
	
	/*table* copy = (table*)malloc(REBALANCE_MULTIPLIER * arrChains->divisor * sizeof(table));
	copy->divisor = REBALANCE_MULTIPLIER * arrChains->divisor;
	for (int i = 0; i < copy->divisor; i++)
	{
		copy[i].divisor = copy->divisor;
		copy[i].chainsFirst = NULL;
	}*/

	element* head = NULL;
	for (int i = 0; i < (*p)->arrChains->divisor; i++)
	{
		head = (*p)->arrChains[i].chainsFirst;
		while (head != NULL)
		{
			add(&copy, head->value);
			head = head->next;
		}
	}

	return copy;
}

void del(pointer** p, int value)
{
	element* head = (*p)->arrChains[value % (*p)->arrChains->divisor].chainsFirst;
	int k = 0; element* prev = NULL;
	while (head != NULL)
	{
		if (head->value == value && k == 0)
		{
			(*p)->arrChains[value % (*p)->arrChains->divisor].chainsFirst = head->next;
			free(head);
			return;
		}
		else if (head->value == value)
		{
			prev->next = head->next;
			free(head);
			return;
		}
		prev = head;
		head = head->next;
		k++;
	}
}

int search(pointer** p, int value)
{
	element* head = (*p)->arrChains[value % (*p)->arrChains->divisor].chainsFirst;
	
	while (head != NULL)
	{
		if (head->value == value)
			return 1;
		head = head->next;
	}

	return 0;
}

void print(pointer** p)
{
	printf("----------\n");
	element* head;
	for (int i = 0; i < (*p)->arrChains->divisor; i++)
	{
		head = (*p)->arrChains[i].chainsFirst;
		while (head != NULL)
		{
			printf("%d ", head->value);
			head = head->next;
		}
		printf("\n");
	}
	printf("----------\n");
}

void freeTable(pointer** p)
{
	element* head;
	for (int i = 0; i < (*p)->arrChains->divisor; i++)
	{
		head = (*p)->arrChains[i].chainsFirst;
		while (head != NULL)
		{
			element* curr = head;
			head = head->next;
			free(curr);
		}
		free(head);
	}
	free((*p)->arrChains);
}
