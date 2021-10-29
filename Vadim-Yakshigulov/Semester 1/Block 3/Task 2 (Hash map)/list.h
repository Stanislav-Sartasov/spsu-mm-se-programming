#pragma once

#include <stdbool.h>

#define NOT_FOUND -1

typedef struct Element
{
	char *key;
	int value;
	struct Element *next;
} Element;

typedef struct LinkedList
{
	Element *head;
	int size;
} LinkedList;

Element *newElement(char *key, int value, Element *next);

Element *initElementByDefault();

void deleteElement(Element *self);

LinkedList *newLinkedList(Element *head, int size);

LinkedList *initLinkedListByDefault();

void deleteLinkedList(LinkedList *self);

void appendElement(LinkedList *self, Element *element);

void insertElement(LinkedList *self, Element *element, int index);

Element *popElement(LinkedList *self, int index);

int getElementIndex(LinkedList *self, Element *element,
					bool (*equalityFunc)(Element *, Element *));

Element *getElementByIndex(LinkedList *self, int index);

void replaceElementByIndex(LinkedList *self, Element *element, int index);

void removeElement(LinkedList *self, Element *element,
				   bool (*equalityFunc)(Element *, Element *));
