#include "hash_table.h"
#include <stdio.h>

int get_number(char* string, int* mode_flag)
{
	printf("%s >>> ", string);
	char start;
	int num;
	char end;
	int exit = scanf("%1[c]", &start);
	if (exit)
	{
		*mode_flag = 3;
		return 0;
	}
	int switch_mode = scanf("%1[\n]", &start);
	if (switch_mode)
	{
		printf("\n");
		*mode_flag = (*mode_flag + 1) % 3;
		return 0;
	}
	int read_result = scanf("%d%c", &num, &end);
	if ((read_result == 2) && (end == '\n'))
	{
		return num;
	}
	else
	{
		printf("Please enter a correct number!\n");
	}

	while (end != '\n')
	{
		scanf("%c", &end);
	}

	return get_number(string, mode_flag);
}

int main()
{
	printf("This program shows possible examples of using hash-table structure.\n");
	printf("Keys and values are supposed to be 32-bit signed integers.\n");
	printf("If program can't find a proper value for given key in hash-table, default value (zero) is returned.\n\n");

	printf("You may now input different keys and values, related to them, to be added in hash table.\nAlso you may get values using their keys or delete them by key.\n");
	printf("To switch between modes press Enter instead of number. To close program input \"c\"\n\n");

	hash_table* table = new_table();

	int mode_flag = 0;
	int value;
	int key;
	while (mode_flag < 3)
	{
		switch (mode_flag)
		{
			case 0:
				printf("mode: add\n");
				value = get_number("Enter a value to be added", &mode_flag);
				if (mode_flag != 0)
				{
					break;
				}
				key = get_number("Enter its key", &mode_flag);
				if (mode_flag != 0)
				{
					break;
				}
				add(table, key, value);
				printf("Pair {%d:%d} successfully added.\n\n", key, value);
				break;
			case 1:
				printf("mode: get\n");
				key = get_number("Enter key to get its value", &mode_flag);
				if (mode_flag != 1)
				{
					break;
				}
				printf("Value fot this key is %d\n\n", get(table, key));
				break;
			case 2:
				printf("mode: del\n");
				key = get_number("Enter key to delete its value", &mode_flag);
				if (mode_flag != 2)
				{
					break;
				}
				del(table, key);
				printf("Value fot this key was deleted (or never existed)\n\n");
				break;
			default:
				break;

		}
	}
	return 0;
}
