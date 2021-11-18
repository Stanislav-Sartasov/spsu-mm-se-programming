#include <stdlib.h>
#include "table.h"

#define DEFAULT_SIZE 11

void addTest(HashTable* hasht);
void delTest(HashTable* hasht);
void searchTest(HashTable* hasht);

int main()
{
	HashTable hasht;
	createTable(&hasht, DEFAULT_SIZE);
	addTest(&hasht);
	delTest(&hasht);
	searchTest(&hasht);
	printTable(&hasht);
	freeTable(&hasht);
	return 0;
}

void addTest(HashTable* hasht)
{
	srand(0);
	for (int i = 0; i < 50; i++)
		add(hasht, rand() % 100);
}

void delTest(HashTable* hasht)
{
	srand(1);
	for (int i = 0; i < 20; i++)
		del(hasht, rand() % 100);
}

void searchTest(HashTable* hasht)
{
	srand(2);
	for (int i = 0; i < 10; i++)
		search(hasht, rand() % 100);
}