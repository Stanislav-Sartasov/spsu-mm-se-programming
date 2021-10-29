#define _CRT_NO_SECURE_WARNINGS
#include <stdio.h>
#include "hash_table.h"
#include "input_and_tools.h"

int main()
{
	printf("The programm for working with a hash-table\n");
	printf("First enter a key then enter a value to add element\n");
	printf("Enter a negative key to delete element of this positive key\n");
	printf("Enter a value that 0 to find an element of this key\n");
	printf("If element is not found, 0 will be returned\n");
	printf("Enter a key that 0 to stop\n\n");

	table* keys = create_table(32);

	int key;
	while(1)
	{
		printf("\n\n///////////////////////\n\n\n");

		printf("Enter a key\n");
		if (!(key = get_int()))
		{
			break;
		}

		if (key < 0)
		{
			del_element(keys, key * (-1));
			printf("Your element deleted\n");
			continue;
		}

		printf("Enter a value\n");
		int value = get_int();

		if (find_element(keys, key) && value)
		{
			printf("This key already exists\n");
			continue;
		}

		if (value)
		{
			add_element(keys, key, value);
			printf("Your element added\n");
		}
		else
		{
			printf("Your value: %d\n", find_element(keys, key));
		}
	}

	printf("The programm stoped\n");

	drop_table(keys);

	return 0;
}