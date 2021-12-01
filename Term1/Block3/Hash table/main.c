#include "table.h"

#define DEFAULT_SIZE 11

void addTest(hashTable* hasht);
void delTest(hashTable* hasht);
void searchTest(hashTable* hasht);

int main()
{
	printf("This program implements a data structure called a hash table"
		"with three main functions(adding, deleting and searching for elements)\n\n");
	hashTable hasht;
	createTable(&hasht, DEFAULT_SIZE);
	addTest(&hasht);
	searchTest(&hasht);
	delTest(&hasht);
	printf("All hash table:");
	printTable(&hasht);
	freeTable(&hasht);
	return 0;
}

void addTest(hashTable* hasht)
{
	printf("Adding some elements...\n\n");
	srand(0);
	for (int i = 0; i < 50; i++)
		add(hasht, rand() % 100);
}

void delTest(hashTable* hasht)
{
	printf("Deleting some elemenets...\n\n");
	srand(1);
	for (int i = 0; i < 30; i++)
		del(hasht, rand() % 100);
}

void searchTest(hashTable* hasht)
{
	printf("Search for some elements:\n");
	srand(2);
	for (int i = 0; i < 10; i++)
		search(hasht, rand() % 100);
	printf("\n");
}