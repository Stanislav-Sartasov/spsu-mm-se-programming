#include <stdlib.h>
#include <stdio.h>
#include "hash_table.h"
#define SIZE 2048

int main()
{
	hash_table my_table;
	create_table(&my_table, SIZE);

	add(&my_table, 3, 3);
	add(&my_table, 5, 5);
	add(&my_table, 123, 123);
	add(&my_table, 102, 102);
	add(&my_table, 103, 103);
	show_table(&my_table);

	add(&my_table, 103, 3);
	show_table(&my_table);

	add(&my_table, 90, 90);
	add(&my_table, 9, 9);

	if (get(&my_table, 123)!=NULL && printf("<%d>\n", get(&my_table, 123)) );
	show_table(&my_table);


	delete_table(&my_table, SIZE);
}