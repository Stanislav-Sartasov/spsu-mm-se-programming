#include "list.h"
#include <stdlib.h>
#include <stddef.h>


Element *newElement(char *key, int value, Element *next)
{
	Element *self = malloc(sizeof(Element));
	self->key = key;
	self->value = value;
	self->next = next;
	return self;
}

Element *initElementByDefault()
{
	return newElement(NULL, 0, NULL);
}

void deleteElement(Element *self)
{
	free(self);
}


LinkedList *newLinkedList(Element *head, int size)
{
	LinkedList *self = malloc(sizeof(LinkedList));
	self->head = (head == NULL) ? initElementByDefault() : head;
	self->size = size;
	return self;
}

LinkedList *initLinkedListByDefault()
{
	return newLinkedList(NULL, 0);
}

void deleteLinkedList(LinkedList *self)
{
	deleteElement(self->head);
	free(self);
}

void appendElement(LinkedList *self, Element *element)
{
	Element *current = self->head;
	while (current->next != NULL)
	{
		current = current->next;
	}
	current->next = element;
	self->size++;
}

void insertElement(LinkedList *self, Element *element, int index)
{
	Element *current = self->head;
	Element *prev;
	for (int i = 0; current->next != NULL; ++i)
	{
		prev = current;
		current = current->next;
		if (i == index)
		{
			prev->next = element;
			element->next = current;
			self->size++;
			return;
		}
	}
}

Element *popElement(LinkedList *self, int index)
{
	Element *current = self->head;
	Element *prev;
	for (int i = 0; current->next != NULL; ++i)
	{
		prev = current;
		current = current->next;
		if (i == index)
		{
			prev->next = current->next;
			self->size--;
			return current;
		}
	}
	return NULL;

}

int getElementIndex(LinkedList *self, Element *element,
					bool (*equalityFunc)(Element *, Element *))
{
	Element *current = self->head;
	for (int i = 0; current->next != NULL; ++i)
	{
		current = current->next;
		if (equalityFunc(current, element))
			return i;
	}
	return NOT_FOUND;
}

Element *getElementByIndex(LinkedList *self, int index)
{
	Element *current = self->head;
	for (int i = 0; current->next != NULL; ++i)
	{
		current = current->next;
		if (i == index)
			return current;
	}
	return NULL;
}

void replaceElementByIndex(LinkedList *self, Element *element, int index)
{
	popElement(self, index);
	insertElement(self, element, index);
}

void removeElement(LinkedList *self, Element *element,
				   bool (*equalityFunc)(Element *, Element *))
{

	Element *current = self->head;
	Element *prev;
	while (current->next != NULL)
	{
		prev = current;
		current = current->next;
		if (equalityFunc(current, element))
		{
			prev->next = current->next;
			self->size--;
			deleteElement(current);
			return;
		}
	}
}
