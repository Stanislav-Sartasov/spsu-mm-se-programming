#include <stdio.h>

#include "hash_table/hash_table.h"

void start_testing(ht_t* table)
{
	printf("Filling the table with keys from 1 to 50...\n");
	for (int i = 1; i <= 50; i++)
		htadd(table, i, i * 123);
	printf("Done!\n\n");

	printf("Getting values with keys from 1 to 60:\n");
	for (int i = 1; i <= 60; i++)
		htget(table, i) == NULL ? printf("%d. NULL\t", i) : printf("%d. %d\t", i, htget(table, i));
	printf("Done!\n\n");

	printf("Removing values with keys from 30 to 70...\n");
	for (int i = 30; i <= 70; i++)
		htremove(table, i);
	printf("Done!\n\n");

	printf("Trying to get values with keys from 1 to 60 again:\n");
	for (int i = 1; i <= 60; i++)
		htget(table, i) == NULL ? printf("%d. NULL\t", i) : printf("%d. %d\t", i, htget(table, i));
	printf("Done!\n\n");

	printf("Testing is over!\n");
}

int main()
{
	printf("This program demonstrates the realization of a hash table.\n\n");

	printf("Creating the hash table...\n");

	ht_t* table = htcreate();

	printf("Done!\n\n");

	start_testing(table);

	htfree(table);

	return 0;
}