#include <stdio.h>
#include "hash_table.h"

int main()
{
	printf("This program realizes an user interface for hash table.\n");
	printf("+ [KEY] [VALUE] Create or change a value of an element in the hash table\n");
	printf("- [KEY]         Remove an element from the hash table\n");
	printf("? [KEY]         Find an element in the hash table\n");
	printf("q               Exit from the programm\n");
	struct hash_table *table = create_table(11);
	char operation = '!';
	printf(">");
	while (operation != 'q')
	{
		scanf("%c", &operation);
		switch (operation)
		{
			int key, value, correctly_scan;
			case '+':
				do
				{
					correctly_scan = scanf("%d%d", &key, &value);
					if (1 != correctly_scan)
					{
						printf("It's not integers")
					}
				}
				while (1 != correctly_scan)
				add_element(table, key, value);
				printf("Element added\n");
				break;
			case '?':
				do
				{
					correctly_scan = scanf("%d", &key);
					if (1 != correctly_scan)
					{
						printf("It's not integer")
					}
				}
				while (1 != correctly_scan)
				INT8 ret = find_element(table, key, &value);
				if (ret)
					printf("Value of element with that key: %d\n", value);
				else
					printf("Element wasn't found\n");
				break;
			case '-':
				do
				{
					correctly_scan = scanf("%d", &key);
					if (1 != correctly_scan)
					{
						printf("It's not integer")
					}
				}
				while (1 != correctly_scan)
				delete_element(table, key);
				printf("Element removed\n");
				break;
			default:
				printf(">");
		}
	}
	free_table(table);
	return 0;
}
