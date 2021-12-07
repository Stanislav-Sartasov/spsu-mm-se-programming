#include "list.h"
#include <stdlib.h>
#include <stddef.h>


elementOfList *newElement(char *key, int value, elementOfList *next)
{
	elementOfList *self = malloc(sizeof(elementOfList));
	self->key = key;
	self->value = value;
	self->next = next;
	return self;
}

elementOfList *initElementByDefault()
{
	return newElement(NULL, 0, NULL);
}

void deleteElement(elementOfList *self)
{
	free(self);
}


linkedList *newLinkedList(elementOfList *head, int size)
{
	linkedList *self = malloc(sizeof(linkedList));
	self->head = (head == NULL) ? initElementByDefault() : head;
	self->size = size;
	return self;
}

linkedList *initLinkedListByDefault()
{
	return newLinkedList(NULL, 0);
}

void deleteLinkedList(linkedList *self)
{
	deleteElement(self->head);
	free(self);
}

void appendElement(linkedList *self, elementOfList *element)
{
	elementOfList *current = self->head;
	while (current->next != NULL)
	{
		current = current->next;
	}
	current->next = element;
	self->size++;
}

void insertElement(linkedList *self, elementOfList *element, int index)
{
	elementOfList *current = self->head;
	elementOfList *prev;
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

elementOfList *popElement(linkedList *self, int index)
{
	elementOfList *current = self->head;
	elementOfList *prev;
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

int getElementIndex(linkedList *self, elementOfList *element,
					bool (*equalityFunc)(elementOfList *, elementOfList *))
{
	elementOfList *current = self->head;
	for (int i = 0; current->next != NULL; ++i)
	{
		current = current->next;
		if (equalityFunc(current, element))
			return i;
	}
	return NOT_FOUND;
}

elementOfList *getElementByIndex(linkedList *self, int index)
{
	elementOfList *current = self->head;
	for (int i = 0; current->next != NULL; ++i)
	{
		current = current->next;
		if (i == index)
			return current;
	}
	return NULL;
}

void replaceElementByIndex(linkedList *self, elementOfList *element, int index)
{
	popElement(self, index);
	insertElement(self, element, index);
}

void removeElement(linkedList *self, elementOfList *element,
				   bool (*equalityFunc)(elementOfList *, elementOfList *))
{

	elementOfList *current = self->head;
	elementOfList *prev;
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
