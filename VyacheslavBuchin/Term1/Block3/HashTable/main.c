#include <stdio.h>
#include "hash_table/int_hash_map.h"

void insertion_and_get_test(hash_map_t* map)
{
	printf("Executing insertion test:\n");

	printf("Trying to insert 1, 228, 1588, 25, 15\n");
	hash_map_insert(&map, 2, 1);
	hash_map_insert(&map, 6, 228);
	hash_map_insert(&map, 3, 1588);
	hash_map_insert(&map, 57891375 + 5897, 25);
	hash_map_insert(&map, 57891375, 15);

	printf("Inserted values: %d, %d, %d, %d, %d\n",
			   hash_map_get(map, 2),
			   hash_map_get(map, 6),
			   hash_map_get(map, 3),
			   hash_map_get(map, 57891375 + 5897),
			   hash_map_get(map, 57891375)
		   );

}

void rebalance_test(hash_map_t** map)
{
	printf("Rebgalance test is running:\n");
	int changed = 0;
	for (int i = 0; i < 15; i++)
	{
		hash_map_insert(map, 54 + i * 5897, i);
		if ((*map)->current_size_ind != 0 && !changed)
			changed = 1;
	}
	if (changed)
		printf("Rebalance test passed\n");
	else
		printf("Rebalance wasn't performed :(\n");
}

void insertion_of_an_existing_key_test(hash_map_t** map)
{
	printf("Insertion of an existing key test running:\n");
	const int TESTING_KEY = 15;
	const int TESTING_VALUE1 = 228;
	const int TESTING_VALUE2 = 337;

	hash_map_insert(map, TESTING_KEY, TESTING_VALUE1);
	printf("The value of key %d is %d\n", TESTING_KEY, hash_map_get(*map, TESTING_KEY));

	printf("Inserting %d value for key %d\n", TESTING_VALUE2, TESTING_KEY);
	hash_map_insert(map, TESTING_KEY, TESTING_VALUE2);
	printf("The value of key %d is %d after inserting\n", TESTING_KEY, hash_map_get(*map, TESTING_KEY));
}

void contains_test(hash_map_t** map)
{
	if (hash_map_contains(*map, 2281337))
		printf("Key %d is in map\n", 2281337);
	else
		printf("Key %d is not in map\n", 2281337);
}

void contains_and_remove_test(hash_map_t** map)
{
	printf("Contains and remove test running:\n");
	contains_test(map);

	printf("Inserting key %d to map\n", 2281337);
	hash_map_insert(map, 2281337, 252525);
	contains_test(map);

	printf("Removing key %d from map\n", 2281337);
	hash_map_remove(*map, 2281337);
	contains_test(map);
}

void get_not_existing_test(hash_map_t* map)
{
	printf("Get function for not existing element test running:\n");
	hash_map_remove(map, 2525);
	if (hash_map_get(map, 2525) == HASH_MAP_KEY_NOT_FOUND)
		printf("Test passed\n");
	else
		printf("Test failed\n");
}

void run_tests(hash_map_t** map)
{
	insertion_and_get_test(*map);
	printf("\n");

	rebalance_test(map);
	printf("\n");

	insertion_of_an_existing_key_test(map);
	printf("\n");

	contains_and_remove_test(map);
	printf("\n");

	get_not_existing_test(*map);
	printf("\n");
}

int main()
{
	hash_map_t* map = empty_hash_map();
	run_tests(&map);

	free_hash_map(map);
	return 0;
}
