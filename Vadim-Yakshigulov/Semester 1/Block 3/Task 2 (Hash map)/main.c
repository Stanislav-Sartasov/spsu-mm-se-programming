#include <stdio.h>
#include "hashtable.h"

void resizeTest()
{
	printf("Hash table resize test started\n");
	hashTable *ht = initHashTableByDefault();
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
	printf("Hash table search test started\n");
	hashTable *ht = initHashTableByDefault();
	addByKey(ht, "1", 1234);
	printf("Value of founded key - %d\n", getValueByKey(ht, "1"));
	printf("Value of not founded key - %d\n", getValueByKey(ht, "Hello"));
	deleteHashTable(ht);
}

void deleteTest()
{
	printf("Hash table delete test started\n");
	hashTable *ht = initHashTableByDefault();
	addByKey(ht, "1", 1234);
	printf("Value of key - %d\n", getValueByKey(ht, "1"));
	removeByKey(ht, "1");
	printf("Value of key after deletion - %d\n", getValueByKey(ht, "1"));
	deleteHashTable(ht);
}

int main()
{
	printf("This program tests the base functionality of hashtable module\n");
	resizeTest();
	searchTest();
	deleteTest();
	return 0;
}


