#include <stdio.h>
#include "hash_table.h"
#include <string.h>
#include <malloc.h>
#include "tests.h"

int main()
{
	printf("Showcase of insert and remove operations:\n");
	add_delete_test();
	printf("\n\n");

	printf("Adding an already existing key:\n");
	add_existing_key_test();
	printf("\n\n");

	printf("Removing a key that is not in the hash table:\n");
	delete_non_existing_key_test();
	printf("\n\n");

	printf("Showcase of rebalance:\n");
	rebalance_test();
	return 0;
}