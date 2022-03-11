#include <stdlib.h>
#include <stdbool.h>
#include "myList.h"

void pop(elem** head)
{
	if (*head != NULL)
	{
		*head = (*head)->next;
	}
}

bool delVal(elem** head, int value)
{
	if (*head == NULL)
	{
		return false;
	}
	if ((*head)->value == value)
	{
		pop(head);
		return true;
	}
	elem* tmp = *head;
	while (tmp->next && (tmp->next)->value != value)
	{
		tmp = tmp->next;
	}
	if (tmp->next == NULL)
	{
		return false;
	}
	else
	{
		pop(&(tmp->next));
		return true;
	}
}

bool findVal(elem* head, int value)
{
	while (head && head->value != value)
	{
		head = head->next;
	}
	if (head != NULL)
	{
		return true;
	}
	else
	{
		return false;
	}
}

void push(elem** head, int value)
{
	elem* first = (elem*)malloc(sizeof(elem));
	first->value = value;
	first->next = *head;
	*head = first;
}