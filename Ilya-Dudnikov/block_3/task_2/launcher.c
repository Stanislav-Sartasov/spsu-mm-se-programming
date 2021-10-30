#include <stdio.h>
#include "hash_table.h"
#include <string.h>
#include <malloc.h>

int main()
{
	printf("This program is an implementation of hash table\n");
	printf("Usage: add <key> <value> to map value to a key\n");
	printf("find <key> to find a value that is mapped to <key>\n");
	printf("delete <key> to delete key and value mapped to this key\n");
	printf("print to print the table\n");
	printf("exit to finish the program\n");

	hash_table ht = {.current_size = INITIAL_TABLE_SIZE, .table = (lst**) malloc(INITIAL_TABLE_SIZE * sizeof(lst*))};
	initialize_hash_table(&ht);

	char input[8] = {'\0'};

	while (strcmp(input, "exit") != 0)
	{
		scanf("%s", input);
		if (strcmp(input, "add") == 0)
		{
			int key, value;
			char after = '\0';
			int correct_input = 1;
			if (scanf("%d %d%c", &key, &value, &after) != 3 || after != '\n')
			{
				printf("Invalid usage of add command. Correct usage: add <key> <value>\n");
				while (after != '\n')
					scanf("%c", &after);
				correct_input = 0;
			}
			if (!correct_input)
				continue;
			add_pair(&ht, key, value);
		}
		else if (strcmp(input, "find") == 0)
		{
			int key;
			char after = '\0';

			int correct_input = 1;
			if (scanf("%d%c", &key, &after) != 2 || after != '\n')
			{
				printf("Invalid usage of find command. Correct usage: find <key>\n");
				while (after != '\n')
					scanf("%c", &after);
				correct_input = 0;
			}
			if (!correct_input)
				continue;
			lst *found = find(&ht, key);
			if (found == NULL)
				printf("There is no element with such key\n");
			else
				printf("%d\n", found->value);
		}
		else if (strcmp(input, "delete") == 0)
		{
			int key;
			char after = '\0';

			int correct_input = 1;
			if (scanf("%d%c", &key, &after) != 2 || after != '\n')
			{
				printf("Invalid usage of delete command. Correct usage: delete <key>\n");
				while (after != '\n')
					scanf("%c", &after);
				correct_input = 0;
			}
			if (!correct_input)
				continue;
			delete(&ht, key);
		}
		else if (strcmp(input, "print") == 0)
		{
			print_table(&ht);
		}
		else
		{
			printf("Invalid input: there is no such command\n");
		}
	}

	delete_hash_table(&ht);
}