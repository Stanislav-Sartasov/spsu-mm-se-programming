#include <stdio.h>
#include <stddef.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>
#include <math.h>

#define DEFAULT_HASHTABLE_SIZE 17
#define DEFAULT_HASHTABLE_LOAD_FACTOR 0.75
#define NOT_FOUND -1
bool isSimple(int n)
{
	for (int d = 2; d < (int) sqrt(n) + 1; ++d)
		if (n % d == 0) return false;
	return (n != 1);
}

int nextSimple(int n)
{
	n = (n % 2 == 0) ? n + 1 : n;
	while (!isSimple(n))
		n += 2;
	return n;
}

int hash(char *key, int size)
{
	// size argument must be simple for optimal hash results
	int result = 0;

	for (int i = 0; i < strlen(key); ++i)
		result = ((size + 1) * result + key[i]) % size;

	result = (result * 2 + 1) % size;
	return result;
}

struct Element
{
	char *key;
	int value;
	struct Element *next;
};

struct Element *newElement(char *key, int value, struct Element *next)
{
	struct Element *self = malloc(sizeof(struct Element));
	self->key = key;
	self->value = value;
	self->next = next;
	return self;
}

struct Element *initElementByDefault()
{
	return newElement(NULL, 0, NULL);
}

bool isEqualElementByKey(struct Element *self, struct Element *element)
{
	return (self->key == element->key);
}

void deleteElement(struct Element *self)
{
	free(self);
}

struct LinkedList
{
	struct Element *head;
	int size;
};

struct LinkedList *newLinkedList(struct Element *head, int size)
{
	struct LinkedList *self = malloc(sizeof(struct LinkedList));
	self->head = (head == NULL) ? initElementByDefault() : head;
	self->size = size;
	return self;
}

struct LinkedList *initLinkedListByDefault()
{
	return newLinkedList(NULL, 0);
}

void deleteLinkedList(struct LinkedList *self)
{
	deleteElement(self->head);
	free(self);
}

void appendElement(struct LinkedList *self, struct Element *element)
{
	struct Element *current = self->head;
	while (current->next != NULL)
	{
		current = current->next;
	}
	current->next = element;
	self->size++;
}

void insertElement(struct LinkedList *self, struct Element *element, int index)
{
	struct Element *current = self->head;
	struct Element *prev;
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

struct Element *popElement(struct LinkedList *self, int index)
{
	struct Element *current = self->head;
	struct Element *prev;
	for (int i = 0;  current->next != NULL; ++i)
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

int getElementIndex(struct LinkedList *self, struct Element *element,
					bool (*equalityFunc)(struct Element *, struct Element *))
{
	struct Element *current = self->head;
	for (int i = 0; current->next != NULL; ++i)
	{
		current = current->next;
		if (equalityFunc(current, element))
			return i;
	}
	return NOT_FOUND;
}

struct Element *getElementByIndex(struct LinkedList *self, int index)
{
	struct Element *current = self->head;
	for (int i = 0; current->next != NULL; ++i)
	{
		current = current->next;
		if (i == index)
			return current;
	}
	return NULL;
}

void replaceElementByIndex(struct LinkedList *self, struct Element *element, int index)
{
	popElement(self, index);
	insertElement(self, element, index);
}

void removeElement(struct LinkedList *self, struct Element *element,
				   bool (*equalityFunc)(struct Element *, struct Element *))
{

	struct Element *current = self->head;
	struct Element *prev;
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

struct HashTable
{
	struct LinkedList **table;
	int size;
	int addedElementsCount;
	double loadFactor;
};

struct HashTable *newHashTable(struct LinkedList **table, int size, int addedElementsCount, double loadFactor)
{
	struct HashTable *self = malloc(sizeof(struct HashTable));
	self->size = size;
	self->addedElementsCount = addedElementsCount;
	self->loadFactor = loadFactor;
	if (table != NULL)
	{
		self->table = table;
		return self;
	}

	self->table = malloc(self->size * sizeof(struct LinkedList *));
	for (int i = 0; i < self->size; ++i)
		self->table[i] = initLinkedListByDefault();

	return self;
}

struct HashTable *initHashTableByDefault()
{
	return newHashTable(NULL, DEFAULT_HASHTABLE_SIZE, 0, DEFAULT_HASHTABLE_LOAD_FACTOR);
}

bool isTimeToResize(struct HashTable *self)
{
	return self->addedElementsCount > self->size * self->loadFactor;
}

void resize(struct HashTable *self, int size)
{
	struct LinkedList **newTable = malloc(size * sizeof(struct LinkedList *));
	for (int i = 0; i < size; ++i)
		newTable[i] = initLinkedListByDefault();

	for (int i = 0; i < self->size; ++i)
	{
		struct Element *currentElement = self->table[i]->head;
		while (currentElement->next != NULL)
		{
			currentElement = currentElement->next;
			appendElement(newTable[hash(currentElement->key, size)], currentElement);
		}
		deleteLinkedList(self->table[i]);
	}
	free(self->table);
	self->table = newTable;
	self->size = size;
}

void addByKey(struct HashTable *self, char *key, int value)
{
	struct Element *elementToAdd = newElement(key, value, NULL);
	struct LinkedList *listWhereElementLocated = self->table[hash(key, self->size)];
	int index = getElementIndex(listWhereElementLocated, elementToAdd, isEqualElementByKey);
	if (index == NOT_FOUND)
		appendElement(listWhereElementLocated, elementToAdd);
	else
		replaceElementByIndex(listWhereElementLocated, elementToAdd, index);
	self->addedElementsCount++;

	if (isTimeToResize(self))
		resize(self, nextSimple(2 * self->size));
}

void removeByKey(struct HashTable *self, char *key)
{
	struct Element *searchedElement = initElementByDefault();
	searchedElement->key = key;
	removeElement(self->table[hash(key, self->size)], searchedElement, isEqualElementByKey);
	self->addedElementsCount--;
}


int getValueByKey(struct HashTable *self, char *key)
{
	struct LinkedList *list = self->table[hash(key, self->size)];
	struct Element *searchedElement = initElementByDefault();
	searchedElement->key = key;
	int index = getElementIndex(list, searchedElement, isEqualElementByKey);
	return (index == NOT_FOUND)? NOT_FOUND : getElementByIndex(list, index)->value;
}


void deleteHashTable(struct HashTable *self)
{
	for (int i = 0; i < self->size; ++i)
		deleteLinkedList(self->table[i]);
	free(self->table);
	free(self);
}


void resizeTest();
void searchTest();
void deleteTest();

int main()
{

	resizeTest();
	searchTest();
	deleteTest();
	return 0;
}


void resizeTest()
{
	struct HashTable *ht = initHashTableByDefault();
	char *keys[15] = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "10", "11", "12", "13", "14"};
	for (int i = 0; i < 15; ++i)
	{
		addByKey(ht, keys[i], i);
		printf("Current array size - %d\n", ht->size);
	}
	deleteHashTable(ht);
}



void searchTest()
{
	struct HashTable *ht = initHashTableByDefault();
	addByKey(ht, "1", 1234);
	printf("Value of founded key - %d\n", getValueByKey(ht, "1"));
	printf("Value of not founded key - %d\n", getValueByKey(ht, "Hello"));
	deleteHashTable(ht);
}

void deleteTest()
{
	struct HashTable *ht = initHashTableByDefault();
	addByKey(ht, "1", 1234);
	printf("Value of key - %d\n", getValueByKey(ht, "1"));
	removeByKey(ht, "1");
	printf("Value of key after deletion - %d\n", getValueByKey(ht, "1"));
	deleteHashTable(ht);
}