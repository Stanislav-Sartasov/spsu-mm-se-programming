#include <stdlib.h>
#include <stdio.h>
#include <math.h>
#include "hashtable.h"


int main()
{
	int i, k, value, an_number, flag;
	htable test;
	int* input_arr;
	int number;
	number = 100;
	printf("This program tests the hashtable functions\n");

	/* Creating array for testing hashtable functions */

	input_arr = malloc(number * sizeof(int));
	for (i = 0; i < number; ++i)
	{
		input_arr[i] = rand() % (int)pow(2, 30);
	}

	/* Testing hashtable functions */

	test = create_htable(number);
	for (i = 0; i < number; ++i)
	{
		add_elem(input_arr[i], &test);
	}
	if (find_elem(input_arr[10], test) == 1)
	{
		printf("Element %d is in hashtable\n", input_arr[10]);
	}
	printf("Deleting element %d from hashtable\n", input_arr[10]);
	delete_elem(input_arr[10], &test);
	if (find_elem(input_arr[10], test) == 0)
	{
		printf("Element %d is not in hashtable\n", input_arr[10]);
	}
	return 0;
}