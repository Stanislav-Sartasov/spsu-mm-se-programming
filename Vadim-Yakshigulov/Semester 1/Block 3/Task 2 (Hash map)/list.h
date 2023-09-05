#pragma once

#include <stdbool.h>

#define NOT_FOUND -1

typedef struct elementOfList
{
	char *key;
	int value;
	struct elementOfList *next;
} elementOfList;

typedef struct linkedList
{
	elementOfList *head;
	int size;
} linkedList;

elementOfList *newElement(char *key, int value, elementOfList *next);

elementOfList *initElementByDefault();

void deleteElement(elementOfList *self);

linkedList *newLinkedList(elementOfList *head, int size);

linkedList *initLinkedListByDefault();

void deleteLinkedList(linkedList *self);

void appendElement(linkedList *self, elementOfList *element);

void insertElement(linkedList *self, elementOfList *element, int index);

elementOfList *popElement(linkedList *self, int index);

int getElementIndex(linkedList *self, elementOfList *element,
					bool (*equalityFunc)(elementOfList *, elementOfList *));

elementOfList *getElementByIndex(linkedList *self, int index);

void replaceElementByIndex(linkedList *self, elementOfList *element, int index);

void removeElement(linkedList *self, elementOfList *element,
				   bool (*equalityFunc)(elementOfList *, elementOfList *));
