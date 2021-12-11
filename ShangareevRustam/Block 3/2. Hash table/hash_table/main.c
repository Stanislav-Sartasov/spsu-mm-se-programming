#include <stdio.h>
#include "myList.h"
#include "myHashTable.h"

int main()
{
	printf("This program shows the power of a hash table\n\n");
	table* hashTable = createHashTable();
	printf("Let's add 10 random elements to the hash "
		"table and look at the lists inside the table:\n");
	for (int i = 0; i < 10; i++)
	{
		add(hashTable, rand());
	}
	demoHashTable(hashTable);
	printf("\nLet's add 40 more random elements to show the rebalance function. "
		"Let's look at hash table lists:\n");
	for (int i = 0; i < 40; i++)
	{
		add(hashTable, rand());
	}
	demoHashTable(hashTable);
	printf("\nLet's remove some elements of the list to show "
		"the function of deleting a hash table element:\n");
	for (int i = 0; i < 50000; i++)
	{
		del(hashTable, rand());
	}
	demoHashTable(hashTable);
	printf("\nLet's demonstrate the element search function:\n");
	int cnt = 0;
	for (int i = 0; i < 10000; i++)
	{
		cnt += (int)find(hashTable, i);
	}
	printf("Check each number from the segment [0; 9999] for hash table ownership\n");
	printf("The number of elements of the hash list from the segment [0; 9999] = %d\n", cnt);
	deleteHashTable(hashTable);
	return 0;
}