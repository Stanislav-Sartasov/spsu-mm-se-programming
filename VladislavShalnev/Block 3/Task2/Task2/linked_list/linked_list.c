#include <stdlib.h>

#include "linked_list.h"

ll_t* llcreate()
{
	ll_t* list = (ll_t*)malloc(sizeof(ll_t));
	if (list == NULL)
		return NULL;

	list->length = 0;
	list->next = NULL;

	return list;
}

void llfree(ll_t* head)
{
	if (head->next != NULL)
		llfree(head->next);

	free(head);
}

ll_t* lladd(ll_t* head, int key, int value)
{
	lli_t* elem = (lli_t*)malloc(sizeof(lli_t));
	if (elem == NULL)
		return NULL;

	elem->key = key;
	elem->value = value;

	elem->next = head->next;
	head->next = elem;
	head->length++;

	return head;
}

lli_t* llfind(ll_t* head, int key)
{
	lli_t* elem = head->next;

	while (elem != NULL)
	{
		if (elem->key == key)
			return elem;

		elem = elem->next;
	}
	
	return NULL;
}

int llremove(ll_t* head, int key)
{
	if (head == NULL || head->length == 0)
		return 0;

	// if first element
	if (head->next->key == key)
	{
		lli_t* next = head->next->next;
		free(head->next);

		head->next = next;
		head->length--;

		return 1;
	}

	// else
	lli_t* elem = head->next;
	while (elem->next)
	{
		if (elem->next->key == key)
		{
			lli_t* next = elem->next->next;
			free(elem->next);

			elem->next = next;
			head->length--;

			return 1;
		}

		elem = elem->next;
	}

	return 0;
}