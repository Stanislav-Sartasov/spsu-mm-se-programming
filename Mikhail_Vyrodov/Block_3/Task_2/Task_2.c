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
		printf("%d\n", input_arr[i]);
	}

	/* Testing hashtable functions */

	test = create_htable(number);
	for (i = 0; i < number; ++i)
	{
		add_elem(input_arr[i], &test, number);
	}
	printf("%d\n", find_elem(input_arr[10], test));
	delete_elem(input_arr[10], &test);
	printf("%d", find_elem(input_arr[10], test));
	return 0;
}